using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.DataLayer.Entities
{
    public class EmailRegister
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DateRegister { get; set; } = DateTime.UtcNow;
    }
}
