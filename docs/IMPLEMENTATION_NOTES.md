# Implementation Notes

This repository now has a concrete .NET 8 browser-host vertical slice. The development branch contains an ASP.NET Core Razor Components host using Interactive Server rendering, an FSM_COS composition path for a host-owned Moniker bundle, and an FSM_API-driven landing Experience with a 100-slot living-actor population.

This is not yet a general page-authoring system. The current catalog resolves only the temporary local Moniker compatibility bundle, and the landing Experience is assembled by fixed host code. Preserve useful browser presentation work while replacing the prototype lifecycle with manifest-driven web content composed through FSM_COS. WebApp is the web host manifestation of the AnyApp role; it must not depend on the desktop AnyApp executable or use its local bridge as the required runtime.

## Dependency direction

The intended dependency direction is:

```
WebApp (web host / AnyApp manifestation)
  ├── browser framework + browser input
  ├── host-neutral Experience manifest contract
  ├── web-delivered artifact resolution / validation
  ├── FSM_COS composition
  └── browser presentation of runtime state

Experiences
  └── published Experience / MicroBundle artifacts

FSM_COS
  └── runtime composition boundary

MicroBundleRepository
  └── artifact publication / discovery

Desktop AnyApp
  └── sibling host using desktop input / presentation
```

The WebApp should consume contracts; it should not reach inward and own the implementation of another layer.

## Current implementation slice

The development branch currently proves a different, earlier vertical slice:

1. start the ASP.NET Core browser host;
2. compose a runtime manifest through FSM_COS;
3. verify the Moniker bundle is present in the resulting `RuntimeAssembly`;
4. run Gateway → LivingGui → Moniker → Gravity → Running progression through FSM_API;
5. present the composed Moniker and living actors through Razor Components.

The catalog is still host-owned and only resolves a temporary Moniker compatibility bundle (BundleId 3101). The landing Experience is fixed in `WebAppExperienceRuntime`; this is not proof of arbitrary page authoring or repository-backed artifact resolution. The local FSM_COS startup composition and the landing simulation are separate paths and must not be conflated as canonical Experience execution.

## Next implementation slice

1. define/reuse a host-neutral Experience identity and request/result contract;
2. identify or establish the canonical Experience/process-group execution owner independently of the web UI;
3. compose the declared Experience and its dependencies through FSM_COS;
4. bind the resulting `RuntimeAssembly` to that Experience and prove FSM_API progression under the canonical owner;
5. expose observable execution state to the browser rendering boundary without a WebApp-owned semantic heartbeat;
6. then connect web-delivered manifests/configuration and the reviewed immutable-artifact materialization contract;
7. test that a composed Experience changes the page without editing the host.

Keep browser presentation separate from semantic execution. FSM_COS composes the declared MicroBundles into a `RuntimeAssembly`; it does not tick the Experience. FSM_API state progression belongs to the canonical Experience/process-group execution owner, which WebApp must consume rather than replace with its private heartbeat.

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
