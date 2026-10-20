# WebApp Release Readiness

**Current estimate: 55% — not release-ready.** This is an engineering readiness estimate, not a test pass rate and not authorization to publish or deploy.

**Branch policy:** work on `development`; keep `master` untouched.  
**Current target:** consume `TheSingularityWorkshop.FSM_COS 0.1.0-alpha.7`.  
**Release/deployment:** not authorized.

## Verified baseline

- The latest source-integration workflow passed on 2026-10-20 (run [39513562614](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39513562614), commit `01f1c9c87c8435594b0cd90022448a3a2fc2dd83`). It passed source-to-UML inventory validation, all eight UML-validator regression cases, source-graph restore, dependency audit, Release build, and tests. The published-package build was skipped as expected on `development`. Earlier run [39513296978](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39513296978) is retained as regression-case evidence. Earlier run [39335815591](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39335815591) reported no vulnerable packages in the restored WebApp application/test source graph; that finding is limited to that graph and run.
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

See the source-grounded [Experience Execution Boundary Audit](EXPERIENCE_EXECUTION_BOUNDARY_AUDIT.md) for the verified FSM_COS/WebApp responsibility boundary and migration gates. The audit does not claim to have located or implemented a canonical execution owner.

- [ ] Remove the host-owned duplicate landing-Experience implementation from `WebAppExperienceRuntime` once the canonical Experience runtime is consumable.
- [ ] Eliminate the runtime's private background heartbeat as the owner of semantic lifecycle; execution must follow the canonical FSM/process-group contract and have explicit ownership.
- [ ] Replace the fixed host-owned Moniker compatibility bundle with a verified canonical artifact.
- [ ] Demonstrate that composition is followed by actual execution of the composed Experience, not merely a successful `RuntimeAssembly` composition.
- [ ] Preserve deterministic, observable lifecycle behavior and add regression coverage before removing compatibility code.

### 3. Artifact and browser journey

- [ ] Load and validate published Experience manifests and immutable artifact identities.
- [ ] Define/consume the canonical trusted artifact materialization contract: MicroBundleRepository returns hash-verified artifact bytes, while FSM_COS catalogs resolve instantiated `IMicroBundle` objects. The bridge from bytes to a trusted bundle is not proven by these interfaces alone; do not substitute arbitrary assembly loading.
- [ ] Verify content hashes before use and fail closed on mismatch or repository failure.
- [ ] Add browser-level end-to-end coverage for the visitor journey and critical error states.
- [ ] Test malformed/forged launch requests, oversized messages, origin validation, session expiry/replay, and disconnect behavior before calling the AnyApp bridge production-ready.
- [ ] Verify the intended experience on supported browser targets; do not claim a public deployment until one is actually verified.

### 4. Documentation and release discipline

- [x] Document the current implementation and remaining artifact-driven migration honestly.
- [x] Keep readiness ledger synchronized with the latest verified source-integration CI evidence; continue updating it as implementation and CI change.
- [ ] Recheck README, architecture, roadmap, and security claims against the shipped behavior.
- [ ] Complete the repository's release checklist and review all changes on `development`.
- [ ] Obtain explicit release authorization before any publication or deployment.

## Purpose correction: AnyApp for the web

The product intent is more precise: **WebApp is AnyApp for the web.** It is a web host in its own right, takes web-based Experience content and browser input, and composes that content through FSM_COS. The Windows/WPF AnyApp executable is a sibling manifestation of the same host role, not a required service or project dependency for WebApp. WebApp should not forward its core execution to the desktop bridge.

