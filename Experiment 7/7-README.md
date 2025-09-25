# Unity VFX Assignment: Water, Smoke, and Sparks with Particle Systems

## Overview

This repository contains a Unity project demonstrating the creation of **water**, **smoke**, and **sparks** visual effects using Unity's built-in Particle System (also referred to as nParticles in some contexts). The project is structured as a lab assignment suitable for students learning VFX fundamentals in Unity.

---

## Table of Contents

- [Project Structure](#project-structure)
- [Requirements](#requirements)
- [Setup Instructions](#setup-instructions)
- [Step-by-Step Guide](#step-by-step-guide)
  - [1. Water Effect](#1-water-effect)
  - [2. Smoke Effect](#2-smoke-effect)
  - [3. Sparks Effect](#3-sparks-effect)
- [Screenshots](#screenshots)
- [Credits](#credits)

---

## Project Structure

```
VFXSubmission06/
├── Assets/
│   ├── Scenes/
│   ├── Prefabs/
│   ├── Materials/
│   ├── Textures/
│   └── Scripts/
├── ProjectSettings/
├── README.md
└── ...
```

---

## Requirements

- **Unity Editor** (version 2021.3 LTS or newer recommended)
- Standard Unity 3D project setup

---

## Setup Instructions

1. **Clone or Download** this repository.
2. Open the project in Unity.
3. Navigate to the `Scenes` folder and open the main scene (e.g., `VFXLab.unity`).
4. Press **Play** to preview the effects.

---

## Step-by-Step Guide

### 1. Water Effect

1. **Create a Plane**:
    - In the Hierarchy: `Right-click > 3D Object > Plane` (acts as the ground).
2. **Add Particle System**:
    - `Right-click > Effects > Particle System`
    - Rename to `WaterParticles`.
3. **Configure**:
    - **Shape**: Cone
    - **Start Lifetime**: 2–4
    - **Start Speed**: 3–5
    - **Start Size**: 0.1–0.2
    - **Start Color**: Light Blue (e.g., RGBA 0.4, 0.7, 1, 0.8)
    - **Gravity Modifier**: 0.5–1
4. **Renderer**:
    - Use a transparent droplet/circle texture.
    - Set material shader to `Particles/Standard Unlit` with fade or transparent rendering.

#### Optional: Add a Splash
- Duplicate the particle system, set a short lifetime and burst emission for splash at impact.

---

### 2. Smoke Effect

1. **Add Particle System**:
    - `Right-click > Effects > Particle System`
    - Rename to `SmokeParticles`.
2. **Configure**:
    - **Shape**: Cone or Sphere
    - **Start Lifetime**: 2–4
    - **Start Speed**: 0.5–1.5
    - **Start Size**: 0.5–1.5
    - **Start Color**: Gray (fading to transparent)
    - **Gravity Modifier**: 0
    - **Color over Lifetime**: Set gradient from gray to transparent.
    - **Size over Lifetime**: Curve from small to large.
    - **Noise Module**: Enable and adjust strength for turbulence.
3. **Renderer**:
    - Use a soft circle or smoke texture with a fade material.

---

### 3. Sparks Effect

1. **Add Particle System**:
    - `Right-click > Effects > Particle System`
    - Rename to `SparksParticles`.
2. **Configure**:
    - **Shape**: Cone
    - **Start Lifetime**: 0.3–0.7
    - **Start Speed**: 5–10
    - **Start Size**: 0.05–0.1
    - **Start Color**: Yellow to Orange
    - **Emission**: Set to a burst (10–30 particles)
    - **Trails Module**: Enable for glowing trails.
3. **Renderer**:
    - Use a point or small round texture.
    - Set material to `Particles/Additive` for glow.

---

## Screenshots

<img width="600" alt="Screenshot 2025-09-23 021316" src="https://github.com/user-attachments/assets/96534cb5-5dbd-44eb-aa5b-d893305cafb0" />

---

## License

This project is for educational purposes.
