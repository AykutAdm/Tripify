using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripify.DTOs.GuideDtos
{
    public class GetGuideByIdDto
    {
        public string GuideId { get; set; }
        public string GuideNameSurname { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string TourId { get; set; }
    }
}
