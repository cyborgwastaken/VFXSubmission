using UnityEngine;

// This line ensures the script can only be attached to a GameObject
// that also has a ParticleSystem component.
[RequireComponent(typeof(ParticleSystem))]
public class SwarmController : MonoBehaviour
{
    // PUBLIC VARIABLES (appear in the Inspector)
    [Tooltip("The target the swarm will follow.")]
    public Transform target; // The target object

    [Tooltip("How fast the particles move.")]
    public float speed = 5.0f;

    [Tooltip("How much the particles spread out from each other.")]
    public float spread = 1.0f;

    [Tooltip("How chaotic and random the individual movement is.")]
    public float noiseFrequency = 0.5f;

    // PRIVATE VARIABLES
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles; // An array to hold the particles

    // This method is called once when the script starts
    void Start()
    {
        // Get the ParticleSystem component attached to this GameObject
        ps = GetComponent<ParticleSystem>();

        // Initialize the particles array. Its size must be the max particles.
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    // LateUpdate is called every frame, after all other Update methods.
    // It's the best place to modify particles to avoid visual glitches.
    void LateUpdate()
    {
        // First, check if the target has been assigned.
        if (target == null)
        {
            Debug.LogWarning("SwarmController needs a target!");
            return; // Stop the script if there's no target
        }

        // Get the current particles from the system and store them in our array.
        // The return value is the number of particles that are currently alive.
        int numParticlesAlive = ps.GetParticles(particles);

        // Now, loop through all the *living* particles
        for (int i = 0; i < numParticlesAlive; i++)
        {
            // --- CORE LOGIC ---

            // 1. Find the direction towards the target
            Vector3 directionToTarget = (target.position - particles[i].position).normalized;

            // 2. Generate some random, wavy motion using Perlin Noise.
            // This makes the movement feel more organic and less robotic.
            float noiseValueX = (Mathf.PerlinNoise(Time.time * noiseFrequency, particles[i].position.x) - 0.5f);
            float noiseValueY = (Mathf.PerlinNoise(Time.time * noiseFrequency, particles[i].position.y) - 0.5f);
            float noiseValueZ = (Mathf.PerlinNoise(Time.time * noiseFrequency, particles[i].position.z) - 0.5f);
            Vector3 noiseVector = new Vector3(noiseValueX, noiseValueY, noiseValueZ) * spread;

            // 3. Combine the target direction with the random noise
            Vector3 combinedDirection = (directionToTarget + noiseVector).normalized;

            // 4. Set the particle's velocity
            Vector3 targetVelocity = combinedDirection * speed;

            // 5. Smoothly change from the current velocity to the target velocity.
            // This prevents jerky movements.
            particles[i].velocity = Vector3.Lerp(particles[i].velocity, targetVelocity, Time.deltaTime);
        }

        // Finally, apply our modified particle data back to the Particle System.
        ps.SetParticles(particles, numParticlesAlive);
    }
}