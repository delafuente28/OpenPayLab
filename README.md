# OpenPayLab

A .NET 10 payment orchestration platform exploring multi-PSP integration, Clean Architecture, Domain-Driven Design (DDD), idempotency, resilience, automated testing, and Continuous Integration (CI/CD).

The project is built as an evolving engineering initiative, focusing on maintainability, reliability, and production-oriented software design.

## Architecture

OpenPayLab follows a layered architecture inspired by Clean Architecture, separating business rules from infrastructure concerns.

The solution is organized into the following projects:

- **OpenPayLab.Domain** – Core domain concepts and business rules.
- **OpenPayLab.Application** – Application logic and use cases.
- **OpenPayLab.Infrastructure** – Infrastructure and external integrations.
- **OpenPayLab.Api** – HTTP API and application entry point.
- **OpenPayLab.Domain.Tests** – Automated domain tests.

## Getting Started

### Prerequisites

- .NET 10 SDK
- Git

### Build the solution

```bash
git clone https://github.com/delafuente28/OpenPayLab.git
cd OpenPayLab

dotnet restore
dotnet tool restore
dotnet build OpenPayLab.slnx --configuration Release
```

### Run automated tests

```bash
dotnet test OpenPayLab.slnx --configuration Release
```

## Continuous Integration

OpenPayLab uses Jenkins to automate its CI pipeline whenever changes are pushed to the `main` branch.

The pipeline uses SCM polling to detect changes in the GitHub repository and runs automatically when a new revision is detected.

The pipeline performs:

1. Repository checkout.
2. Dependency restoration.
3. Local .NET tool restoration.
4. Release build.
5. Automated test execution.
6. TRX test result generation.
7. TRX to JUnit XML conversion.
8. Test result publication in Jenkins.

Test results are published in Jenkins, making build outcomes and test execution visible for every pipeline run.

## Technology Stack

- **Backend:** C#, .NET 10
- **Architecture:** Clean Architecture, Domain-Driven Design
- **Database:** SQL (planned for persistence implementation)
- **DevOps:** Jenkins, Docker, GitHub
- **Testing:** xUnit
- **CI/CD:** Automated builds, test execution and reporting

## Project Goals

- Explore multi-PSP payment orchestration.
- Design reliable payment workflows.
- Implement idempotency and resilience patterns.
- Maintain clear architectural boundaries.
- Build a robust automated testing strategy.
- Evolve towards a maintainable, production-oriented platform.
