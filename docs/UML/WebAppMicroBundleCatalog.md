# WebAppMicroBundleCatalog

![UML class diagram for WebAppMicroBundleCatalog. It implements IMicroBundleCatalog and resolves the local WebAppMonikerMicroBundle by ID and optionally exact version.](assets/WebAppMicroBundleCatalog.svg)

- **Fully qualified name:** `TheSingularityWorkshop.WebApp.Infrastructure.WebAppMicroBundleCatalog`
- **Kind:** public sealed class implementing `IMicroBundleCatalog`
- **Source:** [`Infrastructure/WebAppMicroBundleCatalog.cs`](../../Infrastructure/WebAppMicroBundleCatalog.cs)

## Responsibility

Supplies the current local compatibility bundle to FSM_COS. It holds one `WebAppMonikerMicroBundle` instance in a private readonly field.

## Surface and behavior

- `TryResolve(ulong bundleId, string version, out IMicroBundle? bundle)` succeeds only when the ID matches `WebAppMonikerMicroBundle.BundleId` and the requested version equals the descriptor version using ordinal comparison.
- `TryResolve(ulong bundleId, out IMicroBundle? bundle)` succeeds for the matching ID without checking a version.
- On failure, both overloads assign `null` and return `false`.

## Architectural limitation

This catalog does not resolve published manifests, repository artifacts, content hashes, or arbitrary bundles. It is a local single-bundle catalog and remains a migration placeholder.

## Verification

[WebAppCompositionTests](WebAppCompositionTests.md) indirectly verifies this catalog through the FSM_COS startup composition test. The existing tests do not exhaustively test both catalog overloads or version mismatch behavior.
