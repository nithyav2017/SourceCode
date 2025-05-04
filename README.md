**Project Overview**
  •	Platforms: Android, iOS, Web
  •	Tech Stack:
      o	Frontend (Web View / Hybrid App): HTML, CSS, JavaScript
      o	Backend API: ASP.NET Core Web API
      o	Mobile App:   MAUI (recommended for .NET 8.0)
      o	Database: SQLite (local storage when offline) + SQL Server (backend)
      o	Authentication: Secure PIN-based login
      o	Data Sync: REST API with offline sync support
      o	Security: HTTPS, AES encryption for local DB, JWT tokens for API
**System Architecture**
![image](https://github.com/user-attachments/assets/66da50f5-e83b-433f-b07f-a5fcd7a37701)

Functional Modules
1. User Registration & Login
  •	Registration with PIN
  •	Form captures:
    o	Name, DOB, Email, Phone
    o	HCP specialty
    o	Treatment Indication
    o	Insurance Type
    o	Email/Text Consent
  •	PIN-based authentication with fallback to OTP/email for recovery
2. Data Management
  •	Local database (SQLite for mobile)
  •	Automatic sync to backend (SQL Server) on network availability
  •	Conflict resolution policies
3. Copay Card Generator
  •	Business logic: Based on Insurance Type, generate copay card
  •	Copay card details:
      o	Card number
      o	Discount amount
      o	Validity
      o	QR Code/barcode
  •	Stored in patient’s profile (available offline)
4. Offline Mode
    •	Store data in SQLite
    •	Sync queued data to backend when online
    •	Use HTTP status for retry mechanisms

**Security Requirements**
•	Encryption:
  o	Data at rest: AES-256 (SQLite encryption)
  o	Data in transit: HTTPS (TLS 1.2+)
•	Authentication:
  o	JWT tokens
  o	Expiry/refresh logic
•	Compliance:
  o	HIPAA-compliant storage
  o	Consent checkbox for communication

**  Mobile & Web Frontend UX Design**
•	Accessibility-first design (arthritis-friendly):
  o	Large touch targets
  o	Voice input support
  o	Contrast and readability
•	Web: Responsive design for tablets and desktops
•	Mobile: MAUI app with adaptive layouts

**Technical Stack Details**
Layer	Technology
Mobile App	.NET MAUI (iOS + Android)
Web Frontend	HTML5, CSS3, JS
Backend API	ASP.NET Core Web API
Sync & Auth	REST API, JWT
Local Storage	SQLite + Sync Engine
Backend DB	SQL Server
Hosting	Azure  

**Deployment Plan**
•	Mobile:
  o	Build & deploy MAUI app for Android and iOS
  o	Prepare .apk and .ipa packages
  o	App Store submission with review notes
•	Web:
  o	Host HTML frontend using .NET Core MVC or Blazor
  o	Deploy API to Azure App Service or Docker-based container
•	Documentation:
  o	Setup guide
  o	Configuration guide
  o	Deployment instructions
  o	API Swagger documentation









