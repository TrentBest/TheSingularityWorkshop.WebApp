# WebApp Documentation

The WebApp documentation is layered so that the repository explains its own role without duplicating the theory owned by other Workshop packages.

## Start here

- [Architecture](ARCHITECTURE.md) — what WebApp owns and where it sits.
- [Implementation Notes](IMPLEMENTATION_NOTES.md) — how the current host is actually built.
- [Roadmap](ROADMAP.md) — current proving-ground work and future integration.
- [Experience Manifestation Theory](EXPERIENCE_MANIFESTATION_THEORY.md) — why one Experience can have multiple manifestations.

## Source-to-UML documentation

- [C# source/UML inventory](UML/README.md) — manually reviewed source-unit and type index, dedicated type pages, and accessible SVG class cards. The pages are source-reviewed, hand-authored documentation; they are not yet generated or drift-checked by CI.

## Boundaries

- [WebApp ↔ AnyApp](WEBAPP_ANYAPP_BOUNDARY.md) — optional desktop companion protocol, separate from WebApp's own host/runtime path.
- [WebXR](WEBXR.md) — future browser/XR manifestation.
- [Security Model](SECURITY_MODEL.md) — trust and authority boundaries.
- [Release Readiness](WEBAPP_READINESS.md) — verified release gates, current estimate, and outstanding blockers.

## Documentation ownership

WebApp documents **how WebApp uses external abstractions**.

External repositories own their own detailed theory and API documentation:

- FSM_API owns FSM execution semantics.
- FSM_COS owns runtime composition semantics.
- MicroBundleDomain owns MicroBundle contracts.
- GUI owns semantic presentation contracts.
- Experiences owns canonical Experience artifacts.

WebApp should link to those projects when deeper theory is required rather than copying their documentation into this repository.
