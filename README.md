
# Practical 15 – ASP.NET MVC Authentication (Windows & Forms)

This project demonstrates two authentication mechanisms in ASP.NET MVC using .NET Framework:

- Test 1: Windows Authentication
- Test 2: Forms Authentication

Both implementations use SQL Server (`Practical15` database) for storing and interacting with user data.

---

## Common Prerequisite: Update Connection String

In your `Web.config`, update the connection string based on your system setup:

```xml
<connectionStrings>
    <add name="DefaultConnection"
         connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=Practical15;Integrated Security=True"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

---

## Test 1: Windows Authentication

### Step-by-Step Setup

1. Create a new **ASP.NET MVC (Framework)** project.

2. In the `Web.config` file, confirm or update the following under `<system.webServer>` section:

```xml
<system.webServer>
  <security>
    <authentication>
      <anonymousAuthentication enabled="false" />
      <windowsAuthentication enabled="true" />
    </authentication>
  </security>
</system.webServer>
```

3. Go to the following path:

```
.vs\YOUR_PROJECT_NAME\config\applicationhost.config
```

Ensure the same block exists there. If not, add it manually.

4. Add this table to your **Practical15** database using either Code First or ADO.NET:

```csharp
public class UserRole
{
    public int Id { get; set; }
    public string WindowsUserName { get; set; }
    public string Role { get; set; }
}
```

5. Go to **Control Panel > Programs > Turn Windows Features on or off > Internet Information Services > World Wide Web Services > Security**, and ensure **Windows Authentication** is enabled.

6. In your MVC project:
   - Log the Windows user via `User.Identity.Name`.
   - Create a simple action method (e.g., `Log`) and log or show the current user’s Windows username.
   - This proves that authentication is working.

### Validation

Click the **Log** button or visit the route manually to validate that the logged-in user's identity is being captured correctly from the Windows account.

---

## Test 2: Forms Authentication

### Step-by-Step Setup

1. Use the same MVC project or create a new one.

2. Update the same `applicationhost.config` file:

```xml
<system.webServer>
  <security>
    <authentication>
      <anonymousAuthentication enabled="true" />
      <windowsAuthentication enabled="false" />
    </authentication>
  </security>
</system.webServer>
```

3. Run the following SQL to set up a basic Users table:

```sql
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL
);

INSERT INTO Users (Username, Password)
VALUES 
('admin', 'admin123'),
('user1', 'user123'),
('user2', 'pass456');
```

4. In your MVC project:
   - Create a `Login` view with username and password fields.
   - On form submission, authenticate against the database using ADO.NET or Entity Framework.
   - If valid, set the `FormsAuthentication.SetAuthCookie(username, false)` and redirect.
   - Use `[Authorize]` attribute on secure pages to restrict unauthenticated access.

5. Update the `Web.config` for Forms Authentication:

```xml
<system.web>
  <authentication mode="Forms">
    <forms loginUrl="~/Account/Login" timeout="30" />
  </authentication>
  <authorization>
    <deny users="?" />
  </authorization>
</system.web>
```

### Validation

Run the application and login with one of the users from the `Users` table. After successful login, display the username of the currently logged-in user.

---

## Notes

- This project uses **Code First / ADO.NET**, not Database First, to keep the implementation lightweight.
- Always verify the correct authentication block is active in both `Web.config` and `applicationhost.config`.
- You must restart Visual Studio or IIS Express after modifying the `applicationhost.config` file.
