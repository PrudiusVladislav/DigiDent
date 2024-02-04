## Introduction
**DigiDent** - Dental Clinic Management Application. A project developed using **Modular Monolith** architecture. It presents a simple demo version of an application that would be responsible for managing and authenticating new users, scheduling appointments, creating treatment plans, managing clinic employees' schedules, and much more. Here is a diagram presenting the high-level view of the project's structure:
![High Level View Project Diagram]("docs/Images/DigiDent_HighLevel_Diagram.png")

### Technologies stack
- .NET 8
- C# 12
- ASP.NET Core (Minimal API)
- Entity Framework Core
- MS SQL Server
- RabbitMq

### Architecture and Design
I started developing the project to practice creating an **Event-Driven Architecture**. Initially, the project had a Monolith architecture, where each current **module** was represented as a **bounded context**. But at some moment it became clear, that there is a need to make the bounded contexts more **decoupled**, so the decision has been made to transition to a Modular Monolith. Each of the main modules is structured using **DDD & Clean Architecture**. Here you can see the solution structure of the project:
![Solution Structure]("docs/Images/SolutionStructure_Diagram.png")

To implement the current state of the project, I have used quite a few **design patterns**. Here are some of them:
- Result
- Repository 
- Unit Of Work
- Domain / Integration Event
- Mediator + Command
- Decorator
- Notification
- Domain Service, Strongly Typed IDs, and other DDD-related patterns

### Domain & Database Structure
I think diagrams are great for illustrating complex relations, so here are the **charts** showing the **tables** and their relations within the DigiDent **database**:

**User Access** module's **schema** diagram:
![User Access Database Schema Diagram]("docs/Images/UserAccessSchema_Diagram.png")

**Clinic Management** module's **schema** diagram:
![Clinic Management Database Schema Diagram]("docs/Images/ClinicManagementSchema_Diagram.png")

### Application 
The **application layer** was created heavily depending on the **MediatR** library. Also, I have tried to implement the **CQRS** pattern, to have an opportunity in the future to easily separate my **read** and **write** **models**. For this purpose I used an additional layer of abstractions to separate commands and queries: **ICommand** and **IQuery** that both inherit from MediatR's *IRequest*, and the Handlers: **ICommandHandler** and **IQueryHandler**, both inheriting from *IRequestHandler*. Apart from **requests** sent from the API layer, the application layer in my project is used to handle **domain events** (sometimes converting and publishing them to the Message Broker as **integration events**), and in case of User Access and Clinic Management modules - even to **synchronously communicate** between modules.

### API
The API layer has been developed following **REST API** principles. Since the *ASP.NET Core* has been used as the framework to create the api, I used the lightweight and easy-to-develop option - **Minimal API**. Regarding **middleware**, the Bootstrapper API project is using a custom **global error handling middleware**, to catch unhandled exceptions.
