# Unity Character Movement & VFX System

A Unity 3D project featuring first-person character movement with environmental VFX effects.

## Features

- **First-Person Character Controller** - WASD movement with mouse look
- **Fire Effects** - Dynamic fire with particle systems and lighting
- **Smoke Effects** - Placeable smoke with particle systems
- **Foliage Painter** - Paint vegetation in the scene editor

## How It Works

### Character Movement System
The `CharacterMovement.cs` script uses Unity's CharacterController component to handle player movement:
- **Input Detection** - Captures WASD keys and mouse movement
- **Physics Integration** - Applies gravity and collision detection
- **Camera Control** - Rotates camera based on mouse input with clamping

### VFX Systems
**Fire Effects:**
- ParticleSystem generates fire particles with upward velocity
- Point Light component flickers to simulate realistic fire lighting
- Auto-scaling based on intensity settings

**Smoke Effects:**
- ParticleSystem creates smoke particles with wind simulation
- Automatic lifetime management with fade-out
- Configurable density and movement patterns

### Editor Tools
**Foliage Painter:**
- Raycast from camera to detect ground surfaces
- Instantiates prefabs at hit points with random rotation/scale
- Slope detection prevents placement on steep surfaces

**Smoke Painter:**
- Similar raycast system for smoke placement
- Automatic ground alignment and normal-based positioning

## How to Use

### Running the Project
1. Open the project in Unity 2020.3 or newer
2. Load `Assets/Scenes/MainScene.unity`
3. Press Play to test

### Controls
- **WASD** - Move character
- **Mouse** - Look around
- **Space** - Jump
- **Shift** - Run
- **Escape** - Toggle cursor lock

### Editor Tools
- **Shift + Click** - Paint foliage (select FoliageManager first)
- **Ctrl + Click** - Place smoke effects (select SmokeManager first)

## Scripts

| Script | Purpose |
|--------|---------|
| `CharacterMovement.cs` | First-person character controller |
| `FoliagePainter.cs` | Editor tool for painting foliage |
| `FireEffect.cs` | Fire particle system with lighting |
| `SmokePainter.cs` | Editor tool for placing smoke |
| `SmokeEffect.cs` | Smoke particle system |
| `SimpleGroundSetup.cs` | Basic ground generation |

## Project Structure

```
Assets/
├── Scripts/     # All C# scripts
├── Prefabs/     # Game object prefabs
├── Scenes/      # Unity scenes
└── Materials/   # Materials for effects
```

## Screenshots

<img width="2559" height="1439" alt="Screenshot 2025-09-23 021350" src="https://github.com/user-attachments/assets/fc21c88c-d6e9-4877-936f-b0bc70798aed" />

---

## License

This project is for educational purposes.


