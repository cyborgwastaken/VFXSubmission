using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BoidsController : MonoBehaviour
{

    [Header("Boid Settings")]
    public float maxSpeed = 5.0f;
    public float perceptionRadius = 2.5f;
    public float separationRadius = 1.0f;

    [Header("Behavior Weights")]
    public float cohesionWeight = 1.0f;
    public float alignmentWeight = 1.0f;
    public float separationWeight = 1.5f;

    [Header("Target Settings")]
    [Tooltip("The object for the swarm to follow.")]
    public Transform target; 
    [Tooltip("How strongly the swarm is pulled towards the target.")]
    public float targetWeight = 2.0f; 

   
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private Vector3[] particleVelocities; 

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
        particleVelocities = new Vector3[ps.main.maxParticles];
    }

    void LateUpdate()
    {
        int numParticlesAlive = ps.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            particleVelocities[i] = particles[i].velocity;
        }

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 cohesionSum = Vector3.zero;
            Vector3 alignmentSum = Vector3.zero;
            Vector3 separationSum = Vector3.zero;
            int cohesionNeighbors = 0;
            int alignmentNeighbors = 0;
            int separationNeighbors = 0;

            for (int j = 0; j < numParticlesAlive; j++)
            {
                if (i == j) continue;

                float distance = Vector3.Distance(particles[i].position, particles[j].position);

                if (distance > 0 && distance < perceptionRadius)
                {
                    cohesionSum += particles[j].position;
                    cohesionNeighbors++;
                    alignmentSum += particleVelocities[j];
                    alignmentNeighbors++;
                }

                if (distance > 0 && distance < separationRadius)
                {
                    Vector3 awayVector = particles[i].position - particles[j].position;
                    separationSum += awayVector / (distance * distance);
                    separationNeighbors++;
                }
            }

            Vector3 acceleration = Vector3.zero;

            if (cohesionNeighbors > 0)
            {
                Vector3 centerOfMass = cohesionSum / cohesionNeighbors;
                Vector3 desiredDirection = (centerOfMass - particles[i].position).normalized * maxSpeed;
                acceleration += (desiredDirection - particleVelocities[i]) * cohesionWeight;
            }

            if (alignmentNeighbors > 0)
            {
                Vector3 averageVelocity = alignmentSum / alignmentNeighbors;
                Vector3 desiredDirection = averageVelocity.normalized * maxSpeed;
                acceleration += (desiredDirection - particleVelocities[i]) * alignmentWeight;
            }

            if (separationNeighbors > 0)
            {
                Vector3 averageSeparation = separationSum / separationNeighbors;
                Vector3 desiredDirection = averageSeparation.normalized * maxSpeed;
                acceleration += (desiredDirection - particleVelocities[i]) * separationWeight;
            }

           
            if (target != null)
            {
                
                Vector3 directionToTarget = target.position - particles[i].position;
               
                Vector3 desiredDirection = directionToTarget.normalized * maxSpeed;
               
                acceleration += (desiredDirection - particleVelocities[i]) * targetWeight;
            }

            Vector3 newVelocity = particleVelocities[i] + acceleration * Time.deltaTime;
            newVelocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);

            particles[i].velocity = newVelocity;
        }

        ps.SetParticles(particles, numParticlesAlive);
    }
}