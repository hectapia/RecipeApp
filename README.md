# RecipeApp 🍲

A simple **Blazor Server** application for managing recipes with secure user authentication.  
Users can **register, log in, log out**, and manage their own recipes. Authentication is implemented using **ASP.NET Core Identity-style password hashing** and **cookie-based authentication middleware**.



## ✨ Features

- 🔐 Secure user authentication (register, login, logout)
- 🔑 Password hashing with `PasswordHasher<T>`
- 🍴 Create and view personal recipes
- 🛡️ Protected routes using `[Authorize]` and `AuthorizeView`
- 📦 EF Core with SQLite database
- 🖥️ Blazor Server UI



## 📂 Project Structure
```
RecipeApp/
├─ Components/
│  ├─ _Imports.razor
│  ├─ App.razor
│  ├─ Routes.razor
│  ├─ Layout/ 
│  │   ├─ MainLayout.razor
│  │   ├─ MainLayout.razor.css
│  │   ├─ NavMenu.razor
│  │   └─ NavMenu.razor.css
│  └─ Pages/ 
│     ├─ Counter.razor
│     ├─ Error.razor
│     ├─ Home.razorx
│     ├─ Index.razor
│     ├─ Login.razor
│     ├─ Logout.razor
│     ├─ Recipes.razor
│     ├─ Register.razor
│     └─ Weather.razor
├─ Controllers/
│  └─ AuthController.cs
├─ Data/
│  ├─ ApplicationDbContext.cs
│  └─ DbInitializer.cs
├─ Migrations/
│  ├─ 20251128064942_InitialCreate.cs
│  ├─ 20251128064942_InitialCreate.Designer.css
│  ├─ 20251128181659_AddRecipeModelFix.cs
│  ├─ 20251128181659_AddRecipeModelFix.Designer.cs
│  └─ ApplicationDbContextModelSnapshot.cs
├─ Models/
│  ├─ Recipe.cs
│  └─ User.cs
├─ Pages/
│  └─ _Host.cshtml
├─ Properties/
│  └─ launchSettings.json
├─ Services/
│  ├─ IUserService.cs
│  └─ UserService.cs
├─ Shared/
│  ├─ LoginDisplay.razor
│  ├─ MainLayout.razor
│  └─ NavMenu.razor
└─ wwwroot/
│  ├─ css/
│  │  └─ site.css
│  ├─ lib/
│  │  └─ bootstrap/
│  ├─ app.css
│  └─ favicon.png
├─ appsettings.Development.json
├─ appsettings.json
├─ Program.cs
├─ README.md
├─ RecipeApp.csproj
├─ recipeapp.db
└─ RecipeApp.sln
```



## 🚀 Getting Started

### 1. Create solution and project
``` 
bash
dotnet new sln -n RecipeApp
mkdir src && cd src
dotnet new blazorserver -n RecipeApp
dotnet sln ../RecipeApp.sln add RecipeApp/RecipeApp.csproj
``` 

## Add EF Core + SQLite
``` 
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
``` 

## Apply migrations
``` 
dotnet ef migrations add InitialCreate
dotnet ef database update
``` 

## Run the app
``` 
bash 
dotnet run
``` 

## 🔐 Authentication Flow
- Register: Users create an account with email, display name, and password. Passwords are hashed before storage.
- Login: Credentials are validated, and a secure cookie is issued.
- Logout: Cookie is cleared, ending the session.
- Protected routes: Pages like /recipes require authentication ([Authorize]).

## 🛠️ Tech Stack
Blazor Server (UI framework)

ASP.NET Core Authentication (cookie-based)

Entity Framework Core (ORM)

SQLite (database)

## 📖 Notes
-  In production, enforce HTTPS and set cookie SecurePolicy = Always.
-  Consider adding email confirmation, password reset, and role-based authorization for more advanced scenarios.
-  Recipes are linked to users via a foreign key (OwnerUserId). Public recipes can be created by allowing OwnerUserId = null.

## 👩‍💻 Development Tips
-  Use dotnet watch run for hot reload during development.
-  Run dotnet ef migrations add <Name> whenever you change models.
-  Check logs in the terminal for EF Core and authentication events.

## 📜 License
This project is provided as a learning example. You are free to use and adapt it for your own projects.

## 📸 Screenshots & Usage Examples

## 🔑 Register Page
Users can create a new account by entering their email, display name, and password.

## 🔐 Login Page
Existing users can log in securely with their credentials.

## 🚪 Logout Page
Authenticated users can log out to clear their session.

## 🍴 Recipes Page
Authenticated users can add new recipes and view their personal recipe list.

## 🖼️ How to Capture Screenshots
- Run the app locally with dotnet run.
- Navigate to each page (/register, /login, /recipes).
- Use your OS screenshot tool (Snipping Tool on Windows, Shift+Cmd+4 on macOS).
- Save the images into a folder like docs/images/.
- Update the README paths to point to those files.

## ✅ Example Usage Flow
- Register a new account.
- Login with the account credentials.
- Navigate to Recipes and add a recipe (title + instructions).
- See your recipe appear in the list.
- Logout when finished.
