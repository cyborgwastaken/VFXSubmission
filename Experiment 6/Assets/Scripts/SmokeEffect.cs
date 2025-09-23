using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SmokeEffect : MonoBehaviour
{
    [Header("Smoke Settings")]
    public float smokeDuration = 10f;
    public float smokeIntensity = 1f;
    public bool autoDestroy = true;

    private ParticleSystem smokeParticles;
    private float startTime;

    void Start()
    {
        smokeParticles = GetComponent<ParticleSystem>();
        startTime = Time.time;

        // Configure smoke based on intensity
        var emission = smokeParticles.emission;
        emission.rateOverTime = 20 * smokeIntensity;

        var main = smokeParticles.main;
        main.startSize = new ParticleSystem.MinMaxCurve(0.5f * smokeIntensity, 1.5f * smokeIntensity);
    }

    void Update()
    {
        if (autoDestroy && Time.time - startTime > smokeDuration)
        {
            var emission = smokeParticles.emission;
            emission.enabled = false;

            // Destroy after all particles have died
            Destroy(gameObject, smokeParticles.main.startLifetime.constant);
        }
    }

    public void SetSmokeIntensity(float intensity)
    {
        smokeIntensity = intensity;

        if (smokeParticles != null)
        {
            var emission = smokeParticles.emission;
            emission.rateOverTime = 20 * intensity;

            var main = smokeParticles.main;
            main.startSize = new ParticleSystem.MinMaxCurve(0.5f * intensity, 1.5f * intensity);
        }
    }
}