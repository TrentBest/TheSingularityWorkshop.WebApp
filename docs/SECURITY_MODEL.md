# WebApp Security Model

## Principle

The browser is an untrusted execution environment and a powerful user-facing surface.

The WebApp must never turn convenience into authority.

## Boundaries

### Published content

Published Experience and MicroBundle identities are immutable references.

Use:

```
BundleId + Version + ContentHash
```

to address an artifact.

Never treat a display name or URL alone as sufficient artifact identity.

### AnyApp launch

The `anyapp://` protocol accepts launch intent, not arbitrary instructions.

AnyApp validates the requested identity and obtains artifacts through its own trusted path.

### Local bridge

The loopback bridge should use:

- Origin validation
- short-lived launch/session tokens
- protocol version validation
- message size limits
- bounded message types
- session state
- replay protection where applicable
- rate/resource limits
- explicit disconnect behavior

### XR

XR APIs are capability- and user-consent-driven. The WebApp should request only what the active Experience needs.

## Data minimization

Capability reports should contain only information needed for the current interaction.

Browser user-agent/platform information should not become a general-purpose fingerprinting database.

## No ambient authority

The WebApp should not be able to:

- read arbitrary desktop files
- invoke arbitrary desktop commands
- upload arbitrary assemblies to AnyApp
- alter local runtime configuration without an explicit protocol contract

## Failure behavior

Security failures should produce bounded, user-visible errors and terminate the affected operation.

Do not silently downgrade from a verified artifact to an unverified artifact.

## Threat model to test

Before calling the bridge production-ready, test at minimum:

- forged launch URIs
- missing/expired tokens
- token replay
- invalid Origin
- malformed JSON
- oversized messages
- unsupported protocol versions
- duplicate session messages
- disconnected peers
- artifact hash mismatch
- unavailable repository
- local-network permission denial
- browser refresh during an active session
