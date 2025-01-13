using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Services;
using minimal_api.Infra.Db;
using System.Reflection;

namespace Test.Domain.Services;

[TestClass]
public class AdminServicesTest
{
    private AppDbContext CreateTestContext()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new AppDbContext(configuration);
    }

    [TestMethod]
    public void TestStoreAdmin()
    {
        var context = CreateTestContext();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Admins");

        var adm = new Admin()
        {
            Email = "admin",
            Password = "admin",
            Role = "Adm"
        };

        var adminService = new AdminService(context);
        adminService.Store(adm);

        Assert.AreEqual(1, adminService.GetAll(1).Count);
    }
}
