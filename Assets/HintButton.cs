using UnityEngine;

public class HintButton : MonoBehaviour
{
    private HintManager hintManager;
    private bool isHintsVisible = false; // Single toggle state for all hints

    void Start()
    {
        hintManager = FindObjectOfType<HintManager>();
    }

    void OnMouseDown()
    {
        if (hintManager != null)
        {
            isHintsVisible = !isHintsVisible; // Toggle visibility
            hintManager.ToggleAllHints(isHintsVisible);
            Debug.Log("Hints toggled: " + isHintsVisible);
        }
    }
}