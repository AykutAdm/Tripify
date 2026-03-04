## ✈️ Tripify

---

## 📋 Proje Hakkında
**Tripify**, tur ve seyahat sektörüne yönelik, yapay zeka destekli bir gezi ve rezervasyon web uygulamasıdır.  
Kullanıcılar; tur listesini inceleyebilir, detaylı tur sayfalarına göz atabilir, rezervasyon yapabilir ve turlar hakkında yorum bırakabilir.

Uygulama, tur detaylarında yapay zeka ile oluşturulan **“Neler Bekleyebilirsiniz”** günlük planları, **yorum özetleri** ve **rezervasyon durumu e-postaları** gibi özelliklerle kullanıcı deneyimini zenginleştirir.

Admin paneli üzerinden:
- Turlar, rezervasyonlar, rehberler, kategoriler ve içerikler yönetilebilir
- Rezervasyonlar onaylanabilir veya reddedilebilir
- Rezervasyon durumuna göre müşterilere otomatik e-posta gönderilebilir

Proje; **ASP.NET Core MVC tabanlı Web UI** ile **REST mimarisine sahip Web API**’nin ayrı katmanlar halinde çalıştığı bir mimariye sahiptir.  
Tüm veri erişimi ve yapay zeka işlemleri Web API üzerinden gerçekleştirilir.

---

## 🛠️ Kullanılan Teknolojiler

### 🔧 Backend (Web API)
- ASP.NET Core 9.0 Web API  
- Swagger

### 🎨 Frontend (Web UI)
- ASP.NET Core MVC 9.0

### 🧩 Veri Yönetimi
- DTO katmanı
- AutoMapper

### 🗄️ Veritabanı
- MongoDB

### 📄 PDF Oluşturma
- Select.HtmlToPdf.NetCore  
  (Rezervasyon bilgilerinin PDF formatında dışa aktarılması)

### 🌍 Çoklu Dil Desteği
- JSON tabanlı yerelleştirme  
  (tr, en, de, fr, es)

---

## 🤖 Yapay Zeka Entegrasyonları

### 🔹 OpenAI – Chat Completions API

**🗺️ Neler Bekleyebilirsiniz (Tur Detay Sayfası)**  
Tur başlığı, açıklaması, lokasyonu ve süresine göre 4 günlük detaylı bir gezi planı oluşturulur.  
Kullanıcılar, her günün etkinliklerini önceden görebilir.

**💬 Yorum Özeti**  
Tura ait kullanıcı yorumları analiz edilerek 1–2 cümlelik özet oluşturulur.  
Genel değerlendirme ve öne çıkan noktalar tur detay sayfasında gösterilir.

**📨 Rezervasyon Durumu E-postası**  
Admin tarafından verilen onay veya red kararına göre, rezervasyon bilgileriyle uyumlu Türkçe e-posta metni yapay zeka tarafından üretilir.  
Oluşturulan e-posta, **MailKit** kullanılarak müşteriye gönderilir.

---

## 🖼️ Ekran Görüntüleri

### 🏠 Ana Sayfa

<div align="center">
  <img src="Images/AnaSayfa-1.png" alt="Admin Paneli-1" width="800" style="margin: 10px;">
  <img src="Images/AnaSayfa-2.png" alt="Admin Paneli-2" width="800" style="margin: 10px;">
  <img src="Images/Register-1.png" alt="Admin Paneli-3" width="800" style="margin: 10px;">
  <img src="Images/KullanimSartlari.png" alt="Admin Paneli-4" width="800" style="margin: 10px;">
  <img src="Images/KodDogrulama.png" alt="Admin Paneli-5" width="800" style="margin: 10px;">
  <img src="Images/Login-1.png" alt="Admin Paneli-6" width="800" style="margin: 10px;">
  <img src="Images/2FaktorDogrulama.png" alt="Admin Paneli-7" width="800" style="margin: 10px;">
</div>


### 🔐 Admin Paneli

<div align="center">
  <img src="Images/Dashboard-1.png" alt="Admin Paneli-1" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-ProfileAnalysisWithOpenAI.png" alt="Admin Paneli-9" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-2.png" alt="Admin Paneli-2" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-3.png" alt="Admin Paneli-3" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-MailDetailWithGeminiAI.png" alt="Admin Paneli-4" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-RealMail.png" alt="Admin Paneli-5" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-4.png" alt="Admin Paneli-6" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-5.png" alt="Admin Paneli-7" width="800" style="margin: 10px;">
  <img src="Images/Dashboard-6.png" alt="Admin Paneli-8" width="800" style="margin: 10px;">
</div>
