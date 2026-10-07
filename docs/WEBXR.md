# WebXR Manifestation

## Position

WebXR is a future manifestation capability of the WebApp.

It is not a replacement for the Experience model, GUI semantic layer, FSM_COS, or AnyApp.

## Capability discovery

The browser should discover whether the current environment supports relevant XR modes.

Capability discovery is observational. The WebApp must not assume that a desktop browser, phone, headset, or browser version supports identical XR features.

## Session boundary

An XR session belongs to the browser manifestation.

A future implementation may map:

- Experience identity;
- semantic presentation;
- input events;
- spatial observer state;
- selected interaction state.

into an XR session.

The XR API remains behind the WebApp/XR adapter.

## Observer model

The broader rendering architecture can expose semantic concepts such as:

```
Observer
 ├── position
 ├── orientation
 ├── viewport
 ├── visible extent
 ├── detail horizon
 └── interaction horizon
```

These concepts should remain platform-neutral.

## User activation

Entering immersive XR may require explicit user activation and browser permission/capability checks. Treat immersive entry as a user-driven transition rather than automatic startup.

## Future companion

A future headset client may coordinate with AnyApp.

The intended division is:

- headset: frame-critical presentation, spatial input, immediate interaction;
- AnyApp: suitable local computation, artifact/runtime support, persistence, companion services.

This must be validated against actual target hardware rather than assumed.
