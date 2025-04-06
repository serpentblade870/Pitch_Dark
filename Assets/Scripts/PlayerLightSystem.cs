using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Import TextMeshPro namespace

public class PlayerLightSystem : MonoBehaviour
{
    [Header("Light Settings")]
    public Light playerLight;
    public float maxBatteryLife = 100f;
    public float batteryDrainRate = 5f; // per second
    public float currentBatteryLife;
    public bool isLightOn = true;
    
    [Header("Light Sources")]
    public int collectableLightSources = 0;
    public float candleBatteryValue = 25f; // how much battery each candle adds
    
    [Header("Flickering Effect")]
    public bool enableFlickering = true;
    public float flickerIntensity = 0.2f;
    public float flickerSpeed = 0.1f;
    private float baseIntensity;
    private float randomOffset;
    
    [Header("Game Time Limit")]
    public float maxGameTime = 300f; // 5 minutes
    public float currentGameTime;
    public bool gameTimerActive = true;
    
    [Header("UI References")]
    public Slider batteryMeter; // Change to Slider
    public TMP_Text lightSourceCountText; // Changed to TMP_Text
    public TMP_Text timeRemainingText; // Changed to TMP_Text
    
    void Start()
    {
        // Initialize battery to full
        currentBatteryLife = maxBatteryLife;
        baseIntensity = playerLight.intensity;
        currentGameTime = maxGameTime;
        
        UpdateUI();
    }
    
    void Update()
    {
        // Handle game timer
        if (gameTimerActive)
        {
            currentGameTime -= Time.deltaTime;
            if (currentGameTime <= 0)
            {
                GameOver();
            }
        }
        
        // Drain battery if the light is on
        if (isLightOn && playerLight.enabled)
        {
            currentBatteryLife -= batteryDrainRate * Time.deltaTime;
            if (currentBatteryLife <= 0)
            {
                currentBatteryLife = 0;
                TurnOffLight();
                GameOver();
            }
            
            if (enableFlickering)
            {
                ApplyFlickerEffect();
            }
            else
            {
                float intensityRatio = currentBatteryLife / maxBatteryLife;
                playerLight.intensity = Mathf.Lerp(0.5f, baseIntensity, intensityRatio);
            }
        }
        
        // Toggle light with keyboard (for testing)
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLight();
        }
        
        // Use a light source with keyboard (for testing)
        if (Input.GetKeyDown(KeyCode.R) && collectableLightSources > 0)
        {
            UseLightSource();
        }
        
        UpdateUI();
    }
    
    void ApplyFlickerEffect()
    {
        float batteryRatio = currentBatteryLife / maxBatteryLife;
        float targetBaseIntensity = Mathf.Lerp(0.5f, baseIntensity, batteryRatio);
        float flickerAmount = flickerIntensity * (1.0f - batteryRatio * 0.5f);
        randomOffset = Mathf.PerlinNoise(Time.time * flickerSpeed, 0) * flickerAmount;
        playerLight.intensity = targetBaseIntensity + randomOffset - (flickerAmount * 0.5f);
    }
    
    public void ToggleLight()
    {
        if (!isLightOn && currentBatteryLife > 0)
        {
            isLightOn = true;
            playerLight.enabled = true;
        }
        else if (isLightOn)
        {
            TurnOffLight();
            GameOver();
        }
    }
    
    void TurnOffLight()
    {
        isLightOn = false;
        playerLight.enabled = false;
    }
    
    public void CollectCandle()
    {
        currentBatteryLife = Mathf.Min(currentBatteryLife + candleBatteryValue, maxBatteryLife);
        collectableLightSources++;
        if (!isLightOn && currentBatteryLife > 0)
        {
            isLightOn = true;
            playerLight.enabled = true;
        }
        
        UpdateUI();
    }
    
    public void UseLightSource()
    {
        if (collectableLightSources > 0)
        {
            collectableLightSources--;
            currentBatteryLife = Mathf.Min(currentBatteryLife + candleBatteryValue, maxBatteryLife);
            if (!isLightOn && currentBatteryLife > 0)
            {
                isLightOn = true;
                playerLight.enabled = true;
            }
            
            UpdateUI();
        }
    }
    
    void UpdateUI()
    {
        if (batteryMeter != null)
        {
            batteryMeter.value = currentBatteryLife; // Update slider value directly
        }
        
        if (lightSourceCountText != null)
        {
            lightSourceCountText.text = "Candles: " + collectableLightSources;
        }
        
        if (timeRemainingText != null)
        {
            int minutes = Mathf.FloorToInt(currentGameTime / 60);
            int seconds = Mathf.FloorToInt(currentGameTime % 60);
            timeRemainingText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }
    
    void GameOver()
    {
        Debug.Log("Game Over - Light went out!");
        SceneManager.LoadScene("GameOver");
    }
}
