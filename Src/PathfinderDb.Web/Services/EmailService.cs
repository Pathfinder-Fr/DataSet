// -----------------------------------------------------------------------
// <copyright file="EmailService.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Services
{
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            using (var mail = new MailMessage())
            {
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.To.Add(message.Destination);

                await this.SendAsync(mail);
            }
        }

        public async Task SendAsync(MailMessage message)
        {
            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}