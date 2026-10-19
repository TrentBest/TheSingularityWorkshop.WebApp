# WebApp Release Readiness

**Current estimate: 55% — not release-ready.** This is an engineering readiness estimate, not a test pass rate and not authorization to publish or deploy.

**Branch policy:** work on `development`; keep `master` untouched.  
**Current target:** consume `TheSingularityWorkshop.FSM_COS 0.1.0-alpha.7`.  
**Release/deployment:** not authorized.

## Verified baseline

- The latest source-integration workflow passed restore, dependency audit, Release build, and tests on 2026-10-19 (run [39335815591](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39335815591), commit `0b29db2c`). The audit reported no vulnerable packages for the WebApp application and test projects in the restored development source graph. The Release build succeeded and all 4 tests passed.
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
- [x] Verify direct and transitive NuGet vulnerability auditing against the restored development source graph. Run [39335815591](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39335815591) reports no vulnerable packages for the WebApp application and test projects. This is a source-graph result, not a guarantee of security or a substitute for auditing the eventual published-package graph.
- [x] Re-verify the source-integration workflow after fixing the audit command. Run [39335815591](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39335815591) passed source restore, dependency audit, Release build, and all 4 tests.
- [ ] Verify a warning-free Release build and test completion. Run [39335144122](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39335144122) passed build/tests, but emitted two XML-documentation warnings from the checked-out FSM_API source (`CS1573` and `CS1591`). The source graph is not warning-free yet; determine ownership and fix at the appropriate source rather than suppressing the warnings.

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

The product intent is more precise: **WebApp is AnyApp for the web.** It is a web host in its own right, takes web-based Experience content and browser input, and composes that content through FSM_COS. The Windows/WPF AnyApp executable is a sibling manifestation of the same host role, not a required service or project dependency for WebApp. WebApp should not forward its core execution to the desktop bridge.

The existing AnyApp `ExperienceManifest` and `AnyAppRuntime.Compose` / `ComposeAsync` show useful manifest/composition patterns, but currently live in the `net8.0-windows` WPF `WinExe` project. `AnyAppWebBridge` is a local presentation bridge hosted by the desktop app, not WebApp's canonical runtime. The architectural task is to make the relevant manifest/request-result contract host-neutral as needed, then have WebApp resolve web-delivered artifacts and compose them directly through FSM_COS. README and architecture documentation now explicitly state this intended direction.

The current implementation contradicts that target: `WebAppCosRuntime` composes only a local Moniker bundle, while `WebAppExperienceRuntime` owns page/actor FSMs, a 100-actor demonstration, and a private heartbeat. These are prototype paths, not manifest-driven web content composition. Replacing them with the real web-host path is the primary migration blocker, not merely cleanup.

## Current technical risk

The current `WebAppExperienceRuntime` creates page and actor FSMs in host code, preallocates 100 actor contexts, and starts a private background thread that calls `fsm_API.Interaction.Update` on a 33 ms loop. This is a working vertical slice, but it is not proof that a canonical, manifest-selected Experience owns its lifecycle. The local Moniker bundle and fixed landing simulation are also explicitly transitional in the implementation notes and roadmap.

Do not increase readiness merely because the dependency version or workflow hardening changes. The corrected audit step is now verified, but readiness remains 55% because the central runtime/ownership migration, public-package validation, and browser-level security/journey tests are still open. Raise the estimate only when the corresponding release gate is verified with code, tests, and CI evidence.


## Living documentation and visual audit

The documentation inventory now includes a source-grounded UML-style SVG, `webapp-anyapp-runtime-uml.svg`, alongside `bridge-security-boundary.svg` and `webapp-anyapp-bridge.svg`. It maps observed WebApp and AnyApp types, members, and relationships, and distinguishes implemented dependencies from the intended-but-unproven request/result seam. This closes the first UML inventory gap, not the full living-documentation audit: the diagram must stay synchronized with public contracts and the remaining documents still need a visual/topic suitability review.
