# Project:

## Instructions to Run the Project

1. **Open the existing repository:**
   - Start Visual Studio.
   - Go to **File** > **Open** > **Project/Solution** and locate your existing repository.

2. **Add dependencies in project (PageBuilder.Core):**
   - Open **Package Manager Console** from **Tools** > **NuGet Package Manager** > **Package Manager Console**.
   - Enter the following commands to add the required dependencies:
   - Install-Package Microsoft.Extensions.Configuration -Version 6.0.1
   - Install-Package Microsoft.Extensions.DependencyInjection -Version 6.0.1
   - Make sure you are in the correct project (PageBuilder.Core) when executing these commands.

3. **Add dependencies in project (PageBuilder.WebApi):**
   - Open **Package Manager Console** from **Tools** > **NuGet Package Manager** > **Package Manager Console**.
   - Enter the following commands to add the required dependencies:
   - Install-Package Swashbuckle.AspNetCore -Version 6.5.0
   - Make sure you are in the correct project (PageBuilder.WebApi) when executing these commands.

4. **Alternatively:**
    - Right-click on the solution in Solution Explorer and select Restore NuGet Packages.

5. **Add "aiApiKey" in Manage User Secrets:**
   - Right-click on the project in **Solution Explorer** and select **Manage User Secrets**.
   - The `secrets.json` file will open. Add the following JSON code:
     {
       "aiApiKey": "your-api-key-here"
     }

6. **Run the project:**
   - Ensure all dependencies are successfully installed and configured.