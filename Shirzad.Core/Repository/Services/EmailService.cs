using Microsoft.EntityFrameworkCore;
using Shirzad.Core.Repository.Interfaces;
using Shirzad.DataLayer.Context;
using Shirzad.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.Repository.Services
{
    public class EmailService : IEmailRepository
    {
        private readonly ApplicationContext _context;
        public EmailService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailExisted(string email)
        {
            var emaailadd = await _context.EmailRegisters.FirstOrDefaultAsync(x => x.Email == email);
            if (emaailadd == null)
            {
                return false;
            }
            return true;
        }

        public async Task<ApplicationUser> GetMyUser()
        {
            return await _context.Users.SingleOrDefaultAsync();
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {

            var user = await GetMyUser();
            using (var Client = new SmtpClient())
            {
                var Credential = new NetworkCredential
                {
                    //UserName Example : If your email is test@gmail.com yourUserName is test
                    UserName = user.UserName,
                    Password = user.EmailPassword

                };
                Client.Credentials = Credential;
                Client.Host = "smtp.gmail.com";
                Client.Port = 587; // or 25  -- 587 -- 465 For Send Email
                Client.EnableSsl = true;
                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(user.Email, "Shirzad Store");
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true; //contains html tag
                    emailMessage.Body = message;

                    Client.Send(emailMessage);
                };
                await Task.CompletedTask;
            }
            /////////////////////////////////////////////////////////////////
        }
    }}
