using UnityEngine;

public class WickMovement : MonoBehaviour
{
    [Header("Oscillation Settings")]
    [Tooltip("Maximum angle of oscillation")]
    public float maxOscillationAngle = 5f;
    
    [Tooltip("Speed of oscillation")]
    public float oscillationSpeed = 2f;
    
    [Tooltip("Randomness factor for more natural movement")]
    public float randomnessIntensity = 0.5f;

    [Header("Noise Settings")]
    [Tooltip("Scale of Perlin noise")]
    public float noiseScale = 1f;

    [Header("Flame Influence")]
    [Tooltip("Whether oscillation is influenced by flame intensity")]
    public bool influencedByFlame = true;
    public Light flameLight;
    
    private Vector3 startRotation;
    private float randomOffset;

    void Start()
    {
        // Store the initial rotation
        startRotation = transform.localRotation.eulerAngles;
        
        // Generate a random offset to make each wick move uniquely
        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Calculate oscillation based on time and noise
        float timeScale = Time.time * oscillationSpeed + randomOffset;
        
        // Use Perlin noise for smoother, more natural oscillation
        float noiseValue = Mathf.PerlinNoise(timeScale * noiseScale, 0);
        
        // Adjust oscillation intensity based on flame
        float oscillationIntensity = maxOscillationAngle;
        if (influencedByFlame && flameLight != null)
        {
            // Reduce oscillation when flame is dimmer
            oscillationIntensity *= flameLight.intensity / flameLight.range;
        }
        
        // Map noise to oscillation range
        float oscillationAngle = Mathf.Lerp(
            -oscillationIntensity, 
            oscillationIntensity, 
            noiseValue
        );
        
        // Add some randomness
        oscillationAngle += Random.Range(-randomnessIntensity, randomnessIntensity);
        
        // Create rotation quaternion
        Quaternion newRotation = Quaternion.Euler(
            startRotation.x + oscillationAngle, 
            startRotation.y, 
            startRotation.z
        );
        
        // Apply local rotation to maintain parent-child relationship
        transform.localRotation = newRotation;
    }

    // Optional method to adjust oscillation dynamically
    public void SetOscillationParameters(
        float angle = -1f, 
        float speed = -1f, 
        float randomness = -1f)
    {
        if (angle >= 0) maxOscillationAngle = angle;
        if (speed >= 0) oscillationSpeed = speed;
        if (randomness >= 0) randomnessIntensity = randomness;
    }
}