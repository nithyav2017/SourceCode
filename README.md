# Project Name : Clinical Trial Management System(CTMS)
## Overview: 
The Dotnet branch of this repository contains implementations related to backend development, database integration, and security enhancements.

The Clinical Trial Management System (CTMS) application focuses on refining data models, strengthening security, and enhancing core functionality.

Recent updates follow Domain-Driven Design (DDD) principles and demonstrate SOLID object-oriented programming (OOP) practices through the use of MediatR and an event-driven architecture.

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
- **Publish Docker image (built via Visual Studio) to Azure Container Registry (ACR), then deploy to Azure Container Instances (ACI) using Azure CLI for testing on Azure.**
- 

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
|Abstraction                   |Controller calls 'IMediator', not direct logic                            |
|Encapsulation                 |Scheduling logic is in 'ScheduleVisitCommandHandler'                      |
|Dependency Inversion          |'VisitScheduledEmailHandler' uses 'IEmailService', not concrete           |
|Interface Segregation         |'IEmailService' focuses only on email-related operations                  |
|Polymorphism                  |'IEmailService' can be implemented using SMTP, SendGrid etc.,             |
|Separation of Concerns        |Domain logic, event handling, and Email infrastructure are decoupled      |

### 📁 Realted Files
-[ScheduleVisitCommand.cs] https://github.com/nithyav2017/SourceCode/blob/Dotnet/PharmaClinicalSuite/Application/Events/ScheduledVisit/SchduleVisitCommand.cs - Application Request.

-[ScheduleVisitCommandHandler.cs] https://github.com/nithyav2017/SourceCode/blob/Dotnet/PharmaClinicalSuite/Application/Events/ScheduledVisit/ScheduleVisitCommandHandler.cs   - Handles the business logic.

-[VisitSchduledEvent.cs] https://github.com/nithyav2017/SourceCode/blob/Dotnet/PharmaClinicalSuite/Domain/Events/VisitScheduledEvent.cs - Domain Event.

-[ScheduleVisitEventHandler.cs] https://github.com/nithyav2017/SourceCode/blob/Dotnet/PharmaClinicalSuite/Application/Events/ScheduledVisit/ScheduleVisitEventHandler.cs  - Sends email when event is raised.

-[SmtpEmailService.cs] https://github.com/nithyav2017/SourceCode/blob/Dotnet/PharmaClinicalSuite/Services/SmtpEmailService.cs - Reads config and sends mail using 'SmtpClient'. 





