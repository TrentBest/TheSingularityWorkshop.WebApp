# Implementation Notes

Implementation should extend the existing runtime architecture rather than grow a second architecture inside the WebApp.

## Current implementation boundary

The current host uses:

- .NET 8 Razor Components;
- FSM_API 1.0.13 for runtime execution;
- FSM_COS alpha.5 as the verified consumer boundary;
- MicroBundleDomain 1.0.1 for MicroBundle contracts;
- GUI packages where the host integration requires them.

The WebApp currently retains a host-local Moniker compatibility bundle because the canonical Experiences Moniker artifact is not yet the published runtime dependency.

## Runtime discipline

FSM_API execution is deferred where the API requires deferred processing. Tests must honor that lifecycle rather than forcing production code into synchronous behavior.

Every living actor is an independent FSM instance. Actors may share a processing group without sharing state.

## Future repository integration

Published Experience manifests should reference immutable artifact identities.

Do not introduce a second ad-hoc artifact-addressing scheme inside WebApp.

## Rendering

Browser rendering belongs to the WebApp manifestation. Shared semantic contracts should remain free of browser-specific, WPF, WinUI, Unity, or renderer-engine types.

## Testing

Tests should emphasize architectural boundaries:

- runtime composition;
- manifest identity;
- FSM lifecycle;
- independent actor behavior;
- deferred transition behavior;
- responsive presentation assumptions;
- future artifact identity preservation;
- malformed external input.

Integration tests should distinguish browser limitations from protocol correctness.
