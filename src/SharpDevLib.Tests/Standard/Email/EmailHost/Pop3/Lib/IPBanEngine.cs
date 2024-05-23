using System;
using System.Collections.Generic;
using System.Net;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3.Lib;

public interface IIPBanEngine
{
    void RegisterFailedAttempt(IPAddress ip);
    bool IsBanned(IPAddress ip);
}

public class ThreeStrikesBanEngine : IIPBanEngine
{
    [System.Diagnostics.DebuggerDisplay("{ip}, {FailedAttemptCount}, {UtcLastAttempt}")]
    private class Entry(IPAddress ip)
    {
        private readonly IPAddress ip = ip;
        internal int FailedAttemptCount = 0;
        internal DateTime UtcLastAttempt = DateTime.MinValue;

        internal void Strike()
        {
            FailedAttemptCount += 1;
            UtcLastAttempt = DateTime.UtcNow;
        }

        internal bool HasExpired(int expirySeconds)
        {
            if (UtcLastAttempt == DateTime.MinValue) return false;
            return DateTime.UtcNow > UtcLastAttempt.AddSeconds(expirySeconds);
        }
    }

    private readonly object mutex;
    private readonly Dictionary<IPAddress, Entry> banned;
    private int failedAttemptThreshold;
    private int attemptExpirySeconds;
    private int ipv6UserBits;

    public ThreeStrikesBanEngine()
    {
        mutex = new object();
        banned = [];
        failedAttemptThreshold = 3;
        attemptExpirySeconds = 10000;
        ipv6UserBits = 64;
    }

    public int FailedAttemptThreshold
    {
        get => failedAttemptThreshold;
        set
        {
            if (value <= 0) throw new ArgumentException("Threshold must be one or more.");
            failedAttemptThreshold = value;
        }
    }

    public int AttemptExpirySeconds
    {
        get => attemptExpirySeconds;
        set
        {
            if (value <= 0) throw new ArgumentException("Expiry must be one second or more.");
            attemptExpirySeconds = value;
        }
    }

    public int IPv6UserBits
    {
        get => ipv6UserBits;
        set
        {
            if (value < 1 || value > 128) throw new ArgumentException("IPv6UserBits must be 1 to 128.");
            if (value % 8 != 0) throw new ArgumentException("IPv6UserBits must be a multiple of 8. (For now.)");
            ipv6UserBits = value;
        }
    }

    private void NormalizeIP(ref IPAddress ip)
    {
        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 && ipv6UserBits < 128)
        {
            var ipBytes = ip.GetAddressBytes();
            var startIndex = IPv6UserBits / 8;
            var maxIndex = 128 / 8;
            for (int addrByteIndex = startIndex; addrByteIndex < maxIndex; addrByteIndex += 1) ipBytes[addrByteIndex] = 0;
            ipBytes[15] = 1;
            ip = new IPAddress(ipBytes);
        }
    }

    public void RegisterFailedAttempt(IPAddress ip)
    {
        NormalizeIP(ref ip);
        lock (mutex)
        {
            if (banned.TryGetValue(ip, out var entry) == false)
            {
                entry = new Entry(ip);
                banned[ip] = entry;
            }
            entry.Strike();
        }
    }

    public bool IsBanned(IPAddress ip)
    {
        if (IPAddress.IsLoopback(ip)) return false;
        NormalizeIP(ref ip);
        lock (mutex)
        {
            if (banned.TryGetValue(ip, out var entry))
            {
                if (entry.HasExpired(attemptExpirySeconds))
                {
                    banned.Remove(ip);
                    return false;
                }
                return entry.FailedAttemptCount >= failedAttemptThreshold;
            }
            else return false;
        }
    }
}
