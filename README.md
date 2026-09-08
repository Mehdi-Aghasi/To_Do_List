<div align="center">

# 📝 To-Do List API

### یک RESTful API حرفه‌ای برای مدیریت وظایف با معماری Clean Architecture

**ساخته‌شده با ASP.NET Core 8.0 • C# 12 • CQRS • JWT Authentication**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![JWT](https://img.shields.io/badge/JWT-Auth-black?style=for-the-badge&logo=jsonwebtokens)](https://jwt.io/)
[![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)](#-license)

[معرفی پروژه](#-این-پروژه-دقیقاً-چیست) •
[معماری](#️-معماری-پروژه-4-لایه) •
[نصب و اجرا](#-اجرای-پروژه) •
[مستندات API](#-مستندات-api) •
[نقشه راه](#-نقشه-توسعه-پروژه)

</div>

---

## 🎯 این پروژه دقیقاً چیست؟

**To-Do List API** یک Backend سرویس برای مدیریت وظایف روزانه است که با تمرکز بر **معماری تمیز، مقیاس‌پذیر و قابل نگهداری** طراحی شده.

هدف این پروژه فقط ساختن یک CRUD ساده برای `Task` نیست؛ بلکه پیاده‌سازی صحیح مجموعه‌ای از مفاهیم پیشرفته Backend Development در قالب یک پروژه واقعی است:

- 🏛️ **Clean Architecture** با تفکیک کامل لایه‌ها
- ⚡ **CQRS Pattern** برای جداسازی عملیات Read و Write
- 🔐 **JWT Authentication** مبتنی بر ASP.NET Core Identity
- 🧩 **Feature-Based Organization** به‌جای ساختار سنتی Layer-Based
- 🗃️ **Repository Pattern** برای انتزاع دسترسی به داده
- 📦 **DTO Pattern** برای جداسازی Domain از Contract عمومی API
- ✅ **FluentValidation Pipeline** برای اعتبارسنجی خودکار Requestها
- 🧹 **Soft Delete** با Global Query Filter در سطح EF Core

کاربر می‌تواند:

| | قابلیت |
|---|---|
| 👤 | حساب کاربری ایجاد کند (Register) |
| 🔐 | وارد سیستم شود (Login) |
| 🔑 | با JWT Token به Endpointهای محافظت‌شده دسترسی پیدا کند |
| ➕ | وظیفه جدید ایجاد کند |
| 📋 | لیست وظایف خودش را دریافت کند |
| 🔎 | یک وظیفه مشخص را بر اساس شناسه دریافت کند |
| ✏️ | وظایف را ویرایش کند |
| 🗑️ | وظایف را به‌صورت Soft Delete حذف کند |

---

## 🧠 ایده اصلی پروژه

```text
Register
   ↓
Login
   ↓
JWT Token
   ↓
Access Protected APIs
   ↓
Create / Read / Update / Delete Tasks
```

هر کاربر تنها به وظایف مربوط به خودش دسترسی دارد؛ مالکیت هر Task از طریق `UserId` استخراج‌شده از توکن JWT کنترل می‌شود.

---

## 🛠️ Technology Stack

| Technology | نسخه | کاربرد |
|---|---|---|
| **C#** | 12.0 | زبان اصلی پروژه |
| **ASP.NET Core** | 8.0 | فریم‌ورک Web API |
| **Entity Framework Core** | 8.0 | ORM و ارتباط با دیتابیس |
| **SQL Server** | — | دیتابیس اصلی |
| **MediatR** | — | پیاده‌سازی الگوی CQRS |
| **FluentValidation** | — | اعتبارسنجی ورودی‌ها به‌صورت Pipeline |
| **ASP.NET Core Identity** | — | مدیریت کاربران و احراز هویت |
| **JWT Bearer** | — | Authentication / Authorization |
| **AutoMapper** | — | تبدیل Entity به DTO و برعکس |
| **Swagger / OpenAPI** | 3.0 | مستندسازی و تست تعاملی API |

---

## 🏗️ معماری پروژه (4 لایه)

پروژه بر پایه **Clean Architecture** و با هدف جداسازی کامل **Business Logic** از سایر بخش‌های سیستم طراحی شده است:

```text
To_Do_List
│
├── To_Do_List.Domain              🧩 هسته مرکزی، بدون وابستگی به لایه‌های دیگر
│   ├── Entities
│   │   ├── ToDoTask               (Aggregate Root با Soft Delete)
│   │   └── ApplicationUser        (گسترش IdentityUser)
│   └── Interfaces
│       └── ITaskRepository
│
├── To_Do_List.Application         ⚡ منطق برنامه، سازمان‌دهی Feature-Based
│   ├── Common
│   │   ├── Behaviors              (ValidationBehavior - Pipeline)
│   │   ├── Dtos
│   │   └── JwtTokenGenerator
│   │
│   ├── Features
│   │   ├── Auth
│   │   │   └── Commands
│   │   │       ├── Register
│   │   │       └── Login
│   │   │
│   │   └── Tasks
│   │       ├── Commands
│   │       │   ├── CreateTask
│   │       │   ├── UpdateTask
│   │       │   └── DeleteTask
│   │       └── Queries
│   │           ├── GetAllTasks
│   │           └── GetByIdTask
│   │
│   └── DependencyInjection
│
├── To_Do_List.Infrastructure       🗄️ پیاده‌سازی دسترسی به داده
│   ├── ApplicationDbContext        (EF Core + Identity)
│   ├── TaskRepository              (پیاده‌سازی ITaskRepository)
│   └── Global Query Filter         (Soft Delete → IsDeleted)
│
└── To_Do_List.WebApi                🌐 لایه ارائه (Presentation)
    ├── Controllers
    │   ├── AuthController          (Register, Login)
    │   └── TasksController         (CRUD, [Authorize])
    ├── Swagger UI (با پشتیبانی JWT)
    └── Program.cs                  (پیکربندی کامل DI)
```

> ساختار دقیق فولدرها ممکن است با توسعه پروژه تغییر کند، اما ایده اصلی ثابت است: **هر Feature و هر مسئولیت، در جای مشخص خودش قرار می‌گیرد.**

---

## 🧩 چرا Feature-Based؟

به‌جای اینکه تمام Controllerها، Serviceها و Handlerها در فولدرهای بزرگ و جدا از هم (مثل یک پوشه‌ی عظیم `Services` یا `Controllers`) قرار بگیرند، تمام کدهای مرتبط با یک قابلیت در کنار هم نگه داشته می‌شوند:

```text
Features
└── Tasks
    ├── Commands
    │   ├── CreateTask
    │   ├── UpdateTask
    │   └── DeleteTask
    │
    └── Queries
        ├── GetAllTasks
        └── GetByIdTask
```

نتیجه؟ وقتی Developer بخواهد قابلیتی مربوط به Task را تغییر دهد، تقریباً تمام بخش‌های مرتبط را در یک محدوده‌ی مشخص پیدا می‌کند — بدون پرش بین چند لایه و چند پوشه‌ی نامرتبط.

---

## ⚡ CQRS

در لایه Application از الگوی **CQRS (Command Query Responsibility Segregation)** برای تفکیک عملیات خواندن و تغییر داده استفاده شده است.

```text
                    Application
                         │
             ┌───────────┴───────────┐
             │                       │
         Commands                  Queries
     (تغییر وضعیت سیستم)        (فقط خواندن داده)
             │                       │
     ┌───────┼───────┐          ┌────┴────┐
     │       │       │          │         │
   Create  Update  Delete      GetAll   GetById
```

| نوع | مثال | مسئولیت |
|---|---|---|
| **Command** | `CreateTask`, `UpdateTask`, `DeleteTask`, `Register`, `Login` | تغییر وضعیت سیستم |
| **Query** | `GetAllTasks`, `GetByIdTask` | فقط دریافت اطلاعات |

پیاده‌سازی این الگو با کتابخانه **MediatR** انجام شده است.

---

## 🧪 Validation

اعتبارسنجی ورودی‌ها به‌صورت مستقل و در کنار هر Feature قرار گرفته و از طریق یک **Pipeline Behavior** به‌صورت خودکار قبل از اجرای هر Handler اجرا می‌شود:

```text
CreateTask
├── CreateTaskCommand
├── CreateTaskHandler
└── CreateTaskValidator
```

این جداسازی باعث می‌شود Business Logic از منطق اعتبارسنجی مستقل بماند و Handler مجبور نباشد ورودی‌ها را دستی بررسی کند — تمام این کار توسط `ValidationBehavior` و **FluentValidation** پیش از رسیدن Request به Handler انجام می‌شود.

---

## 🔄 مسیر حرکت یک Request

برای مثال، هنگام ایجاد یک Task:

```text
HTTP Request
     │
     ▼
   WebApi (Controller)
     │
     ▼
CreateTaskCommand
     │
     ▼
ValidationBehavior (FluentValidation)
     │
     ▼
CreateTaskHandler
     │
     ▼
Application / Domain Logic
     │
     ▼
Infrastructure (Repository + DbContext)
     │
     ▼
Database
```

و مسیر بازگشت پاسخ:

```text
Database → Infrastructure → Handler → DTO (AutoMapper) → HTTP Response
```

این تفکیک تضمین می‌کند هر لایه فقط مسئولیت خودش را انجام دهد و هیچ لایه‌ای به جزئیات پیاده‌سازی لایه‌ی دیگر وابسته نباشد.

---

## 🔐 احراز هویت و امنیت

| ویژگی | توضیح |
|---|---|
| **ثبت‌نام** | مبتنی بر ASP.NET Core Identity |
| **Hash پسورد** | خودکار، با الگوریتم PBKDF2 |
| **تولید توکن** | JWT با الگوریتم **HMAC-SHA256** |
| **Claimهای توکن** | `NameIdentifier` (UserId)، `Email`، `Name` |
| **مدت اعتبار توکن** | ۷ روز (قابل تنظیم در Configuration) |
| **مالکیت Task** | استخراج `UserId` از JWT برای فیلتر کردن وظایف هر کاربر |
| **محافظت از Endpoint** | با اتریبیوت `[Authorize]` روی Controllerها |

```text
User
 │
 ├── Register ─────→ ایجاد حساب کاربری (Identity)
 │
 └── Login
        ↓
   اعتبارسنجی اطلاعات ورود
        ↓
   تولید JWT (HMAC-SHA256)
        ↓
      Token
        ↓
 Authorization Header
        ↓
 دسترسی به Endpointهای محافظت‌شده
```

نمونه Header مورد نیاز برای درخواست‌های محافظت‌شده:

```http
Authorization: Bearer <token>
```

---

## 📋 مدیریت تسک‌ها (Full CRUD)

| عملیات | HTTP Method | Endpoint | توضیح |
|---|---|---|---|
| ➕ Create | `POST` | `/api/Tasks` | ایجاد تسک جدید (`UserId` از JWT استخراج می‌شود) |
| 📋 Get All | `GET` | `/api/Tasks` | دریافت لیست تسک‌های کاربر فعلی |
| 🔎 Get By Id | `GET` | `/api/Tasks/{id}` | دریافت یک تسک مشخص |
| ✏️ Update | `PUT` | `/api/Tasks/{id}` | ویرایش تسک |
| 🗑️ Delete | `DELETE` | `/api/Tasks/{id}` | حذف منطقی تسک (Soft Delete) |

> حذف تسک به‌صورت **Soft Delete** انجام می‌شود؛ رکورد از دیتابیس پاک نمی‌شود، بلکه فیلد `IsDeleted` مقداردهی شده و از طریق **Global Query Filter** در EF Core، به‌طور خودکار از تمام کوئری‌های بعدی حذف می‌شود.

---

## 📡 مستندات API

### 🔐 Authentication

#### Register
`POST /api/Auth/register`

**Request:**
```json
{
  "firstName": "مهدی",
  "lastName": "آقاسی",
  "email": "mehdi@example.com",
  "password": "123456"
}
```

#### Login
`POST /api/Auth/login`

**Request:**
```json
{
  "email": "mehdi@example.com",
  "password": "123456"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": "7.00:00:00"
}
```

### 📋 Tasks

#### Create Task
`POST /api/Tasks` — نیازمند هدر `Authorization: Bearer <token>`

**Request:**
```json
{
  "title": "خرید مواد غذایی",
  "description": "خرید نان، شیر و تخم‌مرغ",
  "isCompleted": false
}
```

#### Get All Tasks
`GET /api/Tasks`

#### Get Task By Id
`GET /api/Tasks/{id}`

#### Update Task
`PUT /api/Tasks/{id}`

#### Delete Task
`DELETE /api/Tasks/{id}`

برای مشاهده و تست تعاملی تمام Endpointها می‌توانید از ابزارهای زیر استفاده کنید:

- 🔎 **Swagger UI** (پس از اجرای پروژه، به‌صورت پیش‌فرض در دسترس است)
- 📮 Postman
- 🧪 Insomnia
- 💻 curl

---

## 🚀 اجرای پروژه

### پیش‌نیازها

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (یا LocalDB)
- یک IDE مثل Visual Studio 2022 / VS Code / Rider

### مراحل نصب

**۱. Clone کردن Repository**
```bash
git clone https://github.com/Mehdi-Aghasi/To_Do_List.git
cd To_Do_List
```

**۲. تنظیم Configuration**

فایل `appsettings.json` را در پروژه `To_Do_List.WebApi` بررسی و مطابق محیط خودتان تنظیم کنید (به بخش [Configuration](#️-configuration) مراجعه کنید).

**۳. Restore کردن پکیج‌ها**
```bash
dotnet restore
```

**۴. اعمال Migrationها**
```bash
dotnet ef database update --project To_Do_List.Infrastructure --startup-project To_Do_List.WebApi
```

**۵. اجرای پروژه**
```bash
dotnet run --project To_Do_List.WebApi
```

پس از اجرا، Swagger UI معمولاً در آدرس زیر در دسترس خواهد بود:

```text
https://localhost:{PORT}/swagger
```

---

## ⚙️ Configuration

قبل از اجرای پروژه، Connection String دیتابیس و تنظیمات JWT را متناسب با محیط خودتان در `appsettings.json` تنظیم کنید:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ToDoListDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_MIN_32_CHARACTERS",
    "Issuer": "ToDoListApi",
    "Audience": "ToDoListApiUsers",
    "ExpireDays": 7
  }
}
```

> ⚠️ مقادیر واقعی Secret و Connection String هرگز نباید داخل یک Repository عمومی قرار بگیرند. برای محیط Production از **User Secrets**، **Environment Variables** یا سرویس‌های مدیریت Secret استفاده کنید.

---

## 🧱 اصول طراحی

| اصل | توضیح |
|---|---|
| **Separation of Concerns** | هر لایه و هر بخش، مسئولیت مشخص و منحصربه‌فرد خودش را دارد |
| **Single Responsibility** | Classها و Handlerها تا حد امکان مسئول یک وظیفه‌ی واحد هستند |
| **Dependency Injection** | تمام وابستگی‌ها از طریق DI Container مدیریت می‌شوند |
| **DTO Pattern** | Entityهای دیتابیس مستقیماً به‌عنوان Contract عمومی API استفاده نمی‌شوند |
| **Validation Pipeline** | اعتبارسنجی Requestها پیش از رسیدن به Handler، به‌صورت خودکار انجام می‌شود |
| **CQRS** | عملیات Read و Write به‌طور کامل از یکدیگر تفکیک شده‌اند |
| **Feature-Based Organization** | کدها بر اساس قابلیت (Feature)، نه نوع فایل، سازمان‌دهی شده‌اند |
| **Repository Pattern** | دسترسی به داده پشت یک Interface انتزاعی (`ITaskRepository`) قرار دارد |

---

## 📈 نقشه توسعه پروژه

این پروژه پتانسیل تبدیل‌شدن به یک **Task Management System** کامل‌تر را دارد. قابلیت‌های برنامه‌ریزی‌شده برای آینده:

- [ ] Refresh Token
- [ ] Role-Based Authorization
- [ ] Task Priority
- [ ] Task Category / Tags
- [ ] Due Date & Reminders
- [ ] Search & Filtering
- [ ] Pagination
- [ ] Sorting
- [ ] Task History / Audit Log
- [ ] Unit Tests
- [ ] Integration Tests
- [ ] Docker & Docker Compose
- [ ] CI/CD Pipeline (GitHub Actions)
- [ ] Structured Logging (Serilog)
- [ ] Global Exception Handling Middleware
- [ ] API Versioning
- [ ] Rate Limiting

---

## 🎓 هدف پروژه

این پروژه فراتر از یک To-Do API ساده، بستری برای تمرین و پیاده‌سازی صحیح مفاهیم کلیدی Backend Development است:

```text
C# 12
 │
 ├── ASP.NET Core 8.0
 ├── REST API Design
 ├── JWT Authentication
 ├── CQRS (MediatR)
 ├── FluentValidation
 ├── Repository Pattern
 ├── DTO & AutoMapper
 ├── Soft Delete Strategy
 └── Clean / Layered Architecture
```

هدف نهایی، نوشتن کدی نیست که فقط **کار کند**؛ بلکه ساخت کدی است که:

> **قابل فهم، قابل نگهداری، قابل تست و قابل توسعه باشد.**

---

## 👨‍💻 Developer

Developed with ❤️ by **Mehdi Aghasi**

[![GitHub](https://img.shields.io/badge/GitHub-Mehdi--Aghasi-181717?style=for-the-badge&logo=github)](https://github.com/Mehdi-Aghasi)

---

## ⭐ حمایت از پروژه

اگر این پروژه برای یادگیری معماری Backend، ASP.NET Core یا طراحی API برایتان مفید بود، خوشحال می‌شوم یک ⭐ روی Repository بگذارید.

هر Star، انگیزه‌ای برای یک `git commit` جدید و توسعه‌ی بیشتر پروژه است. 🚀

<div align="center">

**Built with ❤️ using C# & ASP.NET Core**

</div>
