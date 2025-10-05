using UnityEngine;

public class TemperatureSimulator : MonoBehaviour
{
    public Transform mercuryTransform; // Drag Mercury1 here
    private float startScaleY = 0.2f;
    private float maxScaleY = 1f;
    private float timer = 0f;
    private bool isActive = false;
    private int requiredSeeds = 1; // Number of seeds needed to maximize temperature
    private Transform parentJar; // Track the jar this thermometer is in

    void Start()
    {
        if (mercuryTransform != null)
        {
            mercuryTransform.localScale = new Vector3(1, startScaleY, 1);
        }
        enabled = false;
        // Check parent on start in case already placed
        parentJar = transform.parent;
        if (parentJar != null && parentJar.CompareTag("Jar"))
        {
            Debug.Log("Thermometer starting in: " + parentJar.name);
        }
    }

    void Update()
    {
        if (isActive && mercuryTransform != null)
        {
            // Update parent jar if changed
            parentJar = transform.parent;
            int seedCount = CountSeedsInJar();

            // Log the number of seeds for each update
            if (seedCount > 0)
            {
                Debug.Log("Number of GerminatingSeeds in " + (parentJar != null ? parentJar.name : "unknown jar") + ": " + seedCount + " out of " + requiredSeeds);
            }
            else if (parentJar != null)
            {
                Debug.Log("No GerminatingSeeds in " + parentJar.name + ".");
            }

            // Control temperature based on seed count
            if (parentJar != null && seedCount == requiredSeeds)
            {
                timer += Time.deltaTime;
                float progress = Mathf.Clamp01(timer / 1f); // 1-second simulation
                float currentScaleY = Mathf.Lerp(startScaleY, maxScaleY, progress);
                mercuryTransform.localScale = new Vector3(1, currentScaleY, 1);

                Color baseColor = Color.red;
                Color hotColor = Color.yellow;
                Renderer renderer = mercuryTransform.GetComponent<Renderer>();
                if (renderer != null && renderer.material != null)
                {
                    renderer.material.color = Color.Lerp(baseColor, hotColor, progress);
                }

                // Log temperature rising
                if (progress > 0f && progress < 1f)
                {
                    Debug.Log("Temperature in " + parentJar.name + " is rising... Progress: " + (progress * 100).ToString("F1") + "%");
                }
                else if (progress >= 1f)
                {
                    Debug.Log("Temperature in " + parentJar.name + " has reached maximum!");
                }
            }
            else
            {
                // Reset if not enough or no seeds
                timer = 0f;
                mercuryTransform.localScale = new Vector3(1, startScaleY, 1);
                Renderer renderer = mercuryTransform.GetComponent<Renderer>();
                if (renderer != null && renderer.material != null)
                {
                    renderer.material.color = Color.red; // Reset to base color
                }
                if (parentJar != null && seedCount == 0)
                {
                    Debug.Log(parentJar.name + " is empty. Temperature reset.");
                }
                else if (parentJar != null)
                {
                    Debug.Log("Not enough GerminatingSeeds in " + parentJar.name + " (" + seedCount + " out of " + requiredSeeds + "). Temperature paused.");
                }
            }
        }
    }

    void OnEnable()
    {
        isActive = true;
        parentJar = transform.parent; // Update parent on enable
        if (parentJar != null && parentJar.CompareTag("Jar"))
        {
            Debug.Log("Temperature simulation started in " + parentJar.name + ". Requires " + requiredSeeds + " GerminatingSeeds.");
        }
        else
        {
            Debug.Log("Temperature simulation started but not in a jar. Requires " + requiredSeeds + " GerminatingSeeds.");
        }
    }

    int CountSeedsInJar()
    {
        if (parentJar == null)
        {
            Debug.LogWarning("Thermometer not in a jar!");
            return 0;
        }

        Debug.Log("Checking seeds in: " + parentJar.name);
        int count = 0;
        foreach (Transform child in parentJar)
        {
            if (child.CompareTag("Seeds"))
            {
                count++;
                Debug.Log("Found seed: " + child.name + " in " + parentJar.name);
            }
        }
        return count; // Count only objects tagged as "Seeds"
    }
}