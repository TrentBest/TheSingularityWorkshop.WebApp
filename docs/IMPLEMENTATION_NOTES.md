# Implementation Notes

This repository currently begins as a theory-first shell.

Implementation should proceed from the boundaries documented here rather than growing a second runtime architecture inside the WebApp.

## Dependency direction

The intended dependency direction is:

```
WebApp
  ├── browser framework
  ├── GUI semantic consumers/adapters
  ├── Experience discovery client
  └── AnyApp bridge client

Experiences
  └── published Experience/MicroBundle artifacts

FSM_COS
  └── runtime composition

MicroBundleRepository
  └── artifact publication/discovery
```

The WebApp should consume contracts; it should not reach inward and own the implementation of another layer.

## First implementation slice

The first useful slice is intentionally small:

1. load a published Experience catalog
2. display its immutable identity
3. open the Experience in the WebApp
4. offer **Open in AnyApp**
5. connect to the local bridge
6. display connection/session state
7. exchange heartbeat
8. exchange one explicit Experience event

That proves the architecture before adding authoring, XR rendering, or elaborate visual systems.

## Repository integration

Experience manifests should reference immutable MicroBundle artifact identities.

Do not introduce another ad-hoc artifact addressing scheme in WebApp.

## Rendering

Browser rendering may use whatever browser-native implementation is appropriate, but semantic meaning belongs to the shared GUI/Experience contracts.

Do not add WPF, WinUI, Unity, Three.js, or browser DOM types to shared semantic contracts.

## Testing

Tests should be organized around boundaries:

- manifest identity
- catalog behavior
- bridge protocol serialization
- launch URI parsing
- session lifecycle
- malformed input
- capability normalization
- artifact identity preservation

Integration tests should distinguish browser limitations from protocol correctness.
