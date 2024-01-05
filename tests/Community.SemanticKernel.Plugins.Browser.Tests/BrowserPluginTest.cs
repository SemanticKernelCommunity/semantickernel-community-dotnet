namespace Community.SemanticKernel.Plugins.Browser.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    [Ignore]
    public async Task TakePageScreenShootSucceedAsync()
    {
        var browserPlugin = new BrowserPlugin();
        var screenshot = await browserPlugin.TakePageScreenShootAsync();

        Assert.IsFalse(string.IsNullOrEmpty(screenshot));
    }
}