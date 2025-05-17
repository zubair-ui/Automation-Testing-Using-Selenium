# The Internet Herokuapp Automation Testing

Automated UI testing project for [The Internet](https://the-internet.herokuapp.com) web application using Selenium WebDriver in C#.

---

## Table of Contents

- [Project Overview](#project-overview)
- [Tested Pages](#tested-pages)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [How to Run Tests](#how-to-run-tests)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

---

## Project Overview

This project implements automated tests for multiple interactive pages on [The Internet](https://the-internet.herokuapp.com) website.  
It verifies elements such as dynamic content, Shadow DOM access, notification messages, and footer/github links.

Each page is encapsulated using the Page Object Model (POM) pattern for better maintainability and scalability.

---

## Tested Pages

- **Shifting Content:** Verifies multiple shifting content examples and their navigation.
- **Shadow DOM:** Demonstrates how to access Shadow DOM elements via JavaScript Executor.
- **Notification Message:** Handles dynamic notification messages that change upon user interaction.
- ...
- ...

---

## Technologies Used

- C# (.NET)
- Selenium WebDriver
- Selenium ChromeDriver
- Visual Studio

---

## Setup and Installation

1. Clone this repository:

   ```bash
   git clone https://github.com/yourusername/the-internet-automation.git
   ```

2. Open the solution/project in Visual Studio.

3. Install required NuGet packages (if not already):

   - Selenium.WebDriver
   - Selenium.Support
   - Selenium.WebDriver.ChromeDriver

4. Ensure you have a compatible WebDriver (e.g., ChromeDriver) in your system PATH or project folder.

---

## How to Run Tests

- Open the solution in Visual Studio.
- Build the project.
- Run tests via your chosen test runner (Test Explorer in Visual Studio or command line).
- Alternatively, instantiate page classes in a console app to manually run test methods.

---

## Project Structure

```
/TheInternetTest
  /Pages                # Page Object Model classes for different site pages
    - ShiftingContentPage.cs
    - ShadowDomPage.cs
    - NotificationMessagePage.cs
    - ... other page classes
  Program.cs            #  Entry point to run test execution
  README.md
```

---

## Contributing

Contributions are welcome! Feel free to submit issues and pull requests.

Please follow coding standards and add descriptive comments.

---

## License

This project is licensed under the MIT License.

---