The existing AnyApp `ExperienceManifest` and `AnyAppRuntime.Compose` / `ComposeAsync` show useful manifest/composition patterns, but currently live in the `net8.0-windows` WPF `WinExe` project. `AnyAppWebBridge` is a local presentation bridge hosted by the desktop app, not WebApp's canonical runtime. The architectural task is to make the relevant manifest/request-result contract host-neutral as needed, then have WebApp resolve web-delivered artifacts and compose them directly through FSM_COS. A deeper contract gap is now recorded: the repository exposes verified artifact bytes, while `IMicroBundleCatalog` resolves instantiated `IMicroBundle` objects; no trusted byte-to-bundle materialization contract has yet been established in this audit. Do not bridge that gap with arbitrary browser-supplied assembly loading. README, architecture, implementation notes, roadmap, security model, optional-companion boundary, documentation index, and runtime SVG now consistently state the intended direction. The UML no longer implies that the web host's canonical execution path is a handoff into the desktop AnyApp runtime.

The current implementation contradicts that target: `WebAppCosRuntime` composes only a local Moniker bundle, while `WebAppExperienceRuntime` owns page/actor FSMs, a 100-actor demonstration, and a private heartbeat. These are prototype paths, not manifest-driven web content composition. Replacing them with the real web-host path is the primary migration blocker, not merely cleanup.

## Current technical risk

The current `WebAppExperienceRuntime` creates page and actor FSMs in host code, preallocates 100 actor contexts, and starts a private background thread that calls `fsm_API.Interaction.Update` on a 33 ms loop. This is a working vertical slice, but it is not proof that a canonical, manifest-selected Experience owns its lifecycle. The local Moniker bundle and fixed landing simulation are also explicitly transitional in the implementation notes and roadmap.

Do not increase readiness merely because the dependency version or workflow hardening changes. The corrected audit step is now verified, but readiness remains 55% because the central runtime/ownership migration, public-package validation, and browser-level security/journey tests are still open. Raise the estimate only when the corresponding release gate is verified with code, tests, and CI evidence.


## Living documentation and visual audit

The documentation inventory includes the source-grounded `webapp-anyapp-runtime-uml.svg`, `bridge-security-boundary.svg`, and `webapp-anyapp-bridge.svg`. The UML distinguishes observed code from the intended WebApp-owned web-manifest → FSM_COS → browser-result path. The docs now consistently describe the desktop bridge as optional companion behavior. This closes the current wording/diagram inconsistency, not the full living-documentation audit: visuals must stay synchronized with public contracts and remaining documents still need a visual/topic suitability review.

## Source-to-UML documentation status

The initial manual inventory is in [`docs/UML/README.md`](UML/README.md). It maps the current repository C# source units to reviewed Markdown pages and SVG class cards, including the nested `ActorContext`, `ActorPhase`, and `PageContext` types.

This is a documentation foundation, **not a completed automated documentation gate**:
- the source inventory and diagrams are hand-authored;
- CI runs a Python standard-library check for C# source-file inventory coverage, existing source paths, a documentation link for each inventory row, UML SVG links for non-entry-point rows, local Markdown links in `docs/UML`, and SVG `title`/`desc` metadata. The expanded inventory gate and all eight deliberate-defect cases passed in [workflow run 39513296978](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39513296978), including the two cases for missing and nonexistent UML SVG links.
- no Roslyn-backed declaration/type drift check or diagram regeneration exists yet;
- the current SVGs are manually authored class cards, not generated from a canonical UML model;
- Razor components, JavaScript, CSS, and cross-package types are outside this first C# inventory.

Keep the readiness estimate at **55%**. The latest full source-integration gate and all eight deliberate-defect cases are verified by [workflow run 39513562614](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39513562614), along with source-graph restore, dependency audit, Release build, and tests. Earlier regression evidence is available in [run 39513296978](https://github.com/TrentBest/TheSingularityWorkshop.WebApp/actions/runs/39513296978). The additional cases for missing UML diagram links and missing diagram targets are included in that successful run. This remains a narrow file/link/SVG-metadata gate, not Roslyn declaration drift detection or diagram generation. Adding documentation does not close runtime ownership, public-package compatibility, or browser security/journey blockers. Do not call the overall documentation requirement complete until declaration-to-diagram consistency and the remaining source types are addressed.
