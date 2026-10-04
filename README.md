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
