![The Singularity Workshop animated moniker](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Renderer/development/docs/assets/moniker.svg)

# The Singularity Workshop WebApp

> **AnyApp for the web: browser-facing inputs and interaction for FSM_COS—not another application runtime.**

WebApp is the web adapter at the Workshop boundary. It turns browser events and user input into well-defined requests, and presents observable state back through web UI. It should not own the meaning or lifecycle of an Experience.

```mermaid
flowchart LR
  U[Browser user] --> W[WebApp<br/>web input + presentation]
  W --> A[AnyApp<br/>application intent]
  A --> C[FSM_COS<br/>composition + execution boundary]
  C --> F[FSM_API<br/>state progression]
  F --> V[Observable result]
  V --> W
  P[WebPage or other web consumer] -. consumes .-> W
```

## 🟦 01 What

A focused web-facing adapter: browser UI, user input, browser capabilities, and the safe translation of browser events into application-level requests.

## 🟣 02 Why

The web is another way to reach AnyApp and FSM_COS—not a reason to create a parallel host-owned application lifecycle. The same intent should remain meaningful when the manifestation changes.

## 🩵 03 How

WebApp captures and presents. AnyApp expresses the application-level intent. FSM_COS composes and owns its execution boundary; FSM_API provides state-machine progression. WebApp must not silently replace those responsibilities with its own semantic loop.

## 🟢 04 See it in a minute

The repository currently has no verified public WebApp URL, so a public demo cannot honestly be promised. For the current local browser journey and Chromium smoke-test evidence, see [WebPage](https://github.com/TrentBest/WebPage), a consumer/proving ground rather than the definition of WebApp's purpose.

For local verification, use the .NET 8 commands in [Development](#development). The current gateway → Moniker → hub vertical slice is transitional implementation, not yet proof of the intended AnyApp-for-web boundary.

## 🟪 05 Documentation and theory

[Documentation Index](docs/DOCUMENTATION_INDEX.md) · [Architecture](docs/ARCHITECTURE.md) · [Implementation Notes](docs/IMPLEMENTATION_NOTES.md) · [Security Model](docs/SECURITY_MODEL.md) · [Source-grounded runtime UML](docs/assets/webapp-anyapp-runtime-uml.svg)

The class map distinguishes existing source dependencies from the intended request/result seam, which is not yet proven end to end.

---

## Current implementation versus target

The current development implementation still creates a page FSM and 100 actor FSM contexts in `WebAppExperienceRuntime`, and advances them through a private heartbeat. That is useful as a visible prototype, but it is **not** the target architecture. The migration must replace this duplicate lifecycle with web input and presentation connected to canonical AnyApp/FSM_COS behavior, with deterministic tests proving the path.

## Development

Requires .NET 8.

    dotnet restore
    dotnet build --configuration Release
    dotnet test tests/TheSingularityWorkshop.WebApp.Tests/TheSingularityWorkshop.WebApp.Tests.csproj --configuration Release
    dotnet run

No NuGet publication or deployment is authorized by this documentation change.

## Design principle

> **Edify, don't mystify.** Show the real architecture in action; don't substitute a static imitation or a second runtime.

---

<p align="center">
  <a href="https://github.com/TrentBest/TheSingularityWorkshop" aria-label="The Singularity Workshop living avatar">
    <img src="https://raw.githubusercontent.com/TrentBest/WebPage/development/TheSingularityWorkshop/wwwroot/Images/TheSingularityWorkshopLivingAvatar.svg" alt="The Singularity Workshop living avatar — a digital vortex converging on its central eye" width="112" />
  </a><br />
  <strong>The Singularity Workshop</strong><br />
  <em>If a picture is worth a thousand words, what is a living picture worth?</em><br />
  <a href="https://github.com/TrentBest">GitHub</a> · <a href="https://www.patreon.com/TheSingularityWorkshop">Support the Workshop</a>
</p>
