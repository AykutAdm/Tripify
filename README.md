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

### 🏠 Kullanıcı Arayüzü

<div align="center">
  <img src="TripifyImages/AnaSayfa-01.png" alt="Admin Paneli-1" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-02.png" alt="Admin Paneli-2" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-03.png" alt="Admin Paneli-3" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-04.png" alt="Admin Paneli-4" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-05.png" alt="Admin Paneli-5" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-06.png" alt="Admin Paneli-6" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-07.png" alt="Admin Paneli-7" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-08.png" alt="Admin Paneli-8" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-09.png" alt="Admin Paneli-9" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-10.png" alt="Admin Paneli-10" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-11.png" alt="Admin Paneli-11" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-12.png" alt="Admin Paneli-12" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-13.png" alt="Admin Paneli-13" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-14.png" alt="Admin Paneli-14" width="800" style="margin: 10px;">
  <img src="TripifyImages/AnaSayfa-15.png" alt="Admin Paneli-15" width="800" style="margin: 10px;">
</div>


### 🔐 Admin Paneli

<div align="center">
  <img src="TripifyImages/AdminDashboard-01.png" alt="Admin Paneli-1" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-02.png" alt="Admin Paneli-2" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-09.png" alt="Admin Paneli-9" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-10.png" alt="Admin Paneli-10" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-03.png" alt="Admin Paneli-3" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-04.png" alt="Admin Paneli-4" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-05.png" alt="Admin Paneli-5" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-06.png" alt="Admin Paneli-6" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-07-OpenAI.png" alt="Admin Paneli-7" width="800" style="margin: 10px;">
  <img src="TripifyImages/AdminDashboard-08.png" alt="Admin Paneli-8" width="800" style="margin: 10px;">
</div>
