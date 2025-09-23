using UnityEngine;

public class FireEffect : MonoBehaviour
{
    [Header("Fire Particles")]
    public ParticleSystem fireParticles;
    public ParticleSystem smokeParticles;
    public ParticleSystem sparkParticles;

    [Header("Fire Settings")]
    public float fireIntensity = 1f;
    public Color fireColor = Color.red;
    public AudioSource fireAudioSource;

    [Header("Light Settings")]
    public Light fireLight;
    public float lightFlickerSpeed = 10f;
    public float lightIntensityMin = 0.5f;
    public float lightIntensityMax = 2f;

    private float baseLightIntensity;

    void Start()
    {
        SetupFireEffect();
        if (fireLight != null)
            baseLightIntensity = fireLight.intensity;
    }

    void Update()
    {
        UpdateFireLight();
    }

    void SetupFireEffect()
    {
        if (fireParticles == null)
            fireParticles = CreateFireParticles();

        if (smokeParticles == null)
            smokeParticles = CreateSmokeParticles();

        if (sparkParticles == null)
            sparkParticles = CreateSparkParticles();
    }

    ParticleSystem CreateFireParticles()
    {
        GameObject fireGO = new GameObject("Fire Particles");
        fireGO.transform.SetParent(transform);
        fireGO.transform.localPosition = Vector3.zero;

        ParticleSystem ps = fireGO.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = new ParticleSystem.MinMaxGradient(fireColor, Color.yellow);
        main.startLifetime = 1f;
        main.startSpeed = 2f;
        main.startSize = 0.5f;
        main.maxParticles = 100;

        var emission = ps.emission;
        emission.rateOverTime = 50;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.5f;

        var velocityOverLifetime = ps.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.space = ParticleSystemSimulationSpace.Local;
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(2f, 5f);

        var sizeOverLifetime = ps.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        AnimationCurve sizeCurve = new AnimationCurve();
        sizeCurve.AddKey(0f, 0.3f);
        sizeCurve.AddKey(0.5f, 1f);
        sizeCurve.AddKey(1f, 0f);
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, sizeCurve);

        return ps;
    }

    ParticleSystem CreateSmokeParticles()
    {
        GameObject smokeGO = new GameObject("Smoke Particles");
        smokeGO.transform.SetParent(transform);
        smokeGO.transform.localPosition = Vector3.up * 2f;

        ParticleSystem ps = smokeGO.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);
        main.startLifetime = 3f;
        main.startSpeed = 1f;
        main.startSize = 1f;
        main.maxParticles = 50;

        var emission = ps.emission;
        emission.rateOverTime = 10;

        var velocityOverLifetime = ps.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(1f, 3f);

        var sizeOverLifetime = ps.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        AnimationCurve sizeCurve = new AnimationCurve();
        sizeCurve.AddKey(0f, 0.5f);
        sizeCurve.AddKey(1f, 2f);
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, sizeCurve);

        return ps;
    }

    ParticleSystem CreateSparkParticles()
    {
        GameObject sparkGO = new GameObject("Spark Particles");
        sparkGO.transform.SetParent(transform);
        sparkGO.transform.localPosition = Vector3.zero;

        ParticleSystem ps = sparkGO.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = Color.yellow;
        main.startLifetime = 0.5f;
        main.startSpeed = 5f;
        main.startSize = 0.1f;
        main.maxParticles = 20;

        var emission = ps.emission;
        emission.rateOverTime = 5;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.2f;

        return ps;
    }

    void UpdateFireLight()
    {
        if (fireLight != null)
        {
            float flicker = Mathf.PerlinNoise(Time.time * lightFlickerSpeed, 0f);
            fireLight.intensity = baseLightIntensity + Mathf.Lerp(lightIntensityMin, lightIntensityMax, flicker) * fireIntensity;
        }
    }

    public void SetFireIntensity(float intensity)
    {
        fireIntensity = intensity;

        if (fireParticles != null)
        {
            var emission = fireParticles.emission;
            emission.rateOverTime = 50 * intensity;
        }

        if (smokeParticles != null)
        {
            var emission = smokeParticles.emission;
            emission.rateOverTime = 10 * intensity;
        }
    }
}