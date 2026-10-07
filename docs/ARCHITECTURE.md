# WebApp Architecture

## Purpose

**TheSingularityWorkshop.WebApp** is the browser manifestation of The Singularity Workshop.

It is a host and presentation surface. It is not the Experience itself, not the desktop runtime, and not a replacement for FSM_COS.

The current implementation deliberately proves the runtime path first:

```
WebApp host
   ↓
startup/runtime composition
   ↓
FSM_COS
   ↓
MicroBundleDomain contract
   ↓
host-owned bundle
   ↓
FSM_API-driven Experience
   ↓
browser manifestation
```

The browser is where the Experience becomes visible. It does not become the owner of the underlying runtime semantics.

## Responsibilities

The WebApp owns browser-native concerns:

- presentation and navigation;
- browser interaction;
- responsive viewport behavior;
- manifestation-specific rendering;
- public discovery and explanation;
- future browser capability integration.

It must not become:

- a second FSM_COS;
- an arbitrary code execution surface;
- the canonical source of Experience semantics;
- a replacement for MicroBundleRepository;
- a desktop runtime hidden behind HTTP.

## Current vertical slice

The current landing Experience demonstrates:

1. a responsive Workshop gateway;
2. an explicit entry transition;
3. an FSM_API-driven page lifecycle;
4. independently instantiated living actors sharing a processing group;
5. population growth;
6. moniker reveal;
7. gravity;
8. transition into the Workshop hub.

The important distinction is that **composition and execution are real**. The page is not merely animating a prerecorded mock.

## Dependency direction

```
WebApp
  ↓
FSM_COS
  ↓
MicroBundleDomain

WebApp
  ↓
FSM_API
```

The WebApp may consume external package contracts and capabilities. Those packages do not depend upward on the WebApp.

## Manifestation boundary

A future published Experience may have multiple manifestations:

```
             Experience
          /      |       \
      WebApp   AnyApp    MyVR
       browser desktop    XR
```

Shared identity does not require shared UI, renderer, operating-system API, or frame loop.

## Design principle

> The WebApp is a door into the Workshop, not the Workshop itself.
