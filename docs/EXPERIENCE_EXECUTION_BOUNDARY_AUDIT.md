# Experience Execution Boundary Audit

**Branch reviewed:** `development`  
**Review date:** 2026-10-20  
**Scope:** WebApp host, FSM_COS composition contract, and the currently documented FSM_API progression path. This is a source/documentation audit, not proof of a complete runtime integration.

## Finding

The current WebApp slice composes a local Moniker MicroBundle through FSM_COS and separately progresses a host-owned landing-page/actor FSM through FSM_API. Those are two paths. Their coexistence does not prove that the `RuntimeAssembly` returned by FSM_COS is the Experience that FSM_API progresses.

This is a real integration gap, not something to fix by renaming or wrapping the existing heartbeat.

## Source-grounded responsibility boundary

The FSM_COS development-branch document [Runtime Boundary](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/development/docs/RUNTIME_BOUNDARY.md) explicitly defines the boundary as:

- FSM_COS resolves and loads MicroBundles, performs arbitration, and returns `RuntimeAssembly`.
- Starting an Experience, stepping FSM_API process groups, and platform lifecycle remain outside FSM_COS.
- Assembly is not execution.

The FSM_COS [Consuming MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/development/docs/CONSUMING_MICROBUNDLES.md) describes a catalog that resolves manifest roots/dependencies to instantiated `IMicroBundle` objects. That catalog/composition contract alone does not establish how an artifact becomes a trusted executable bundle or who advances the resulting Experience.

The FSM_API [tick-boundary and registry-ownership guide](https://github.com/TrentBest/FSM_API/blob/development/Documentation/User%20Guide/Theory/06_Tick_Boundaries_And_Registry_Ownership.md) documents `Interaction.Update(processingGroup)` as a process-group tick followed by deferred-mutation processing. It explicitly warns that this is not a global lock or complete lifecycle abstraction. The public tick primitive is useful execution machinery, but the sources inspected here do not establish a host-neutral Experience owner that binds a manifest/assembly to lifecycle, scheduling, and results.

The WebApp [Implementation Notes](IMPLEMENTATION_NOTES.md) and [Release Readiness](WEBAPP_READINESS.md) record the current host-owned `WebAppExperienceRuntime`, its private heartbeat, the local Moniker compatibility bundle, and the lack of proof that the composed assembly is the object being executed.

## Required integration contract before migration

Do not remove the current compatibility implementation until the following contract is located in an existing shared layer or deliberately designed and tested there:

1. **Identity:** a host-neutral Experience request identifies the immutable Experience/manifest and the exact bundle versions and content hashes it requires.
2. **Composition result:** the FSM_COS `RuntimeAssembly` is associated with that request through a documented, testable boundary. A successful composition alone is not an execution-success result.
3. **Execution owner:** one canonical, non-presentation owner starts, advances, stops, and observes the Experience and its FSM_API process groups. It must define scheduling, cancellation/disposal, error propagation, and observable state.
4. **Host adapter:** WebApp submits validated requests and presents state/results from that owner. It does not create a second semantic heartbeat or privately reimplement Experience lifecycle.
5. **Trusted materialization:** repository delivery and catalog resolution must preserve `BundleId + Version + ContentHash`; hash verification and trust decisions occur before a bundle is accepted. Do not load arbitrary browser-supplied assembly bytes.
6. **Proof:** tests must demonstrate that the requested manifest determines the composed bundles, the canonical owner advances the associated Experience, browser-visible state changes as a result, and failures do not leave orphaned execution.

## Migration gates

| Gate | Evidence required | Current status |
|---|---|---|
| Locate canonical execution owner | Existing shared source/API and lifecycle contract reviewed | **Not yet established by this WebApp audit** |
| Bind composition to execution | Test proves the returned assembly is what the owner executes | **Not proven** |
| Remove private heartbeat | Regression tests cover start/stop, progression, cancellation and disposal | **Blocked** |
| Replace Moniker compatibility bundle | Manifest-driven artifact identity and trusted materialization verified | **Not proven** |
| Browser journey | End-to-end test covers success and critical failures | **Not proven** |

## What would count as evidence

A diagram, interface name, successful `FsmCos.Execute`, or passing startup test is insufficient on its own. Evidence must connect the actual manifest and resolved bundle identities to the canonical execution owner and observable state transitions. Tests should fail if the assembly is composed but never advanced, if a different hard-coded FSM advances instead, or if execution continues after disposal.

## Current conclusion

The architectural direction is clear, but the canonical execution-owner contract has not yet been established by this audit. Keep the existing implementation labelled transitional, investigate the shared runtime/Experience contracts next, and do not claim manifest-driven execution or release readiness until the integration gates above are verified.
