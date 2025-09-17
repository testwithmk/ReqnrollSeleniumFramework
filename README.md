Reqnroll Selenium Framework: 
Automation testing project developed during an internship at MVP Studio.
The web portal runs in Docker containers, with configuration managed via YAML.

Tech Stack:-
- Programming Language: C#
- Test Framework: NUnit, Reqnroll (BDD)
- Automation Tool: Selenium WebDriver
- Containerization: Docker (local containers)
- Configuration Management: YAML, appsettings.json
- Design Pattern: Page Object Model (POM)
- IDE: Visual Studio

Features:-
- Cross-browser support: Chrome, Firefox, Edge
- Profile Tab Testing: Languages & Skills (add, edit, delete, validations)
- Data-driven scenarios using SpecFlow tables
- Dynamic test data generation for large payloads
- Hooks for scenario setup/teardown
- Config-driven browser & settings

Project Structure:-
- Pages/           -> Page objects
- StepDefinitions/ -> Step definition files
- Utilities/       -> WebDriver, ConfigReader, TestDataHelper
- Hooks/           -> Setup and cleanup hooks
- Features/        -> Gherkin feature files
- Config/          -> appsettings.json

Setup:-

1. Clone the repo:
git clone <repository-url>

2. Open in Visual Studio and restore NuGet packages: 
 
3. Set browser in Config/appsettings.json:
{
  "AppSettings": { "Browser": "Chrome" }
}

Run Tests:-
- From Visual Studio Test Explorer
- Or via CLI: dotnet test

SpecFlow tags:-
@Add, @Edit, @Delete, @InvalidInput, @Duplicate, @Destructive
