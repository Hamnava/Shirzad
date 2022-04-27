
using System.ComponentModel.DataAnnotations;


namespace Shirzad.DataLayer.Entities
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; } = DateTime.UtcNow;
    }
}
