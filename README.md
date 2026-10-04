# The Singularity Workshop WebApp

The public face of **The Singularity Workshop**.

This site is intentionally more than a project README rendered in a browser. It is the Workshop's front door: a place to explain the ideas, demonstrate composition, introduce the growing family of projects, and eventually become the entry point into interactive Workshop experiences.

## Current experience

The first public-facing slice establishes:

- a strong Workshop identity without requiring a permanent logo;
- a responsive landing experience;
- a visual metaphor for independent pieces becoming a composed runtime;
- a small manifest-style composition demonstration;
- the architecture story in plain language;
- links into the public GitHub ecosystem and NuGet packages;
- zero third-party UI dependencies.

## Direction

The WebApp will grow toward the public Workshop experience:

**discover → understand → demonstrate → enter the Workshop**

The long-term goal is not a static marketing site. It is a web doorway into experiences assembled from the same independent packages and composition boundaries described by the Workshop ecosystem.

## Development

Requires .NET 8.

```powershell
dotnet restore
dotnet build --configuration Release
dotnet run
```

## Design principle

The site should **edify, not mystify**. Visuals can make the ideas memorable, but the architecture shown here must remain faithful to the actual projects behind it.


## Runtime composition

The WebApp now crosses the real FSM_COS boundary instead of only illustrating it.

At process startup the host builds a runtime manifest containing the Workshop Moniker bootstrap request (3101), resolves that capability through the host-owned IMicroBundleCatalog, and executes it through FsmCos. The resulting RuntimeAssembly is then rendered by the Blazor GUI renderer.

The important dependency direction is:

**WebApp → FSM_COS → MicroBundleDomain → host-owned MicroBundle catalog**

FSM_COS does not know that the Moniker exists. The host chooses what is available and what belongs in its startup manifest.

The current branch contains a temporary WebApp-local Moniker bridge because the independent TheSingularityWorkshop.Experiences.Moniker package is not yet published. That bridge is intentionally shaped to disappear once the canonical package is available; the bundle identity remains 3101.

The result is a real vertical slice:

**startup manifest → FSM_COS → Moniker MicroBundle → semantic GUI tree → Blazor**

