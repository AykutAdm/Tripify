using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripify.DTOs.MailDtos
{
    public class MailRequestDto
    {
        public string BookingId { get; set; }      // Hangi booking için
        public string Status { get; set; }         // Approved / Rejected
        public string ReceiverEmail { get; set; }  // Kullanıcının mail adresi
        public string Subject { get; set; }        // Mail konusu
        public string MessageDetail { get; set; }
    }
}
