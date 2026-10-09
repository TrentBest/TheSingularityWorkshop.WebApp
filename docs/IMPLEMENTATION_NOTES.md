# Implementation Notes

This repository now has a concrete .NET 8 browser-host vertical slice. The development branch contains an ASP.NET Core Razor Components host using Interactive Server rendering, an FSM_COS composition path for a host-owned Moniker bundle, and an FSM_API-driven landing Experience with a 100-slot living-actor population.

This is not yet a general page-authoring system. The current catalog resolves only the temporary local Moniker compatibility bundle, and the landing Experience is assembled by fixed host code. Implementation should preserve the working vertical slice while moving page intent into explicit artifacts and contracts rather than growing a second runtime architecture inside WebApp.

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

## Current implementation slice

The development branch currently proves a different, earlier vertical slice:

1. start the ASP.NET Core browser host;
2. compose a runtime manifest through FSM_COS;
3. verify the Moniker bundle is present in the resulting `RuntimeAssembly`;
4. run Gateway → LivingGui → Moniker → Gravity → Running progression through FSM_API;
5. present the composed Moniker and living actors through Razor Components.

The catalog is still host-owned and only resolves a temporary Moniker compatibility bundle (BundleId 3101). The landing Experience is fixed in `WebAppExperienceRuntime`; this is not proof of arbitrary page authoring or repository-backed artifact resolution.

## Next implementation slice

1. define a page/Experience artifact contract from existing canonical APIs;
2. let authoring produce a manifest/configuration rather than host-specific runtime code;
3. compose that declaration through FSM_COS;
4. present the resulting Experience through the existing browser rendering boundary;
5. test that an authored artifact can change the page without editing the host;
6. then advance repository-backed discovery and the WebApp ↔ AnyApp boundary.

Keep the browser presentation and FSM_API/FSM_COS ownership boundaries intact.

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
