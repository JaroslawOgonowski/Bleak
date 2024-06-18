using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanelPosition : MonoBehaviour
{
    [SerializeField] private GameObject mobInfoPanel;
    [SerializeField] private GameObject generaButtonsPanel;
    [SerializeField] private Canvas canvas;

    private Vector3 generaButtonsPositionWhenMobInfoInactive;
    private Vector3 generaButtonsPositionWhenMobInfoActive;
    private bool isFirstUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the positions relative to the canvas
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float canvasHeight = canvasRect.rect.height;

        // Set positions relative to the top of the screen
        generaButtonsPositionWhenMobInfoInactive = new Vector3(0, -canvasHeight + (canvasHeight - 45f), 0);
        generaButtonsPositionWhenMobInfoActive = new Vector3(0, -canvasHeight + (canvasHeight - 182.5f), 0);

        // Initial position update (if needed)
        UpdateGeneraButtonsPanelPosition();
    }

    // Method called when the object is enabled
    void OnEnable()
    {
        if (!isFirstUpdate)
        {
            UpdateGeneraButtonsPanelPosition();
        }
    }

    // Method called when the object is disabled
    void OnDisable()
    {
        if (!isFirstUpdate)
        {
            UpdateGeneraButtonsPanelPosition();
        }
        isFirstUpdate = false; // Ensuring it is reset after the first update
    }

    // Updates the position of the GeneraButtonsPanel
    private void UpdateGeneraButtonsPanelPosition()
    {
        RectTransform generaButtonsRect = generaButtonsPanel.GetComponent<RectTransform>();

        if (!mobInfoPanel.activeSelf)
        {
            generaButtonsRect.anchoredPosition = generaButtonsPositionWhenMobInfoInactive;
        }
        else
        {
            generaButtonsRect.anchoredPosition = generaButtonsPositionWhenMobInfoActive;
        }
    }
}
