using UnityEngine;
using TMPro; // For TextMeshPro

public class HintManager : MonoBehaviour
{
    public TextMeshPro jar1HintText;
    public TextMeshPro jar2HintText;
    public TextMeshPro thermoHintText;
    public TextMeshPro seedsHintText;
    public TextMeshPro seedsbHintText;
    private bool isHintVisible = false;

    void Start()
    {
        // Ensure all hint texts start disabled
        if (jar1HintText != null) jar1HintText.gameObject.SetActive(false);
        if (jar2HintText != null) jar2HintText.gameObject.SetActive(false);
        if (thermoHintText != null) thermoHintText.gameObject.SetActive(false);
        if (seedsHintText != null) seedsHintText.gameObject.SetActive(false);
        if (seedsbHintText != null) seedsbHintText.gameObject.SetActive(false);
    }

    public void ToggleAllHints(bool show)
    {
        isHintVisible = show;
        UpdateHintVisibility();
        
        if (isHintVisible)
        {
            if (jar1HintText != null) jar1HintText.text = "Jar1";
            if (jar2HintText != null) jar2HintText.text = "Jar2";
            if (thermoHintText != null) thermoHintText.text = "Thermometer";
            if (seedsHintText != null) seedsHintText.text = "GerminatingSeeds";
            if (seedsbHintText != null) seedsbHintText.text = "BoiledSeeds";
        }
    }

    void UpdateHintVisibility()
    {
        if (jar1HintText != null) jar1HintText.gameObject.SetActive(isHintVisible);
        if (jar2HintText != null) jar2HintText.gameObject.SetActive(isHintVisible);
        if (thermoHintText != null) thermoHintText.gameObject.SetActive(isHintVisible);
        if (seedsHintText != null) seedsHintText.gameObject.SetActive(isHintVisible);
        if (seedsbHintText != null) seedsbHintText.gameObject.SetActive(isHintVisible);
    }

    void ClearHints()
    {
        if (jar1HintText != null) jar1HintText.text = "";
        if (jar2HintText != null) jar2HintText.text = "";
        if (thermoHintText != null) thermoHintText.text = "";
        if (seedsHintText != null) seedsHintText.text = "";
        if (seedsbHintText != null) seedsbHintText.text = "";
        UpdateHintVisibility();
    }
}