
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

1. Clone project.

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
.vs\Practical-15\config\applicationhost.config
```

Ensure the same block exists there.

4. Go to **Control Panel > Network and Internet > Internet Options > Security > Local Intranet**:
   - Click on **Sites**, then **Advanced**, and add `http://localhost` to the list.
   - Then click on **Custom level**, scroll down to **User Authentication > Logon**, and select **"Automatic logon with current username and password"**.

5. In output:
   - Goto log table or check the current user’s Windows username.
   - This proves that authentication is working.

### Validation

Click the **Log** button or visit the route manually to validate that the logged-in user's identity is being captured correctly from the Windows account.

---

## Test 2: Forms Authentication

### Step-by-Step Setup

1. Use the same MVC project and make Task2 project as startup.

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

### Validation

Run the application and login with one of the users from the `Users` table. After successful login, you will be able to see username of the currently logged-in user.

---

## Notes

- This project uses **Code First / ADO.NET**, not Database First, to keep the implementation lightweight.
- Always verify the correct authentication block is active in both `Web.config` and `applicationhost.config`.
- You must restart Visual Studio after modifying the `applicationhost.config` file.
