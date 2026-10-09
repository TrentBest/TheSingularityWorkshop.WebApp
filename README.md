# The Singularity Workshop WebApp

The public browser manifestation of **The Singularity Workshop**.

WebApp is a host and proving ground: it presents Workshop Experiences in the browser while relying on the ecosystem's actual runtime and composition boundaries rather than recreating them.

## Current vertical slice

The current development line demonstrates:

- a responsive Workshop gateway;
- explicit entry into a live Experience;
- FSM_API-driven page progression;
- independently instantiated living actors sharing a processing group;
- FSM_COS composition through a host-owned MicroBundle catalog;
- Moniker → gravity → hub progression;
- a responsive browser manifestation.

The current host retains a temporary local Moniker compatibility bundle (BundleId 3101) until the canonical Experiences artifact is published and verified for consumption.

## Architecture

The essential direction is:

**WebApp → FSM_API / FSM_COS → MicroBundleDomain → host-owned or published MicroBundles**

WebApp owns browser presentation and interaction. It does not become FSM_COS, a repository, a desktop runtime, or the owner of Experience semantics.

## Documentation

The documentation layer lives under [`docs/`](docs/DOCUMENTATION_INDEX.md).

Start with:

- [Documentation Index](docs/DOCUMENTATION_INDEX.md)
- [Architecture](docs/ARCHITECTURE.md)
- [Implementation Notes](docs/IMPLEMENTATION_NOTES.md)
- [Experience Manifestation Theory](docs/EXPERIENCE_MANIFESTATION_THEORY.md)
- [Roadmap](docs/ROADMAP.md)
- [Security Model](docs/SECURITY_MODEL.md)

External packages own their own detailed theory and API documentation. WebApp documents how it uses those abstractions.

## Development

Requires .NET 8.

    dotnet restore
    dotnet build --configuration Release
    dotnet test tests/TheSingularityWorkshop.WebApp.Tests/TheSingularityWorkshop.WebApp.Tests.csproj --configuration Release
    dotnet run

No NuGet publication is performed by this repository without explicit release approval.

## Design principle

> **Edify, don't mystify.**

The browser should make the Workshop understandable by showing the real architecture in action—not by replacing that architecture with a static imitation.