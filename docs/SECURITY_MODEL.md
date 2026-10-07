# WebApp Security Model

## Principle

The browser is a powerful user-facing surface, but it must not gain authority merely because it is convenient.

> Convenience must never become ambient authority.

## Published content

When repository-backed Experiences are integrated, immutable content should be addressed by explicit artifact identity such as:

```
BundleId + Version + ContentHash
```

A display name or URL alone is not sufficient artifact identity.

## Companion launch

A future AnyApp launch protocol should carry launch intent, not arbitrary instructions.

AnyApp must independently validate requested Experience identity and obtain artifacts through its own trusted path.

## Local bridge

A future browser/desktop bridge should establish explicit boundaries including:

- origin validation;
- short-lived launch/session tokens;
- protocol version validation;
- bounded message types;
- message-size limits;
- session state;
- replay protection where applicable;
- rate/resource limits;
- explicit disconnect behavior.

## No ambient authority

The WebApp should never be able to request:

- arbitrary desktop commands;
- arbitrary filesystem access;
- arbitrary assembly loading;
- credential transfer;
- unrestricted local-network operations.

## Failure behavior

Security failures should be bounded, visible to the user, and fail closed.

Do not silently downgrade from a verified artifact to an unverified artifact.

## Threat model

Before any companion bridge is considered production-ready, test at minimum:

- forged launch requests;
- expired or replayed tokens;
- invalid origins;
- malformed messages;
- oversized messages;
- unsupported protocol versions;
- duplicate session messages;
- disconnected peers;
- artifact identity mismatch;
- unavailable repositories;
- browser refresh during a session.
