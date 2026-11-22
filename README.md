# OpenPayLab

A .NET 10 payment orchestration platform exploring multi-PSP integration, Clean Architecture, Domain-Driven Design (DDD), idempotency, resilience, automated testing, and Continuous Integration (CI/CD).

## Continuous Integration

The project uses Jenkins to automate the CI pipeline on changes pushed to the main branch.

The pipeline performs:

- Dependency restoration.
- Local .NET tool restoration.
- Release build.
- Automated test execution.
- TRX test result generation.
- TRX to JUnit XML conversion.
- Test result publication in Jenkins.

**Technology stack:** .NET 10, C#, Jenkins, Docker, GitHub, xUnit.
