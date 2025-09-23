# Semi-Rigid and Rigid Debris Simulation in Blender

This Blender Python script simulates a combination of **rigid debris** and **semi-rigid debris connected with springs**. It demonstrates rigid body physics and spring constraints in a single cohesive simulation.

## Features

* Creates a **ground plane** for debris to fall onto.
* Spawns **rigid cubes** (red) that fall freely under gravity.
* Spawns **semi-rigid cubes** (blue) connected by spring constraints to form a chain.
* Configures physics properties like **mass, friction, restitution, spring stiffness, and damping**.
* Adds a **camera** for viewing the simulation.
* Automatically sets up the **Rigid Body World** and scene parameters.

## Requirements

* Blender 3.x (tested with 3.6+)
* Python 3.x (Blender’s built-in Python interpreter is used)

## Usage

1. Open Blender.
2. Open a **Scripting** window
   <img width="340" height="104" alt="Screenshot 2025-09-23 001024" src="https://github.com/user-attachments/assets/65e1f77a-3eb4-4d08-8616-f856aceb8c4f" />




3. click on new text paste the Python script provided as code.py .
   <img width="1919" height="998" alt="Screenshot 2025-09-23 001045" src="https://github.com/user-attachments/assets/cf99c7eb-a324-4f6c-bea2-e9920aa39d39" />




4. Press **Run Script** to set up the scene.
   <img width="1919" height="996" alt="Screenshot 2025-09-23 001056" src="https://github.com/user-attachments/assets/4ac774b3-3c82-4d43-9724-ee8d8ebb25b0" />




5. Press **Spacebar** in the 3D Viewport to play the physics simulation.
   <img width="1919" height="991" alt="Screenshot 2025-09-23 001103" src="https://github.com/user-attachments/assets/97ea8903-9720-4572-bad6-1e5472d5ddef" />



   * Go to **Scene Properties → Rigid Body World → Cache**
   * Click **Bake**

## Script Parameters

All parameters for rigid cubes, semi-rigid cubes, and springs are configured at the top of the script:

```python
NUM_RIGID = 5          # Number of rigid cubes
NUM_SEMI_RIGID = 5     # Number of semi-rigid cubes
DROP_HEIGHT = 8.0      # Initial height of cubes
SIM_FRAMES = 250       # Total frames of the simulation
GRAVITY = -9.81        # Gravity strength

# Spring settings inside connect_spring()
stiffness = 50         # Spring stiffness
damping = 5            # Spring damping
```

* **Rigid cubes**: mass = 3, color = red
* **Semi-rigid cubes**: mass = 1.5, color = blue, connected with spring constraints

These settings control the physics behavior of both types of debris and the spring stiffness/damping of the semi-rigid chain. You can also uncomment the last line to form a **closed ring** of semi-rigid cubes.

## Notes

* The blue "thread-like" lines in the 3D viewport indicate **spring constraints**.
* The script automatically ensures a **Rigid Body World** is present.


