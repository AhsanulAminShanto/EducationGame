using UnityEngine;

public class TemperatureSimulator : MonoBehaviour
{
    public Transform mercuryTransform; // Drag Mercury1 here
    private float startScaleY = 0.2f;
    private float maxScaleY = 1f;
    private float timer = 0f;
    private bool isActive = false;
    private int requiredSeeds = 2; // Number of seeds needed to maximize temperature

    void Start()
    {
        if (mercuryTransform != null)
        {
            mercuryTransform.localScale = new Vector3(1, startScaleY, 1);
        }
        enabled = false;
    }

    void Update()
    {
        if (isActive && mercuryTransform != null)
        {
            int seedCount = CountSeedsInJar();

            // Log the number of seeds for each update
            if (seedCount > 0)
            {
                Debug.Log("Number of GerminatingSeeds in Jar: " + seedCount + " out of " + requiredSeeds);
            }
            else
            {
                Debug.Log("No GerminatingSeeds in Jar.");
            }

            // Control temperature based on seed count
            if (seedCount == requiredSeeds)
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
                    Debug.Log("Temperature is rising... Progress: " + (progress * 100).ToString("F1") + "%");
                }
                else if (progress >= 1f)
                {
                    Debug.Log("Temperature has reached maximum!");
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
                if (seedCount == 0)
                {
                    Debug.Log("Jar is empty. Temperature reset.");
                }
                else
                {
                    Debug.Log("Not enough GerminatingSeeds (" + seedCount + " out of " + requiredSeeds + "). Temperature paused.");
                }
            }
        }
    }

    void OnEnable()
    {
        isActive = true;
        Debug.Log("Temperature simulation started. Requires " + requiredSeeds + " GerminatingSeeds.");
    }

    int CountSeedsInJar()
    {
        Transform jar = GameObject.Find("Jar1").transform; // Assumes Jar1 exists
        if (jar != null)
        {
            int count = 0;
            foreach (Transform child in jar)
            {
                if (child.CompareTag("Seeds"))
                {
                    count++;
                }
            }
            return count; // Count only objects tagged as "Seeds"
        }
        Debug.LogWarning("Jar1 not found!");
        return 0;
    }
}