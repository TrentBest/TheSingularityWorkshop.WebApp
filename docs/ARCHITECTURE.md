# WebApp Architecture

## Purpose

![WebApp to AnyApp bridge](assets/webapp-anyapp-bridge.svg)

![Source-grounded runtime UML](assets/webapp-anyapp-runtime-uml.svg)

**WebApp is AnyApp for the web.** Its job is to give the Workshop's application/composition boundary a browser-facing surface: web-based user input, browser events and capabilities, and presentation of observable results.

It is an adapter—not a second application runtime, not an alternative composition engine, and not the owner of Experience semantics or lifecycle. The existing bridge SVG describes a safety boundary; it does not mean WebApp should take over the execution behind that boundary.

## Intended direction

```mermaid
flowchart LR
  U[Person / browser input] --> W[WebApp<br/>web adapter]
  W --> A[AnyApp<br/>application-level intent]
  A --> C[FSM_COS<br/>composition + execution boundary]
  C --> F[FSM_API<br/>state progression]
  F --> R[Observable state / result]
  R --> W
  P[WebPage or another web consumer] -. uses .-> W
```

The key contract is the request/result boundary. WebApp translates browser interactions into defined application requests and renders resulting state. It must not implement a parallel semantic lifecycle to make the page appear alive.

## Responsibilities

### WebApp owns

- browser-native input, interaction, navigation, and presentation;
- adapting browser events into explicit application-level requests;
- browser capability discovery, including Web APIs and WebXR;
- web-safe session and connection status;
- rendering observable state returned by the application/composition layer;
- safe, validated communication across a documented protocol boundary.

### WebApp does not own

- application semantics that belong to AnyApp or Experience artifacts;
- FSM_COS composition or a replacement execution loop;
- an independently invented FSM lifecycle for the canonical Experience;
- arbitrary assembly loading, code execution, filesystem commands, or a remote shell;
- the canonical artifact repository or mutation of immutable artifact identity.

## Relationship to AnyApp

AnyApp is the application-level counterpart; WebApp provides its web-facing manifestation. Keep intent independent of presentation so a browser interaction and a non-web interaction can request the same meaningful operation without duplicating the operation's semantics.

A request such as “enter this published Experience” is an application intent. The web layer may validate and submit that request, then present progress and results. It must not smuggle arbitrary code, assembly bytes, process instructions, or filesystem commands across the boundary.

## Relationship to FSM_COS and FSM_API

FSM_COS remains the composition and execution boundary. WebApp supplies the browser-facing inputs and presentation around that boundary; it must not create a second lifecycle owner merely because the browser needs periodic rendering. FSM_API state progression should be driven according to the canonical runtime/process-group contract, not a WebApp-owned heartbeat that becomes the semantic clock.

The exact integration contract must be proven against the current FSM_COS API and canonical Experience artifacts before the transitional runtime is removed. A successful composition call alone is insufficient evidence that the composed Experience actually executes.

### Source inspection: the missing seam

The current AnyApp `development` branch already defines `ExperienceManifest`, `AnyAppRuntime.Compose` / `ComposeAsync`, and `AnyAppWebBridge`. These are real candidate contracts, not hypothetical future types. However, AnyApp currently builds as a `net8.0-windows` WPF `WinExe`; WebApp must not take a project reference on that desktop executable as if it were a portable runtime library. `AnyAppWebBridge` is a local presentation bridge hosted by AnyApp, with a loopback HTTP endpoint and WebSocket support, not itself a general-purpose web-hosted composition service.

Therefore the next architectural decision is to prove which seam is intended: (a) extract/reuse a host-neutral application/runtime contract that both hosts can consume, or (b) define and test a narrow protocol to a running AnyApp host. The answer must follow the intended product behavior—not a convenience reference. The current WebApp `WebAppCosRuntime` is separate: it composes only the local `WebAppMonikerMicroBundle`, while `WebAppExperienceRuntime` separately creates and ticks its own page/actor FSMs. The two paths are not yet connected.

## Relationship to WebPage

WebPage is a consumer/proving ground for web experiences, not the architectural definition of WebApp. Avoid moving WebPage-specific information architecture, landing-page choreography, or a showcase simulation into the WebApp package's core purpose.

## Artifact identity

Where WebApp presents or requests a published artifact, preserve its immutable identity:

```
BundleId + Version + ContentHash
```

Resolve and validate manifests, verify content hashes, and fail closed when the requested artifact cannot be validated. Do not silently mutate identity or substitute a local demo artifact as if it were canonical.

## State and observability

Keep distinct facts distinct:

- **discovered** — an identity is known;
- **available** — required artifacts can be resolved and validated;
- **requested** — the browser submitted an explicit action;
- **running** — the canonical runtime reports active execution;
- **completed / failed** — the canonical runtime reports an outcome;
- **connected** — an optional companion/protocol connection is established;
- **immersive-capable / immersive-active** — browser/device capability and session state.

These are observations of a lifecycle, not permission for WebApp to own it. Browser rendering may refresh on its own schedule; semantic progression must follow the canonical runtime contract.

## Current migration gap

The current `WebAppExperienceRuntime` creates page and actor FSMs in host code, preallocates 100 actor contexts, and advances them on a private 33 ms heartbeat. Its Moniker → gravity → hub choreography is a transitional prototype. It does not yet satisfy the intended “AnyApp for the web” role and must not be treated as proof of canonical Experience execution.

Before removing it, add deterministic, observable integration tests that prove:

1. browser input becomes the expected application-level request;
2. FSM_COS composes the intended Experience from canonical artifacts;
3. the composed Experience actually progresses under its proper runtime owner;
4. WebApp renders the resulting state without independently driving semantic lifecycle;
5. invalid input, unavailable artifacts, disconnects, and failures are surfaced safely.

## Design principle

> WebApp is the web-facing adapter—not the application, not the composition engine, and not a competing runtime.
