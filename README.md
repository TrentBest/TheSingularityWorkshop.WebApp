# The Singularity Workshop WebApp

The browser manifestation of **The Singularity Workshop**.

The WebApp is a doorway into Experiences: it discovers published Experiences, presents them in a browser, connects to local companion runtimes when requested, and provides the browser-side boundary for future WebXR manifestations.

It is **not** the Experience itself, the desktop runtime, or a duplicate of FSM_COS.

## Architecture

![WebApp to AnyApp bridge](docs/assets/webapp-anyapp-bridge.svg)

The bridge is deliberately narrow: it coordinates an explicitly requested Experience rather than turning the browser into a remote command source.

## Architecture

An Experience may have multiple manifestations:

- **WebApp** — browser distribution, discovery, interaction, Web APIs, WebXR, and companion connectivity.
- **AnyApp** — local desktop execution, persistence, computation, artifact caching, and local services.
- **Future VR** — immersive presentation and spatial interaction.

These manifestations share identity and protocol boundaries without requiring shared rendering implementations.

## Immutable identity

Published MicroBundles are addressed by:

```text
BundleId + Version + ContentHash
```

The WebApp presents and consumes these identities; it does not redefine them.

## WebApp ↔ AnyApp

The browser may explicitly launch a published Experience in AnyApp through the `anyapp://` protocol and then connect through a constrained loopback WebSocket bridge.

The bridge is a coordination protocol, **not a remote shell**.

It may exchange bounded information such as:

- Experience identity
- session identity
- capabilities
- lifecycle state
- heartbeat
- explicit events

It must not become an arbitrary code, command, filesystem, or assembly execution channel.

## WebXR

WebXR is a manifestation capability of the browser. The semantic Experience model remains independent of headset and browser rendering APIs.

Future spatial presentation will use semantic observer concepts rather than leaking platform-specific camera types into shared contracts.

## Documentation

- [Architecture](docs/ARCHITECTURE.md)
- [Experience Manifestation Theory](docs/EXPERIENCE_MANIFESTATION_THEORY.md)
- [WebApp ↔ AnyApp Boundary](docs/WEBAPP_ANYAPP_BOUNDARY.md)
- [WebXR Manifestation](docs/WEBXR.md)
- [Security Model](docs/SECURITY_MODEL.md)
- [Roadmap](docs/ROADMAP.md)
- [Implementation Notes](docs/IMPLEMENTATION_NOTES.md)

## Guiding principle

> **The WebApp is a door into the Workshop, not the Workshop itself.**

Implementation starts only after the boundaries above have been reviewed against the surrounding Workshop architecture.
