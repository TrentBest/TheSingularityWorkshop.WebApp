# WebApp Architecture

## Purpose

**TheSingularityWorkshop.WebApp** is the browser manifestation of the Singularity Workshop.

It is not the Experience itself, not the desktop runtime, and not a replacement for FSM_COS. It is a manifestation and interaction surface through which an Experience can be discovered, entered, observed, and eventually connected to other manifestations.

The WebApp exists to make the Workshop reachable through a browser while preserving the same underlying identity, artifact, and Experience model used by desktop and future immersive clients.

## Architectural position

```
                         Experience
                              |
             +----------------+----------------+
             |                |                |
           WebApp           AnyApp             VR
             |                |                |
        browser UI      local runtime       immersive UI
             |                |                |
             +-------- shared identity -------+
                              |
                         FSM_COS / runtime
                              |
                     MicroBundle artifacts
                              |
                    MicroBundle Repository
```

The diagram describes manifestations, not a requirement that all clients execute the same code.

### WebApp responsibilities

The WebApp owns browser-native concerns:

- discovery and presentation
- navigation
- browser interaction
- Web APIs
- WebXR capability discovery
- communication with a local AnyApp companion
- browser-safe session state
- distribution of published Experience identities
- user-facing connection and capability status

### WebApp does not own

The WebApp must not become:

- a desktop runtime hidden behind HTTP
- an assembly loader
- an arbitrary code execution surface
- the canonical source of Experience semantics
- a duplicate FSM_COS implementation
- a substitute for the MicroBundle Repository

## The Experience boundary

An Experience has identity independent of manifestation.

A browser route, desktop executable, or future VR client may represent the same Experience without becoming the owner of its meaning.

Immutable artifact identity is:

```
BundleId + Version + ContentHash
```

An Experience manifest refers to those immutable artifacts. The WebApp may discover and present them, but it should not silently mutate their identity.

## Browser as manifestation

The browser provides an unusually powerful distribution boundary:

1. open a URL
2. discover an Experience
3. inspect its identity and capabilities
4. enter the Experience
5. optionally connect to a local AnyApp companion
6. optionally request immersive capabilities

The browser therefore acts as an access point into the Workshop rather than as the Workshop's definition.

## AnyApp relationship

WebApp and AnyApp are complementary.

The browser is strong at reach, presentation, interaction, distribution, and browser APIs. AnyApp is strong at local execution, local persistence, computation, artifact caching, and capabilities unavailable to a browser.

The bridge between them is a protocol boundary. It is not a remote shell.

The WebApp may request an action such as "open this published Experience in AnyApp"; it must never transmit arbitrary code, assembly bytes, filesystem commands, or process instructions as part of that request.

## WebXR relationship

WebXR is treated as another manifestation boundary.

WebXR capability belongs to the browser/device. GUI semantics remain platform-neutral. The WebApp may translate semantic Experience presentation into browser/XR presentation, while FSM/API/runtime contracts remain outside the renderer.

Future XR work should preserve this separation:

```
Experience semantics
       |
semantic presentation
       |
WebApp
       |
WebXR manifestation
```

## State model

The WebApp should distinguish at least:

- **discovered** — identity is known
- **available** — required published artifacts can be resolved
- **entered** — browser manifestation is active
- **connected** — AnyApp companion is connected
- **running** — local runtime reports active execution
- **immersive-capable** — browser reports XR capability
- **immersive-active** — an XR session is active

These states describe observations and lifecycle, not ownership of the underlying Experience.

## Design principle

> The WebApp is a door into the Workshop, not the Workshop itself.

That distinction should guide implementation decisions whenever browser convenience conflicts with architectural ownership.
