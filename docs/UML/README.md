# WebApp source-to-UML documentation

This directory is the manually reviewed source-documentation index for the WebApp C# files on `development`. It records what was inspected and links each declared maintained type to a dedicated explanation and SVG class diagram. These are hand-authored source reviews, not generated output; a changed source file requires a review of its page and diagram.

## Coverage inventory

| C# source file | Declared type / source unit | Documentation | UML SVG | Status |
|---|---|---|---|---|
| `Program.cs` | Top-level application entry point; compiler-generated `Program` is not explicitly declared in source | [Program.cs](Program.md) | Sequence/registration flow described in page | Documented manually |
| `Infrastructure/WebAppCosRuntime.cs` | `WebAppCosRuntime` | [WebAppCosRuntime](WebAppCosRuntime.md) | [WebAppCosRuntime.svg](assets/WebAppCosRuntime.svg) | Source reviewed |
| `Infrastructure/WebAppExperienceRuntime.cs` | `WebAppExperienceRuntime`, nested `ActorContext`, nested `ActorPhase`, nested `PageContext` | [WebAppExperienceRuntime](WebAppExperienceRuntime.md), [ActorContext](ActorContext.md), [ActorPhase](ActorPhase.md), [PageContext](PageContext.md) | [WebAppExperienceRuntime.svg](assets/WebAppExperienceRuntime.svg), [ActorContext.svg](assets/ActorContext.svg), [ActorPhase.svg](assets/ActorPhase.svg), [PageContext.svg](assets/PageContext.svg) | Source reviewed; transitional implementation explicitly noted |
| `Infrastructure/WebAppMicroBundleCatalog.cs` | `WebAppMicroBundleCatalog` | [WebAppMicroBundleCatalog](WebAppMicroBundleCatalog.md) | [WebAppMicroBundleCatalog.svg](assets/WebAppMicroBundleCatalog.svg) | Source reviewed |
| `Infrastructure/WebAppMonikerMicroBundle.cs` | `WebAppMonikerMicroBundle` | [WebAppMonikerMicroBundle](WebAppMonikerMicroBundle.md) | [WebAppMonikerMicroBundle.svg](assets/WebAppMonikerMicroBundle.svg) | Source reviewed |
| `tests/TheSingularityWorkshop.WebApp.Tests/WebAppCompositionTests.cs` | `WebAppCompositionTests` | [WebAppCompositionTests](WebAppCompositionTests.md) | [WebAppCompositionTests.svg](assets/WebAppCompositionTests.svg) | Source reviewed |

## Scope and verification rules

- Source links target the `development` branch; source code and tests remain authoritative.
- The diagrams show only relationships visible in the inspected source. They are static, manually authored SVGs and are **not** generated from Roslyn, reflection, or a canonical UML model.
- Test links indicate the existing tests and their assertions; they do not imply broader integration or browser coverage.
- No claim is made yet that CI detects source/type inventory drift, validates all links/SVGs, or regenerates diagrams. That automation remains a follow-up.
- This inventory currently covers the repository's `.cs` files, not Razor component markup, JavaScript, CSS, project files, or every public API in referenced packages. Those have their own source and documentation boundaries.

## Review checklist

When a listed source file changes:
1. Re-read the complete source and relevant tests.
2. Update each affected type page and SVG.
3. Check source and diagram links and the SVG `title`/`desc`.
4. Revisit [Architecture](../ARCHITECTURE.md), [Implementation Notes](../IMPLEMENTATION_NOTES.md), and [Release Readiness](../WEBAPP_READINESS.md) if the behavior or responsibility boundary changed.
5. Do not call documentation coverage automated or complete until CI compares declarations with this inventory and deliberately missing pages/diagrams are proven to fail the check.
