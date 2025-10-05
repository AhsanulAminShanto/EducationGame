using UnityEngine;
using UnityEngine.SceneManagement; // Correct namespace

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float simulationDuration = 30f; // Real-time seconds for sim
    public bool simulationRunning = false;
    public float germTempRise = 5f; // Degrees C rise

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartSimulation()
    {
        simulationRunning = true;
        // Trigger simulation logic (e.g., in TemperatureSimulator)
        // Note: This method is not currently used in your minimal setup but kept for future expansion
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Simplified using SceneManager directly
    }
}