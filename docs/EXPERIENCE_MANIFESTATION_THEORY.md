# Experience Manifestation Theory

## One Experience, multiple manifestations

The Workshop treats an Experience as an identity and composition boundary that can have multiple manifestations.

A manifestation is a concrete way of observing and interacting with that Experience.

Examples:

- WebApp manifestation
- AnyApp desktop manifestation
- future VR manifestation

A manifestation may have capabilities unavailable to another manifestation. That does not make it a different Experience.

## Shared identity, not shared implementation

The manifestations should converge on:

- ExperienceId
- Version
- Runtime identity
- immutable MicroBundle artifact identities
- session identity when connected
- relevant lifecycle state
- declared capabilities

They do not need to share:

- UI controls
- rendering engines
- operating-system APIs
- input systems
- frame loops
- platform-specific resource models

This is intentional.

## Co-entanglement

"Co-entangled" is architectural shorthand for coordinated manifestations of one environment.

It does **not** mean literal quantum entanglement.

A connected WebApp and AnyApp can exchange:

- identity
- capabilities
- lifecycle state
- heartbeat
- explicit events
- user-requested commands within a constrained protocol

Either side may continue operating when the other disappears, subject to the semantics of the current Experience.

## Authority

The browser is not authoritative merely because it initiated an action.

The desktop application is not authoritative merely because it can execute local code.

Authority is assigned by the owning protocol and runtime boundary.

In particular:

- URLs identify resources and launch intents.
- manifests identify Experiences and artifacts.
- repositories provide published artifacts.
- FSM_COS composes runtime assemblies.
- AnyApp executes its local runtime.
- WebApp presents and interacts with the browser manifestation.
- the bridge coordinates explicitly permitted cross-manifestation behavior.

## Failure is normal

Manifestations must tolerate partial failure.

Examples:

- WebApp is available but AnyApp is not installed.
- AnyApp is running but the browser tab closes.
- XR is unavailable.
- repository discovery is temporarily unavailable.
- a published artifact cannot be resolved.
- a bridge session expires.

A manifestation should report these states rather than pretending that another manifestation exists.

## Session identity

A bridge session is distinct from Experience identity.

```
ExperienceId
    |
Experience Version
    |
Artifact identities
    |
Bridge Session
    |
Manifestation capabilities
```

This prevents a transient browser/desktop connection from becoming part of the immutable Experience identity.

## Consequence

The same Experience can be:

- browsed without AnyApp
- opened in AnyApp without XR
- presented in XR without desktop execution
- coordinated across browser and desktop when both are available

The architecture therefore scales by adding manifestations rather than duplicating the Experience.
