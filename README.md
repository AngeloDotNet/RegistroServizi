# Registro Servizi

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-Interactive%20Server-512BD4?style=flat-square&logo=blazor&logoColor=white)
![MudBlazor](https://img.shields.io/badge/UI-MudBlazor-594AE2?style=flat-square)
![Bootstrap](https://img.shields.io/badge/UI-Bootstrap-594AE2?style=flat-square)
![SQL Server](https://img.shields.io/badge/SQL%20Server-EF%20Core-512BD4?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![MIT](https://img.shields.io/badge/License-MIT-35D120?style=flat-square)

[Overview](#overview) • [Tech stack](#tech-stack) • [License](#license) • [Give a star](#give-a-star) • [Inspired by](#inspired-by)

## Overview

RegistroServizi is a SaaS application designed for public first aid services (P.A.) and voluntary associations.

<!-- It is inspired by an application of the same name, developed in Delphi by Luca Memini. -->

> [!NOTE]
> RegistroServizi is under active development.

<!--
## Architecture

| Project | Responsibility |
| --- | --- |
| `RegistroServizi.Domain` | Contains the core business logic and domain entities for the application. |
| `RegistroServizi.Application` | Contains application services, DTOs, and business logic that orchestrates domain entities. |
| `RegistroServizi.Data` | Handles data access and database interactions using Entity Framework Core. |
| `RegistroServizi.Web` | Contains the Blazor Server application, UI components, and pages. |
-->

## Tech stack

- .NET 10 / ASP.NET Core / Blazor Server
- Entity Framework Core 10 with SQL Server
- ASP.NET Core Identity
- MudBlazor / Bootstrap
- GitHub Actions for CI/CD and release automation
- Docker for containerization and deployment

<!--
### Database Migrations

Create a migration from the repository root:

```bash
dotnet ef migrations add <MigrationName> --project RegistroServizi.Data --startup-project RegistroServizi.Web
```

Apply migrations manually:

```bash
dotnet ef database update --project RegistroServizi.Data --startup-project RegistroServizi.Web
```

## Documentation

- [.NET documentation](https://learn.microsoft.com/dotnet/)
- [Blazor documentation](https://learn.microsoft.com/aspnet/core/blazor/)
- [MudBlazor documentation](https://mudblazor.com/)
- [Entity Framework Core documentation](https://learn.microsoft.com/ef/core/)
-->

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Give a star

If you find this project useful or interesting, please consider giving it a star on GitHub. Your support is greatly appreciated!

## Inspired by

The original RegistroServizi application, developed in Delphi by <a href="https://shorturl.at/S3yxp">Luca Memini</a>, served as the inspiration for this project.

The goal was to modernize the application using contemporary technologies while maintaining its core functionalities and user experience.