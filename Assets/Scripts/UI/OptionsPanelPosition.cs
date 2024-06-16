using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanelPosition : MonoBehaviour
{
    [SerializeField] private GameObject mobInfoPanel;
    [SerializeField] private GameObject generaButtonsPanel;
    [SerializeField] private Transform startingPosition;

    private Vector3 generaButtonsOriginalPosition;

    // Start jest wywo³ywany przed pierwsz¹ klatk¹ update
    void Start()
    {
        generaButtonsOriginalPosition = generaButtonsPanel.transform.localPosition;
        UpdateGeneraButtonsPanelPosition();
    }

    // Metoda wywo³ywana, gdy obiekt jest w³¹czony
    void OnEnable()
    {
        UpdateGeneraButtonsPanelPosition();
    }

    // Metoda wywo³ywana, gdy obiekt jest wy³¹czony
    void OnDisable()
    {
        UpdateGeneraButtonsPanelPosition();
    }

    // Aktualizuje pozycjê GeneraButtonsPanel
    private void UpdateGeneraButtonsPanelPosition()
    {
        if (!mobInfoPanel.activeSelf)
        {
            Vector3 position = generaButtonsPanel.transform.localPosition;
            position.y = -45;
            generaButtonsPanel.transform.localPosition = position;
        }
        else
        {
            generaButtonsPanel.transform.localPosition = generaButtonsOriginalPosition;
        }
    }
}
