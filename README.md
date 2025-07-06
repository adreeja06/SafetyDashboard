


---

### 📄 `README.md`

````markdown
# Person in Premises Permit Management System - Internship Module

This project is a web-based application built using **ASP.NET Core MVC**, **Entity Framework Core**, and **SQLite**, designed to manage entries and permits for individuals inside a premises or workplace. It was developed as part of an internship project.

---

## 🛠️ Tech Stack

- ASP.NET Core MVC (.NET 6 or above)
- Entity Framework Core (EF Core)
- SQLite (local DB)
- Razor Pages for views
- Visual Studio / VS Code (Recommended IDE)

---

## 💻 How to Run the Project (Windows)

### 1. **Pre-requisites**

- Install [.NET SDK 6.0 or later](https://dotnet.microsoft.com/en-us/download)
- (Optional but recommended) Install [Visual Studio 2022+](https://visualstudio.microsoft.com/) with ASP.NET and web development workload.

---

### 2. **Clone the Repository**

```bash
git clone https://github.com/adreeja06/SafetyDashboard.git
cd SafetyDashboard
````

---

### 3. **Navigate to the Project Folder**

```bash
cd 1stModule-PIPremises
```

---

### 4. **Apply Migrations and Run the App**

Run the following commands in terminal/PowerShell (inside the project directory):

```bash
dotnet ef database update
dotnet run
```

> This will apply the existing migrations, create the `personinpremises.db` file if not present, and start the local server.

---

## ⚠️ Important Note

If the portal does not function as expected (e.g., crashes or shows model-related errors), it may be due to a **backdated or incompatible SQLite database**.

### 🔄 In such cases:

1. **Delete the existing database file**:

   ```
   personinpremises.db
   ```

2. **Re-apply Migrations**:

```bash
dotnet ef database update
```

3. **Run the app again**:

```bash
dotnet run
```

---

## 📁 Folder Structure

```bash
1stModule-PIPremises/
├── Controllers/
├── Models/
├── Views/
├── Migrations/
├── wwwroot/
├── appsettings.json
├── Program.cs
├── personinpremises.db
```

---

## 👩‍💻 Maintained by

**Adreeja Mahato**
Intern, Indian Oil Corporation Limited

Email: \[adreejamahato@gmail.com]

---


