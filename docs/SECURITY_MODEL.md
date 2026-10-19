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

### Web-hosted Experience execution

WebApp is the web manifestation of the AnyApp host role. It validates the requested Experience manifest, immutable artifact identities, and authorization policy before resolving web-delivered artifacts and composing through FSM_COS. Browser input is intent, not authority to load arbitrary assemblies, execute commands, or bypass artifact validation.

The web host must not depend on the desktop AnyApp process for its own runtime composition. Any shared manifest/request-result contract should be host-neutral and must not drag WPF or desktop lifecycle into WebApp.

### Optional desktop launch and local bridge

The `anyapp://` protocol is an optional desktop-companion launch path for Experiences that explicitly request it. It accepts launch intent, not arbitrary instructions. It is not the canonical execution path for ordinary WebApp Experiences.

AnyApp validates the requested identity and obtains artifacts through its own trusted path.

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
