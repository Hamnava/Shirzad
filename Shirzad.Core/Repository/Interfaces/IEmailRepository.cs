using Shirzad.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.Core.Repository.Interfaces
{
    public interface IEmailRepository
    {
        Task<bool> IsEmailExisted(string email);
        Task SendEmailAsync(string email, string subject, string message, string username, string password, string senderEmail);
        
    }
}
