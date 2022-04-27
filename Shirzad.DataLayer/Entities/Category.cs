
using System.ComponentModel.DataAnnotations;

namespace Shirzad.DataLayer.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

       
    }
}
