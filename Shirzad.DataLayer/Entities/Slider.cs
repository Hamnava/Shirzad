
using System.ComponentModel.DataAnnotations;


namespace Shirzad.DataLayer.Entities
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        public string SliderTitle { get; set; }
        public string sliderAlt { get; set; }
        public int SliderSort { get; set; }
        public string SliderImg { get; set; }
        public bool IsActive { get; set; }
    }
}
