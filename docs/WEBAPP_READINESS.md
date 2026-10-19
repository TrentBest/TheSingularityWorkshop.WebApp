# WebApp Release Readiness

**Current estimate: 58% — not release-ready.** This is an engineering readiness estimate, not a test pass rate and not authorization to publish or deploy.

**Branch policy:** work on `development`; keep `master` untouched.  
**Current target:** consume `TheSingularityWorkshop.FSM_COS 0.1.0-alpha.7`.  
**Release/deployment:** not authorized.

## Verified baseline

- The existing development build workflow last completed successfully on 2026-10-18 (run 152, commit `86f3875`).
- The application targets .NET 8 and treats compiler warnings as errors.
- The solution has a test project covering FSM_COS startup composition, Moniker structure, preallocated actor contexts, and FSM-driven entry.
- The public-feed package reference has been changed from FSM_COS `0.1.0-alpha.5` to `0.1.0-alpha.7`.
- Third-party GitHub Actions in the build workflow have been changed from movable version tags to the verified immutable commit pins used by the FSM_COS development workflow.
- CI evidence: [run 153](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39327389272) and [run 154](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39327412289) confirm that package-based restore currently fails because alpha.7 is not published; the source-integration job in run 154 restored successfully and is building the development source graph.

## Release gates

### 1. Build and dependency integrity

- [x] Pin GitHub Actions to immutable commit SHAs.
- [x] Align the published-package path to FSM_COS alpha.7.
- [ ] Verify public NuGet restore/build/test against alpha.7 in CI. **Current blocker:** run 153 and run 154 failed package restore because NuGet.org does not yet contain FSM_COS alpha.7 (nearest published version reported by CI: alpha.6). Do not publish it from WebApp; validate the development source graph while alpha.7 remains unpublished.
- [ ] Add a direct and transitive NuGet vulnerability audit that fails on findings.
- [ ] Verify the source-integration workflow builds and tests against current development dependencies.
- [ ] Verify warning-free Release build and complete test results after the latest commits.

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

## Current technical risk

The current `WebAppExperienceRuntime` creates page and actor FSMs in host code, preallocates 100 actor contexts, and starts a private background thread that calls `fsm_API.Interaction.Update` on a 33 ms loop. This is a working vertical slice, but it is not proof that a canonical, manifest-selected Experience owns its lifecycle. The local Moniker bundle and fixed landing simulation are also explicitly transitional in the implementation notes and roadmap.

Do not increase readiness merely because the dependency version or workflow hardening changes. Raise the estimate only when the corresponding release gate is verified with code, tests, and CI evidence.
