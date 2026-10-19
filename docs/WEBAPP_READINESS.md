# WebApp Release Readiness

**Current estimate: 55% — not release-ready.** This is an engineering readiness estimate, not a test pass rate and not authorization to publish or deploy.

**Branch policy:** work on `development`; keep `master` untouched.  
**Current target:** consume `TheSingularityWorkshop.FSM_COS 0.1.0-alpha.7`.  
**Release/deployment:** not authorized.

## Verified baseline

- The source-integration workflow passed restore, Release build, and tests on 2026-10-19 (run 158, commit `41c335c`). Later audit-workflow validation is currently blocked because `dotnet list package` attempts to restore the public-package graph instead of inspecting the already-restored source graph.
- The application targets .NET 8 and treats compiler warnings as errors.
- The solution has a test project covering FSM_COS startup composition, Moniker structure, preallocated actor contexts, and FSM-driven entry.
- The public-feed package reference has been changed from FSM_COS `0.1.0-alpha.5` to `0.1.0-alpha.7`.
- Third-party GitHub Actions in the build workflow have been changed from movable version tags to the verified immutable commit pins used by the FSM_COS development workflow.
- CI evidence: [run 153](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39327389272) and [run 154](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39327412289) confirm that package-based restore currently fails because alpha.7 is not published. [Run 158](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39327750610) passed source restore, Release build, and tests.

## Release gates

### 1. Build and dependency integrity

- [x] Pin GitHub Actions to immutable commit SHAs.
- [x] Align the published-package path to FSM_COS alpha.7.
- [ ] Verify public NuGet restore/build/test against alpha.7 in CI. **Current blocker:** run 153 and run 154 failed package restore because NuGet.org does not yet contain FSM_COS alpha.7 (nearest published version reported by CI: alpha.6). Do not publish it from WebApp; validate the development source graph while alpha.7 remains unpublished.
- [ ] Verify direct and transitive NuGet vulnerability auditing against the restored graph. Run 162 failed because the audit command re-triggered public-package restore and hit the known unpublished FSM_COS alpha.7 dependency; this is a workflow invocation problem, not yet evidence of a vulnerability finding.
- [ ] Re-verify the source-integration workflow after fixing the audit command. Run 158 (commit `41c335c`) passed restore/build/tests, but run 162 did not reach build/test because the audit step failed.
- [ ] Re-verify warning-free Release build and test completion on the latest workflow revision; run 158 is valid historical evidence only.

### 2. Runtime and architectural ownership

- [ ] Remove the host-owned duplicate landing-Experience implementation from `WebAppExperienceRuntime` once the canonical Experience runtime is consumable.
- [ ] Eliminate the runtime's private background heartbeat as the owner of semantic lifecycle; execution must follow the canonical FSM/process-group contract and have explicit ownership.
- [ ] Replace the fixed host-owned Moniker compatibility bundle with a verified canonical artifact.
- [ ] Demonstrate that composition is followed by actual execution of the composed Experience, not merely a successful `RuntimeAssembly` composition.
- [ ] Preserve deterministic, observable lifecycle behavior and add regression coverage before removing compatibility code.

### 3. Artifact and browser journey

- [ ] Load and validate published Experience manifests and immutable artifact identities.
- [ ] Verify content hashes before use and fail closed on mismatch or repository failure.
- [ ] Add browser-level end-to-end coverage for the visitor journey and critical error states.
- [ ] Test malformed/forged launch requests, oversized messages, origin validation, session expiry/replay, and disconnect behavior before calling the AnyApp bridge production-ready.
- [ ] Verify the intended experience on supported browser targets; do not claim a public deployment until one is actually verified.

### 4. Documentation and release discipline

- [x] Document the current implementation and remaining artifact-driven migration honestly.
- [ ] Keep readiness ledger synchronized with CI evidence and implementation changes.
- [ ] Recheck README, architecture, roadmap, and security claims against the shipped behavior.
- [ ] Complete the repository's release checklist and review all changes on `development`.
- [ ] Obtain explicit release authorization before any publication or deployment.

## Purpose correction: AnyApp for the web

The intended purpose is now explicit: WebApp is the browser-facing counterpart to AnyApp, supplying web user input, browser events/capabilities, and presentation around FSM_COS. It is not intended to become a separate showcase application or own Experience lifecycle. The README and architecture guide now state this boundary and show the intended input → application intent → FSM_COS → observable result flow.

The current implementation contradicts that target: `WebAppExperienceRuntime` owns page/actor FSMs, a 100-actor demonstration, and a private heartbeat. That is now tracked as a purpose-level migration blocker, not merely a cleanup item.

## Current technical risk

The current `WebAppExperienceRuntime` creates page and actor FSMs in host code, preallocates 100 actor contexts, and starts a private background thread that calls `fsm_API.Interaction.Update` on a 33 ms loop. This is a working vertical slice, but it is not proof that a canonical, manifest-selected Experience owns its lifecycle. The local Moniker bundle and fixed landing simulation are also explicitly transitional in the implementation notes and roadmap.

Do not increase readiness merely because the dependency version or workflow hardening changes. Raise the estimate only when the corresponding release gate is verified with code, tests, and CI evidence.


## Living documentation and visual audit

A first-pass repository inventory found 9 Markdown documents under `docs/` and 2 SVG assets under `docs/assets/`. The current SVGs are `bridge-security-boundary.svg` and `webapp-anyapp-bridge.svg`. This is an inventory count, not a claim that every document needs a separate diagram: the next audit must map each document and complicated subject to an appropriate living visual, and check whether class/API relationships need UML-style data-card diagrams (namespace, type kind/name, members, and relationships). No UML class diagram was found in the current docs asset inventory. This documentation gate remains open.
