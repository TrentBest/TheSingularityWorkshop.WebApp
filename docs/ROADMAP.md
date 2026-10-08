# WebApp Roadmap

## Phase 0 — Theory

- [x] establish WebApp repository
- [x] define WebApp architectural position
- [x] define Experience manifestation theory
- [x] define WebApp ↔ AnyApp boundary
- [x] define initial WebXR boundary
- [ ] review theory against AnyApp, Experiences, FSM_COS, GUI, and MicroBundleRepository

## Phase 1 — MVP browser manifestation

- [ ] establish the actual WebApp application structure
- [ ] load published Experience identities
- [ ] present Experience discovery
- [ ] enter a published Experience
- [ ] preserve immutable artifact identity
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

## Phase 5 — Forge

The WebApp becomes a first-class Workshop access point for the Forge:

- discover
- compose
- publish
- inspect
- launch
- connect

Authoring functionality should arrive only after the underlying artifact and manifest boundaries are stable.

## Non-goals

The WebApp is not intended to:

- become the desktop runtime
- duplicate FSM_COS
- become a general code execution service
- require Unity
- make WebXR mandatory
- make AnyApp mandatory
- treat browser state as immutable Experience state
