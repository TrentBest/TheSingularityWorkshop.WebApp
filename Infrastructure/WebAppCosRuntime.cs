using TheSingularityWorkshop.FSM_COS;
using TheSingularityWorkshop.MicroBundleDomain;

namespace TheSingularityWorkshop.WebApp.Infrastructure;

public sealed class WebAppCosRuntime
{
    public const ulong RuntimeId = 3101001;

    public WebAppCosRuntime(WebAppMicroBundleCatalog catalog)
    {
        ArgumentNullException.ThrowIfNull(catalog);

        var manifest = new RuntimeManifest(
            RuntimeId,
            [MicroBundleDependencyRequest.Unconfigured(WebAppMonikerMicroBundle.BundleId)]);

        Assembly = new FsmCos(catalog).Execute(manifest);

        if (!Assembly.TryGetBundle<WebAppMonikerMicroBundle>(
                WebAppMonikerMicroBundle.BundleId,
                out var moniker) ||
            moniker?.Root is null)
        {
            throw new InvalidOperationException(
                "The Workshop Moniker did not compose through FSM_COS during WebApp startup.");
        }

        Moniker = moniker;
    }

    public RuntimeAssembly Assembly { get; }
    public WebAppMonikerMicroBundle Moniker { get; }
}
