# Project Name : PharmaClinicalSuite
## Overview: 
The branch Dotnet in this repository contains details about implementations related to backend development, database integration, and security enhancements.
The application Clinical Trial Management System(CTMS) focused on refining data models, improving security, and enhancing application functionality and latest updates follows Domain-Driven Design (DDD) and demonstrate solid OOP principles through a MediatR and Event-Driven architecture.

### **✅ Completed Tasks**
- **Implemented edit functionality** in the application.
- **Hashed the ID** in URLs for improved security.
- **Resolved database navigation property issues** to ensure referential integrity.
- **Configured Docker SQL Server connections** to enable seamless backend integration.
- **Refactored dropdown filtering logic** for dynamic data binding.
- **Enhanced form UI elements** to improve spacing and readability.
- **Implemented MDM logic using Fuzz algorithm** to identify and eliminate duplicate records.
- **Created multiple isolated DbContext instances** to enable parallel operations and prevent interaction conflicts.
- **Implemented event-driven design using MediatR and Domain Events** to decouple business logic from infrastructure concerns and support scalable side-effect handling.

---
### Flow Overview
--**User schedules a visit** via the UI.
--The **Controller** sends a 'ScheduleVisitCommand' through **MediatR**.
--The **Command Handler** creates a 'Visit' and saves it through EF Core.
--VisitScheduledEvent is raised after successful scheduling.
--An **event handler** listens to 'VisitScheduleEvent' and send an email using 'IEmailService'

---
### OOP Principles Demostrated
|Principle                     |Where It's Applied                                                        | 
|------------------------------|--------------------------------------------------------------------------|
|Abstarction                   |Controller calls 'IMediator', not direct logic                            |
|Encapsulation                 |Scheduling logic is in 'ScheduleVisitCommandHandler'                      |
|Dependency Inversion          |'VisitScheduledEmailHandler' uses 'IEmailService', not concrete           |
|Interface Segregation         |'IEmailService' focuses only on email-related operations                  |
|Polymorphism                  |'IEmailService' can be implemented using SMTP, SendGrid etc.,             |
|Separation of Concerns        |Domain logic, event handling, and Email infrastructure are decoupled      |

### Realted Files
-'ScheduleVisitCommand.cs' - Application Request.
-'scheduleVisitCommandHandler.cs' - Handles the business logic.
-'VisitSchduledEvent.cs' - Domain Event.
-'VisitScheduledEmailHandler.cs' - Sends email when event is raised.
-'SmtpEmailService.cs' - Reads config and sends mail using 'SmtpClient'.





