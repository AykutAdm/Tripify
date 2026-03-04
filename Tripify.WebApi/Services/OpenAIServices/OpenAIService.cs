using OpenAI.Chat;
using System.Text;
using Tripify.DTOs.BookingDtos;
using Tripify.DTOs.CommentDtos;

namespace Tripify.WebApi.Services.OpenAIServices
{
    public class OpenAIService : IOpenAIService
    {
        private readonly string _apiKey;
        private readonly ChatClient _chatClient;

        public OpenAIService(IConfiguration configuration)
        {
            _apiKey = configuration["OpenAI:ApiKey"] ?? throw new Exception("OpenAI API Key bulunamadı!");
            _chatClient = new ChatClient("gpt-4o-mini", _apiKey);
        }

        public async Task<string> GenerateWhatToExpectAsync(string tourTitle, string tourDescription, string location, string duration)
        {
            var prompt = $@"
Bir tur için 'What to Expect' (Neler Bekleyebilirsiniz) bölümü oluştur.

Tur Bilgileri:
- Başlık: {tourTitle}
- Açıklama: {tourDescription}
- Lokasyon: {location}
- Süre: {duration}

Lütfen aşağıdaki formatta, 4 günlük bir plan oluştur. Her gün için başlık ve açıklama yaz. 
Format şu şekilde olmalı (JSON formatında):

[
  {{
    ""day"": 1,
    ""title"": ""Gün 1 - Başlık"",
    ""description"": ""O günün detaylı açıklaması""
  }},
  {{
    ""day"": 2,
    ""title"": ""Gün 2 - Başlık"",
    ""description"": ""O günün detaylı açıklaması""
  }},
  {{
    ""day"": 3,
    ""title"": ""Gün 3 - Başlık"",
    ""description"": ""O günün detaylı açıklaması""
  }},
  {{
    ""day"": 4,
    ""title"": ""Gün 4 - Başlık"",
    ""description"": ""O günün detaylı açıklaması""
  }}
]

SADECE JSON formatında cevap ver, başka hiçbir açıklama ekleme.
";

            ChatCompletion completion = await _chatClient.CompleteChatAsync(prompt);

            return completion.Content[0].Text;
        }

        public async Task<string> GenerateCommentSummaryAsync(List<CommentSummaryDto> comments)
        {
            if (comments == null || !comments.Any())
            {
                return "Henüz yorum bulunmamaktadır.";
            }

            var commentTexts = new StringBuilder();
            foreach (var comment in comments)
            {
                commentTexts.AppendLine($"- [{comment.Score}/5 Yıldız] {comment.Headline}: {comment.CommentDetail}");
            }

            var prompt = $@"
Aşağıdaki tur yorumlarını analiz et ve SADECE 1-2 cümlelik kısa bir özet oluştur. 
Genel duyguyu ve öne çıkan noktaları vurgula. Türkçe, samimi ve içten bir dil kullan.

Yorumlar:
{commentTexts}

SADECE özet metni yaz, başka hiçbir şey ekleme. Maksimum 2 cümle olsun.
";

            ChatCompletion completion = await _chatClient.CompleteChatAsync(prompt);

            return completion.Content[0].Text;
        }

        public async Task<string> GenerateBookingStatusEmailAsync(GetBookingByIdDto booking, string status, string? tourTitle)
        {
            string durum;

            if (status == "Approved")
            {
                durum = "onaylandı";
            }
            else if (status == "Rejected")
            {
                durum = "reddedildi";
            }
            else
            {
                durum = "güncellendi";
            }

            var prompt = $@"
Aşağıdaki rezervasyon için kullanıcının anlayacağı, samimi ve profesyonel bir TÜRKÇE e-posta metni yaz.

Şirket adı: Tripify
Rezervasyon durumu: {durum}

Rezervasyon bilgileri:
- Müşteri: {booking.FirstName} {booking.LastName}
- E-posta: {booking.Email}
- Tur Başlığı: {tourTitle ?? booking.Title}
- Ülke / Şehir: {booking.Country} / {booking.City}
- Giriş: {booking.CheckInDate:dd.MM.yyyy}
- Çıkış: {booking.CheckOutDate:dd.MM.yyyy}
- Kişi Sayısı: {booking.NumberOfPeople}
- Toplam Tutar: {booking.TotalPrice} TL

İki farklı senaryo için davran:
1) Eğer durum 'onaylandı' ise:
   - Rezervasyonun başarıyla onaylandığını söyle.
   - Tarihleri ve temel bilgileri kısaca tekrar et.
   - Tripify ekibi olarak iyi yolculuklar dile.

2) Eğer durum 'reddedildi' ise:
   - Nazikçe, uygun bir sebep uydurarak (kontenjan doluluğu, tarih uyuşmazlığı vb.) özür dile.
   - Mümkünse alternatif tarih/tur önermeyi ima et.
   - Tripify olarak tekrar yardımcı olmak istediğini belirt.

GENEL KURALLAR:
- Maili sanki Tripify müşteri temsilcisi yazmış gibi yaz.
- Maksimum 3–4 paragraf olsun.
- Hitapta kullanıcının adını kullan (mesela 'Merhaba {booking.FirstName} Bey/Hanım' değil, sadece 'Merhaba {booking.FirstName}').
- SADECE mail gövdesini döndür, başka açıklama ekleme.
";

            ChatCompletion completion = await _chatClient.CompleteChatAsync(prompt);
            return completion.Content[0].Text;
        }
    }
}
