using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light flickerLight;
    public float minIntensity = 50f;
    public float maxIntensity = 100f;
    public float flickerDuration = 0.1f;

    private float timer;

    void Start()
    {
        flickerLight = GetComponent<Light>();
    }

    void Update()
    {
        // Increment the timer by the time since last frame
        timer += Time.deltaTime;

        // Check if the timer has reached the flicker duration
        if (timer >= flickerDuration)
        {
            // Reset the timer
            timer = 0f;

            // Assign a random intensity within the range
            flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
