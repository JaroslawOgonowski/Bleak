using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollGeneralButtonsMenu : MonoBehaviour
{
    [SerializeField] private Button rollButton;
    [SerializeField] private Button unrollButton;
    [SerializeField] private GameObject buttonsPanel;

    private void Start()
    {
        unrollOnClick();
        rollButton.onClick.AddListener(() => rollClick());
        unrollButton.onClick.AddListener(() => unrollOnClick());

    }

    private void rollClick()
    {
        unrollButton.gameObject.SetActive(true);
        rollButton.gameObject.SetActive(false);
        buttonsPanel.SetActive(false);
    }
    private void unrollOnClick()
    {
        unrollButton.gameObject.SetActive(false);
        rollButton.gameObject.SetActive(true);
        buttonsPanel.SetActive(true);
    }
}
