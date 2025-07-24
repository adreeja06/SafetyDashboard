# Person-In-Premises & Permit Management Dashboard Module



This project is a web-based application built using **ASP.NET Core MVC** and **Entity Framework Core**. It was developed as part of a summer internship at **Indian Oil Corporation Limited (IOCL), Eastern Region Pipelines (ERPL), Kolkata**. The internship was completed between **June 10th, 2025 and July 9th, 2025**.

The system is designed to manage entries and permits for individuals within a premises or workplace, as per the project "Person-In-Premises Permit Management Dashboard Module".

---

## ✨ Features

This application provides a simple and effective way to manage personnel on-site.

-   **Dashboard View**: A main dashboard to see a list of all individuals currently on the premises.
-   **Person Management**: Full **CRUD** (Create, Read, Update, Delete) functionality for managing person details.
-   **Permit Management**: A system to issue, view, update, and revoke permits for individuals.
-   **Entry/Exit Logging**: Tracks when a person enters or leaves the premises.
-   **Simple & Clean UI**: Built with Razor Pages and standard HTML/CSS for a straightforward user experience.

---

## 🙏 Acknowledgements

This project was made possible through the internship opportunity provided by **Indian Oil Corporation Limited, Eastern Region Pipelines (ERPL), Kolkata**.

A special thanks to my project guide and supervisor, **Shri. Pramod Mandal, General Manager of Information Systems**, for his invaluable guidance and support throughout the project's development. I would also like to thank **Shri. Soutan Koley, Assistant Manager (LLD and CSR)**, for facilitating the internship process.

---

## 🛠️ Tech Stack

-   **.NET 6** (ASP.NET Core MVC)
-   **Entity Framework Core**
-   **SQLite** (for local database)
-   **Razor Pages** for views
-   **HTML / CSS**

---

## 💻 How to Run the Project (Windows)

### 1. **Pre-requisites**

-   Install [.NET SDK 6.0 or later](https://dotnet.microsoft.com/en-us/download).
-   (Optional but recommended) Install [Visual Studio 2022+](https://visualstudio.microsoft.com/) with the **ASP.NET and web development** workload.

---

### 2. **Clone the Repository**

```bash
git clone [https://github.com/adreeja06/SafetyDashboard.git](https://github.com/adreeja06/SafetyDashboard.git)
cd SafetyDashboard
```

---

### 3. **Database Setup**

This project uses **Entity Framework Core migrations** to set up the SQLite database automatically.

1.  Open a terminal or command prompt in the project's root directory (the one containing the `.csproj` file).
2.  Run the following command to apply the migrations and create the database file (`SafetyDashboard.db`):

    ```bash
    dotnet ef database update
    ```

    This command will read the migration files in the project and create the database schema accordingly.

---

### 4. **Run the Application**

You can run the project in two main ways:

#### **Using the .NET CLI**

1.  In your terminal, from the project's root directory, run the application:

    ```bash
    dotnet run
    ```
2.  Once the application starts, it will log the URL it's listening on (e.g., `http://localhost:5000` or `https://localhost:5001`). Open this URL in your web browser.

#### **Using Visual Studio**

1.  Open the `.sln` file in Visual Studio 2022.
2.  Press `F5` or click the "Start Debugging" button (green play icon) to build and run the project.
3.  Visual Studio will automatically open a browser window with the application running.

---

## 📂 Project Structure

The project follows the standard **ASP.NET Core MVC** pattern:

-   `/Models`: Contains the C# classes that represent the application data (e.g., `Person`, `Permit`). These are used by EF Core to create the database tables.
-   `/Views`: Contains the Razor (`.cshtml`) files that define the UI. Each controller has a corresponding folder of views.
-   `/Controllers`: Contains the C# classes that handle user requests, process data (using Models), and return a response (typically a View).
-   `/Data`: Includes the `DbContext` class and migration files for Entity Framework Core.
-   `appsettings.json`: Configuration file, including the database connection string.
-   `Program.cs`: The main entry point of the application where services and middleware are configured.
