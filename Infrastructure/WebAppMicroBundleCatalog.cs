using TheSingularityWorkshop.FSM_COS;
using TheSingularityWorkshop.MicroBundleDomain;

namespace TheSingularityWorkshop.WebApp.Infrastructure;

public sealed class WebAppMicroBundleCatalog : IMicroBundleCatalog
{
    private readonly WebAppMonikerMicroBundle _moniker = new();

    public bool TryResolve(ulong bundleId, out IMicroBundle? bundle)
    {
        if (bundleId == WebAppMonikerMicroBundle.BundleId)
        {
            bundle = _moniker;
            return true;
        }

        bundle = null;
        return false;
    }
}
