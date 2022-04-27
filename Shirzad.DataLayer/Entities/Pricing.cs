using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shirzad.DataLayer.Entities
{
    public class Pricing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
    }
}
