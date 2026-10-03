# WebApp ↔ AnyApp Boundary

## Goal

Allow the browser manifestation to discover and open a published Experience in AnyApp and, when explicitly connected, exchange bounded runtime information.

## Launch

The current launch concept is:

```
anyapp://experience/{experienceId}/{version}/{contentHash}?token=...
```

The URI is a **launch mechanism**, not an authority mechanism.

AnyApp must independently validate the requested Experience against its repository/runtime rules.

## Bridge

The local bridge is a loopback WebSocket protocol.

The intended flow is:

1. WebApp identifies an immutable published Experience.
2. User explicitly chooses **Open in AnyApp**.
3. WebApp creates a short-lived launch token.
4. Browser invokes the registered `anyapp://` protocol.
5. AnyApp starts or locates its local bridge.
6. AnyApp validates the launch request.
7. WebApp connects to the loopback endpoint.
8. Both sides exchange versioned capability/session information.
9. Explicit events and lifecycle updates may flow across the bridge.

## Security boundary

The bridge must never become a general-purpose command channel.

Forbidden protocol responsibilities include:

- arbitrary process execution
- arbitrary filesystem access
- arbitrary assembly loading requested by the browser
- shell commands
- credential transfer
- unrestricted local-network access

The bridge should instead expose a small, versioned vocabulary of operations.

## Trust

Trust should be layered:

1. browser user gesture
2. launch-token validation
3. loopback transport
4. Origin allowlist
5. session binding
6. protocol version validation
7. Experience identity validation
8. artifact verification
9. bounded command vocabulary

Failure at any layer should fail closed.

## Browser restrictions

The implementation must account for browser security behavior around:

- secure contexts
- mixed content
- local network permissions
- WebSocket access to loopback
- user gesture requirements for privileged browser APIs

The protocol documentation therefore describes intended behavior; actual browser/device compatibility must be verified during integration.

## Connection semantics

Connection state should be visible to the user.

Suggested lifecycle:

```
Disconnected
   ↓
Launching
   ↓
Connecting
   ↓
Connected
   ↓
Synchronizing
   ↓
Ready
   ↓
Disconnected
```

An error should identify the boundary that failed rather than presenting a generic "something went wrong".

## Independence

Neither side should require the other merely to start.

This preserves the value of the browser as a distribution surface and AnyApp as a local runtime.

## Future direction

The bridge can eventually carry carefully defined Experience events, telemetry, viewport/capability observations, and synchronization state.

It should remain deliberately smaller than the runtime itself.
