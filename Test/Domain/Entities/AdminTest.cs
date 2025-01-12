using minimal_api.Domain.Entities;

namespace Test.Domain.Entities;

[TestClass]
public class AdminTest
{
    [TestMethod]
    public void TestGetSetProperties()
    {
        var adm = new Admin
        {
            Id = 1,
            Email = "admin",
            Password = "admin",
            Role = "Adm"
        };

        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("admin", adm.Email);
        Assert.AreEqual("admin", adm.Password);
        Assert.AreEqual("Adm", adm.Role);

    }
}
