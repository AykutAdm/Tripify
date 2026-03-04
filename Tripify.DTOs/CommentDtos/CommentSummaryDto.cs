using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripify.DTOs.CommentDtos
{
    public class CommentSummaryDto
    {
        public string Headline { get; set; }
        public string CommentDetail { get; set; }
        public int Score { get; set; }
    }
}
