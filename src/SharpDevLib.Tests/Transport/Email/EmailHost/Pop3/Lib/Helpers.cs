using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;

internal static class Helpers
{
    static readonly HashSet<string> allowedDisposedObjectNames = ["System.Net.Sockets.Socket", "System.Net.Sockets.NetworkStream"];

    internal static void TryCallCatch(Action fn)
    {
        try
        {
            fn();
        }
        catch (ObjectDisposedException ex) when (allowedDisposedObjectNames.Contains(ex.ObjectName)) { }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Start()")) { }
    }

    internal static void DoNothingAction()
    {
    }

    internal static string AsDotQuoted(string line)
    {
        if (line.Length > 1 && line[0] == '.') return "." + line;
        else return line;
    }

    internal static Task WriteLineAsync(this Stream str, string text)
    {
        var lineAsBytes = Encoding.ASCII.GetBytes(text + "\r\n");
        return str.WriteAsync(lineAsBytes, 0, lineAsBytes.Length);
    }

    internal static bool IsConnectionClosed(this Task<int> task)
    {
        if (task.Status == TaskStatus.RanToCompletion && task.Result == 0) return true;
        if (task.Status == TaskStatus.Faulted && TestPerException(task.Exception)) return true;
        return false;

        static bool TestPerException(Exception? ex)
        {
            if (ex is AggregateException agex) return agex.InnerExceptions.Any(TestPerException);
            if (ex is IOException ioex && ioex.HResult == -2146232800) return true;
            return false;
        }
    }

    internal static void LockContinueWith(this Task task, object mutex, Action<Task> fn)
    {
        task.ContinueWith(Internal);
        void Internal(Task taskInner)
        {
            lock (mutex) fn(taskInner);
        }
    }


    internal static void LockContinueWith<T>(this Task<T> task, object mutex, Action<Task<T>> fn)
    {
        task.ContinueWith(Internal);
        void Internal(Task<T> taskInner)
        {
            lock (mutex) fn(taskInner);
        }
    }
}
