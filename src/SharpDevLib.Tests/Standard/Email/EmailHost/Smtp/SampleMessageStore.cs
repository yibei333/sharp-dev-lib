﻿using MimeKit;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Standard.Email.EmailHost.Models;
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;
using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Smtp;

public class SampleMessageStore(IServiceProvider serviceProvider) : SmtpBase(serviceProvider), IMessageStore
{
    public Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream(buffer.ToArray());
        var message = MimeMessage.Load(new ParserOptions { CharsetEncoding = Encoding.UTF8 }, memoryStream, cancellationToken);

        var fromUser = UserService.Get(x => x.Name == transaction.From.User).FirstOrDefault() ?? throw new NullReferenceException();
        var toUsers = UserService.Get(x => transaction.To.Select(x => x.User).Contains(x.Name)).ToList();
        var notExistToUsers = transaction.To.Select(x => x.User).Except(toUsers.Select(x => x.Name)).ToList();
        if (notExistToUsers.Count != 0) Console.WriteLine($"external user:{string.Join(",", notExistToUsers)}");
        toUsers = toUsers.Where(x => !notExistToUsers.Contains(x.Name)).ToList();

        //save file
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"Data/Email/{Guid.NewGuid()}.txt");
        buffer.ToArray().SaveToFile(path);

        var email = new Models.Email(message.Subject, message.MessageId, path) { ReferenceMessageIds = message.MessageId };
        EmailSerivce.Save(email);

        var fromDetail = new EmailDetail(email.Id, fromUser.Id, 1);
        EmailDetailSerivce.Save(fromDetail);

        toUsers.ForEach(x =>
        {
            var toDetail = new EmailDetail(email.Id, x.Id, 2);
            EmailDetailSerivce.Save(toDetail);
        });

        return Task.FromResult(SmtpResponse.Ok);
    }
}

