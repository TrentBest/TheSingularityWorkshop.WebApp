using TheSingularityWorkshop.WebApp.Infrastructure;

namespace TheSingularityWorkshop.WebApp.Tests;

public sealed class WebAppCosRuntimeTests
{
    [Fact]
    public void Startup_ComposesMonikerThroughFsmCos()
    {
        var runtime = new WebAppCosRuntime(new WebAppMicroBundleCatalog());

        Assert.Equal(WebAppCosRuntime.RuntimeId, runtime.Assembly.RuntimeId);
        Assert.Equal(WebAppMonikerMicroBundle.BundleId, runtime.Moniker.BundleId);
        Assert.NotNull(runtime.Moniker.Root);
    }
}
