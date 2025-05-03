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
- 

---

## 🛠️ Kullanılan Teknolojiler

| Katman        | Teknoloji             |
|--------------|------------------------|
| Backend      | ASP.NET Core 8         |
| API İletişimi| MassTransit + RabbitMQ |
| Gerçek Zamanlı| SignalR               |
| Veritabanı   | PostgreSQL             |
| Mesajlaşma   | RabbitMQ               |
| Container    | Docker + Docker Compose|
| Authentication | JWT (Bearer Token)  |
| Bildirim     | SMTP Mail (NotificationService) |


---

## 📦 Mikroservis Mimarisi

Tüm servisler kendi veritabanına ve sorumluluğuna sahip olacak şekilde izole edilmiştir.

```bash
.
├── AuthService
├── UserProfileService
├── CourseService
├── OfferService
├── OrderService
├── PaymentService
├── NotificationService
├── MatchingService
├── ChatService
├── ReviewService
└── CodeMate.Web (Frontend)

![Image](https://github.com/user-attachments/assets/e9da7207-c583-4830-88cb-13ad02fc4f7a)
![Image](https://github.com/user-attachments/assets/eeb94396-05d0-4fc9-b895-79093ac2c317)
![Image](https://github.com/user-attachments/assets/891163bb-d5be-4ed0-88d2-d45d7061bb11)
![Image](https://github.com/user-attachments/assets/093bf35e-4dcd-46ed-b8e8-b8c20e45f767)
![Image](https://github.com/user-attachments/assets/c24013c7-8d65-451d-a471-4c266873015e)
![Image](https://github.com/user-attachments/assets/2763c801-e44f-42ae-8816-7001350f0c86)
![Image](https://github.com/user-attachments/assets/1dbea1ea-b704-40ce-a603-cd99739f12e6)
![Image](https://github.com/user-attachments/assets/7cd1b55e-11c2-490f-ab8e-37335b811139)
![Image](https://github.com/user-attachments/assets/455736c7-c00e-4de3-a341-f505b826f45b)
![Image](https://github.com/user-attachments/assets/7592c4ff-0d1f-463e-a35d-01603256a6ea)

![Image](https://github.com/user-attachments/assets/e23a9fcb-5181-4645-9f2d-0a5935e3057c)
