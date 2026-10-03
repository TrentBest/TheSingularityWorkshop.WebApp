# Experience Manifestation Theory

## One Experience, multiple manifestations

The Workshop treats an Experience as an identity and composition boundary that can have multiple manifestations.

A manifestation is a concrete way of observing, interacting with, or supplying capabilities to that Experience.

Examples:

- WebApp manifestation
- AnyApp desktop manifestation
- MyVR VR manifestation

A manifestation may have capabilities unavailable to another manifestation. That does not make it a different Experience.

## Creator-owned requirements

The Experience creator declares the capability envelope the Experience requires.

The envelope may contain:

- required capabilities;
- preferred capabilities or execution locations;
- optional capabilities;
- delegable work;
- frame-critical work.

The platform does not impose a universal capability ladder.

An Experience may require MyVR alone, WebApp + MyVR, AnyApp, AnyApp + MyVR, WebApp + AnyApp, all three, or another supported arrangement.

The WebApp therefore participates in capability negotiation rather than assuming it is either the "lowest" or "highest" compute tier.

## Shared identity, not shared implementation

The manifestations should converge on:

- ExperienceId
- Version
- Runtime identity
- immutable MicroBundle artifact identities
- creator-declared requirements
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

## Capability-driven execution

The runtime arrangement is derived from:

1. the Experience's declared requirements;
2. the capabilities currently available;
3. authority and ownership rules;
4. latency constraints, especially for frame-critical work.

For an ambitious Experience, all manifestations may participate simultaneously:

```
                    Experience
                         |
          +--------------+--------------+
          |              |              |
       WebApp          AnyApp          MyVR
       browser         desktop         VR
          |              |              |
     browser APIs   local compute    frame-critical
     browser compute storage/GPU     pose/input
          \              |              /
           +------- coordinated --------+
```

This is a capability graph, not a fixed upgrade path.

## Co-entanglement

"Co-entangled" is architectural shorthand for coordinated manifestations of one environment.

It does **not** mean literal quantum entanglement.

A connected WebApp, AnyApp, and MyVR can exchange bounded protocol state while remaining independent processes/devices.

Either side may continue operating when another manifestation disappears, subject to the semantics of the current Experience.

## Authority

The browser is not authoritative merely because it initiated an action.

The desktop application is not authoritative merely because it can execute local code.

The VR client is not authoritative merely because it has immersive hardware.

Authority is assigned by the owning protocol and runtime boundary.

In particular:

- URLs identify resources and launch intents.
- manifests identify Experiences and artifacts.
- repositories provide published artifacts.
- FSM_COS composes runtime assemblies.
- AnyApp executes its local runtime.
- WebApp presents and interacts with the browser manifestation.
- MyVR presents and interacts with the VR manifestation.
- the bridge coordinates explicitly permitted cross-manifestation behavior.

## Failure is normal

Manifestations must tolerate partial failure.

Examples:

- WebApp is available but AnyApp is not installed.
- AnyApp is running but the browser tab closes.
- MyVR is available but a required desktop capability is not.
- XR is unavailable.
- repository discovery is temporarily unavailable.
- a published artifact cannot be resolved.
- a bridge session expires.

A manifestation should report these states rather than pretending that another manifestation exists.

If a required capability disappears, the Experience follows its declared failure semantics. Optional capabilities should degrade without corrupting semantic state.

## Session identity

A bridge session is distinct from Experience identity.

```
ExperienceId
    |
Experience Version
    |
Artifact identities
    |
Creator requirements
    |
Bridge Session
    |
Manifestation capabilities
```

This prevents a transient browser/desktop/VR connection from becoming part of the immutable Experience identity.

## Consequence

The same Experience can be:

- browsed without AnyApp;
- opened in AnyApp without XR;
- presented in MyVR without AnyApp when requirements permit;
- coordinated across WebApp and MyVR;
- coordinated across WebApp and AnyApp;
- coordinated across AnyApp and MyVR;
- coordinated across all three simultaneously.

The architecture therefore scales by adding manifestations and capabilities rather than duplicating the Experience.
