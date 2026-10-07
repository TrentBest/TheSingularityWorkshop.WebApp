# Experience Manifestation Theory

## One Experience, multiple manifestations

An Experience is an identity and composition boundary that can be manifested through different clients.

Examples include:

- WebApp browser manifestation;
- AnyApp desktop manifestation;
- future MyVR manifestation.

A manifestation may have capabilities unavailable to another manifestation without becoming a different Experience.

## Shared identity, not shared implementation

Manifestations should converge on concepts such as:

- Experience identity;
- version;
- immutable artifact identity;
- creator-declared requirements;
- relevant lifecycle state;
- declared capabilities.

They do not need to share:

- UI controls;
- rendering engines;
- operating-system APIs;
- input systems;
- frame loops;
- platform-specific resource models.

This separation is intentional.

## Capability-driven execution

The eventual runtime arrangement should be derived from:

1. the Experience's declared requirements;
2. capabilities currently available;
3. authority and ownership rules;
4. latency constraints, especially for frame-critical work.

This is a capability graph, not a fixed hierarchy in which the browser is automatically the least capable participant.

## Authority

A browser is not authoritative merely because it initiated an action.

A desktop application is not authoritative merely because it can execute local code.

Authority belongs to the owning protocol and runtime boundary.

The intended division is:

- manifests identify Experiences and requested artifacts;
- repositories provide published artifacts;
- FSM_COS composes runtime assemblies;
- hosts execute and manifest them;
- the WebApp presents the browser manifestation.

## Partial failure is normal

Manifestations must tolerate the absence of optional companions or capabilities.

Examples:

- WebApp is available while AnyApp is not installed;
- a browser session closes while a local runtime continues;
- XR is unavailable;
- repository discovery is temporarily unavailable;
- an artifact cannot be resolved.

The user should see the boundary that failed rather than receiving a misleading claim that the entire Experience disappeared.

## Session identity

A transient browser/desktop connection is distinct from immutable Experience identity.

```
Experience identity
      ↓
Artifact identities
      ↓
Creator requirements
      ↓
Session identity
      ↓
Manifestation capabilities
```

That separation prevents a temporary connection from becoming part of the permanent Experience definition.
