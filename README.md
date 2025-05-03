# 🧠 CodeMate — Microservices Mentorship Platform

CodeMate, yazılımcıların birbirleriyle bilgi paylaşmasını, mentorluk almasını ve teknik konularda destek bulmasını sağlayan **mikroservis mimarili** bir platformdur.  
Bu projede tam anlamıyla **event-driven architecture**, **JWT tabanlı authentication**, **container-based deployment**, **real-time communication** ve **skill-based matching** gibi gelişmiş sistemler hayata geçirilmiştir.

---

## 🚀 Özellikler

- ✅ Kullanıcı kayıt / giriş / token işlemleri (`AuthService`)
- ✅ Kullanıcı profili & yetenek yönetimi (`UserProfileService`)
- ✅ Kurs oluşturma ve listeleme (`CourseService`)
- ✅ Teklif verme & onay sistemi (`OfferService`)
- ✅ Sipariş oluşturma (`OrderService`)
- ✅ Ödeme sonrası bildirim sistemi (`PaymentService + NotificationService`)
- ✅ Otomatik mentor-mentee eşleştirme (`MatchingService`)
- ✅ Gerçek zamanlı mesajlaşma (`ChatService + SignalR`)
- ✅ Kullanıcı değerlendirme sistemi (`ReviewService`)
- ✅ MVC tabanlı frontend (`CodeMate.Web`)

---

## 🛠️ Kullanılan Teknolojiler

| Katman        | Teknoloji               |
|---------------|--------------------------|
| Backend       | ASP.NET Core 8           |
| API Messaging | MassTransit + RabbitMQ   |
| Gerçek Zamanlı| SignalR                  |
| Veritabanı    | PostgreSQL               |
| Kimlik Doğrulama | JWT (Bearer Token)   |
| Bildirim      | SMTP Mail                |
| Cache         | Redis                    |
| Konteyner     | Docker + Docker Compose  |

---

## 📦 Mikroservis Mimarisi

Tüm servisler birbirinden bağımsız çalışabilir şekilde inşa edilmiştir.  
Her biri ayrı bir veritabanı ve sorumluluk alanına sahiptir.


![Image](https://github.com/user-attachments/assets/6ef00a0e-34fc-4292-93e2-6a6aa2b0ac5b)
![Image](https://github.com/user-attachments/assets/23f2fff7-ba13-4917-a39f-47a4f5a6df13)
![Image](https://github.com/user-attachments/assets/91590138-1a6d-4ef5-b083-5db2d3c420de)
![Image](https://github.com/user-attachments/assets/e12595e1-5e4c-41eb-bdd2-edd4e1e4dfac)
![Image](https://github.com/user-attachments/assets/a63127ff-f33c-4e98-8b87-a8a513a94b78)
![Image](https://github.com/user-attachments/assets/0721f7d5-1347-4145-80d3-81a2b625626b)
![Image](https://github.com/user-attachments/assets/60b1129a-b80e-4430-aab7-29d9fc935f7c)
![Image](https://github.com/user-attachments/assets/ea5ac90b-09f2-4e34-b891-dbc5850a3067)
![Image](https://github.com/user-attachments/assets/c2cc2236-1ace-4471-aa4a-76d4e7e49f98)
![Image](https://github.com/user-attachments/assets/17c4988d-1c68-4112-a633-751ffbf98df5)
