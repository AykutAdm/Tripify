namespace Tripify.WebUI.Dtos.MailDtos
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
