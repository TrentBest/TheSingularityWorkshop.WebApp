# WebXR Manifestation

## Position

WebXR is a manifestation capability of the WebApp.

It is not a replacement for the Experience model, GUI semantic layer, FSM_COS, or AnyApp.

## Capability discovery

The browser should discover whether the current environment supports relevant XR modes.

Capability discovery is observational. The WebApp must not assume that a desktop browser, phone, headset, or browser version supports the same XR features.

## Session boundary

An XR session belongs to the browser manifestation.

A future implementation may map:

- Experience identity
- semantic presentation
- input events
- spatial observer state
- selected interaction state

into an XR session.

The XR API remains behind the WebApp/XR adapter.

## Observer model

The rendering architecture should be compatible with the broader semantic observer concept:

```
Observer
 ├── position
 ├── orientation
 ├── viewport
 ├── visible extent
 ├── detail horizon
 └── interaction horizon
```

These are semantic concepts. They should not be represented in GUI Core using headset-specific or browser-specific types.

## Detail and interaction horizons

An observer does not necessarily need the complete Experience representation.

The WebApp can progressively expose:

- what is visible
- what level of detail is useful
- what can be interacted with

This provides a conceptual basis for future LOD and interaction culling without leaking rendering-engine assumptions into the semantic layer.

## User activation

Entering immersive XR may require an explicit user gesture and browser permission/capability checks. The WebApp should therefore treat immersive entry as a user-driven transition rather than automatic startup.

## Future VR companion

A future headset client may coordinate with AnyApp.

The intended division is:

- headset: frame-critical presentation, spatial input, immediate interaction
- AnyApp: suitable local computation, artifact/runtime support, persistence, companion services

This division must be validated against actual hardware and application constraints rather than assumed.
