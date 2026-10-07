# WebApp ↔ AnyApp Boundary

## Purpose

The browser and desktop runtime are complementary manifestations.

The WebApp is strong at reach, presentation, interaction, and browser capabilities. AnyApp is strong at local execution and capabilities unavailable to a browser.

The boundary between them must remain a protocol boundary, not a remote shell.

## Intended launch flow

1. WebApp identifies an immutable published Experience.
2. The user explicitly chooses to open it in AnyApp.
3. A bounded launch request is created.
4. AnyApp independently validates the requested identity.
5. AnyApp resolves the artifact through its trusted path.
6. A bounded session may be established if the Experience requires coordination.

The exact URI and transport are implementation details and remain future work.

## Forbidden responsibilities

The bridge must not become a channel for:

- arbitrary process execution;
- arbitrary filesystem access;
- arbitrary assembly loading requested by the browser;
- shell commands;
- credential transfer;
- unrestricted local-network access.

## Independence

Neither side should require the other merely to start.

This preserves the browser as a distribution surface and AnyApp as an independent local runtime.

## Future session lifecycle

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

Connection state should be visible to the user.

The bridge should remain deliberately smaller than the runtime itself.
