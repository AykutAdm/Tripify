using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripify.DTOs.TourDtos
{
    public class ResultLast4TourDto
    {
        public string TourId { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public DateTime TourDate { get; set; }
        public string DayNight { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
    }
}
