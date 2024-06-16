using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanelPosition : MonoBehaviour
{
    [SerializeField] private GameObject mobInfoPanel;
    [SerializeField] private GameObject generaButtonsPanel;
    [SerializeField] private Transform startingPosition;

    private Vector3 generaButtonsOriginalPosition;

    // Start jest wywo�ywany przed pierwsz� klatk� update
    void Start()
    {
        generaButtonsOriginalPosition = generaButtonsPanel.transform.localPosition;
        UpdateGeneraButtonsPanelPosition();
    }

    // Metoda wywo�ywana, gdy obiekt jest w��czony
    void OnEnable()
    {
        UpdateGeneraButtonsPanelPosition();
    }

    // Metoda wywo�ywana, gdy obiekt jest wy��czony
    void OnDisable()
    {
        UpdateGeneraButtonsPanelPosition();
    }

    // Aktualizuje pozycj� GeneraButtonsPanel
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
