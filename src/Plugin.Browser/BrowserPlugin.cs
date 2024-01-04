using System.ComponentModel;
using System.Drawing;
using Aquality.Selenium.Browsers;
using Microsoft.SemanticKernel;

namespace Community.SemanticKernel.Plugins.Browser;

public sealed class BrowserPlugin
{


    [KernelFunction, Description("Take a screenshot of the current page.")]
    public Task<string> TakePageScreenShootAsync(
        CancellationToken cancellationToken = default)
    {
        var screenshot = AqualityServices.Browser.GetScreenshot();
        return Task.FromResult(Convert.ToBase64String(screenshot));
    }
}
