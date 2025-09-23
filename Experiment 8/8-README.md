# Unity Particle Swarms & Bubbles with C#

This Unity project is a practical demonstration of creating complex, dynamic particle behaviors by controlling each particle individually through C# scripts. It moves beyond Unity's standard particle modules to implement realistic flocking swarms and physics-based bubbles using mathematical expressions and algorithms.


## Features
- Boids Flocking Swarm: A particle system that simulates swarm intelligence using Craig Reynolds' classic Boids algorithm.
    - Implements the three core steering behaviors: Cohesion, Separation, and Alignment.
    - Includes a target-following behavior, allowing the swarm to chase a designated Transform in the scene.


- Bubbles: A particle system that simulates bubbles rising through a fluid.
   - Particle movement is driven by mathematical forces calculated in real-time:
 
 ## Prerequisites
- Unity Hub
- Unity 2021.3 (LTS) or newer.
- The project must be configured to use the Universal Render Pipeline (URP) for the particle materials to work correctly.

## Getting Started
1. Open Unity
2. Create a Hierarchy like this
   
   <img width="368" height="400" alt="Screenshot 2025-09-23 002944" src="https://github.com/user-attachments/assets/0f3a1579-e6ee-4c47-91c8-56fa2dd9f818" />

3. Create a Particle System for both bubbles and Swamp
 <img width="1919" height="1022" alt="Screenshot 2025-09-23 003031" src="https://github.com/user-attachments/assets/7229cbea-61fa-4963-858d-e17b85975bee" />


4. Setup the Scripts by creating new scripts
   <img width="1668" height="847" alt="Screenshot 2025-09-23 002705" src="https://github.com/user-attachments/assets/d2eabb43-aff4-40ac-9256-97e13c7206bd" />
<img width="1689" height="854" alt="Screenshot 2025-09-23 002630" src="https://github.com/user-attachments/assets/8b521aac-a876-47d4-9c9c-e4bc9d716cb1" />

5. Play the scene
   <img width="1919" height="1079" alt="Screenshot 2025-09-23 002044" src="https://github.com/user-attachments/assets/95bb6566-d936-4fa3-95c6-75706624b1fe" />



## Scripts Overview
### BoidsController.cs
- This script is the core of the swarm simulation. It is attached to the swarm's ParticleSystem GameObject.
- It gets all living particles on each frame.
- For each particle, it loops through all other particles to find its neighbors within a perception radius.
- It calculates the Cohesion, Separation, and Alignment vectors based on these neighbors.
- If a target is assigned, it calculates an additional steering vector towards that target.
- It applies these combined forces to the particle's velocity and then pushes the changes back to the ParticleSystem.

### SimplePlayerMovement.cs
- A utility script used to move the swarm's target object around the scene using the WASD or arrow keys. This helps demonstrate the swarm's following behavior interactively.

## Script Parameters
<img width="416" height="369" alt="Screenshot 2025-09-23 013249" src="https://github.com/user-attachments/assets/d912eddc-3538-4357-b637-48a25a662735" />

- These settings control the behaviour of swamp towards itself and the target object.


