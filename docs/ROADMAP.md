# WebApp Roadmap

## Phase 0 — Theory and implementation baseline

- [x] establish WebApp repository
- [x] define WebApp architectural position
- [x] define Experience manifestation theory
- [x] define WebApp ↔ AnyApp boundary
- [x] define initial WebXR boundary
- [x] establish a .NET 8 ASP.NET Core Razor Components host
- [x] compose a host-owned Moniker bundle through FSM_COS
- [x] present the composed Moniker through the GUI.Blazor rendering boundary
- [x] run the landing Experience lifecycle and living actors through FSM_API
- [ ] review theory against AnyApp, Experiences, FSM_COS, GUI, and MicroBundleRepository

## Phase 1 — Artifact-driven browser manifestation

- [x] establish the application structure and browser presentation path
- [x] enter the fixed landing Experience through the Gateway
- [x] preserve FSM_API progression and FSM_COS composition boundaries in the current slice
- [ ] replace the temporary host-owned Moniker compatibility bundle with a verified canonical artifact
- [ ] load published Experience identities and immutable artifact addresses
- [ ] present Experience discovery
- [ ] enter a selected published Experience
- [ ] preserve and verify immutable artifact identity end-to-end
- [ ] add explicit **Open in AnyApp**
- [ ] show AnyApp connection state
- [ ] show shared Experience/session identity
- [ ] exchange heartbeat and bounded events
- [ ] handle unavailable companion/repository states cleanly

## Phase 2 — Repository-backed Experiences

- [ ] resolve immutable MicroBundle artifact addresses
- [ ] consume published Experience manifests
- [ ] remove assumptions about AnyApp's compiled-in bundle catalog
- [ ] verify artifact hashes before use
- [ ] make repository failures observable

## Phase 3 — Semantic presentation

- [ ] consume GUI semantic representation where appropriate
- [ ] keep browser rendering outside GUI Core
- [ ] establish semantic observer/view concepts
- [ ] introduce progressive detail where evidence supports it
- [ ] preserve deterministic Experience identity

## Phase 4 — XR

- [ ] capability discovery
- [ ] user-initiated immersive entry
- [ ] semantic observer mapping
- [ ] spatial interaction events
- [ ] browser/desktop synchronization
- [ ] test against actual target hardware

## Phase 5 — Forge and authored webpages

WebApp is the browser manifestation and presentation surface. It must be able to consume a page/Experience artifact produced by Workshop authoring; authoring should not be faked by adding more fixed branches to `Home.razor` or `WebAppExperienceRuntime`.

- define the page artifact contract using canonical Workshop APIs
- let WebPage/Workshop authoring create and configure a manifest-backed page
- compose its declared capabilities through FSM_COS
- preview the resulting Experience through WebApp's existing browser presentation path
- discover and inspect published Experiences
- publish only after artifact identity, authorization, and repository boundaries are proven
- launch and connect to AnyApp where the Experience declares that capability

Authoring and publication must follow stable artifact and manifest boundaries; the current fixed landing Experience is a foundation, not a general page builder.

## Non-goals

The WebApp is not intended to:

- become the desktop runtime
- duplicate FSM_COS
- become a general code execution service
- require Unity
- make WebXR mandatory
- make AnyApp mandatory
- treat browser state as immutable Experience state
