using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideInventory : MonoBehaviour
{
    [SerializeField] private Sprite showSprite;
    [SerializeField] private Sprite hideSprite;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button showHideButton;

    private void Start()
    {
        showHideButton.onClick.AddListener(() => ShowHideInventoryOnClick());
    }
    private void ShowHideInventoryOnClick()
    {
        if(panel != null)
        {
            if(panel.activeSelf)
            {
                showHideButton.GetComponentInChildren<Image>().sprite = showSprite;
                panel.SetActive(false);
            }
            else
            {
                showHideButton.GetComponentInChildren<Image>().sprite = hideSprite;
                panel.SetActive(true);
            }
        }
    }
}
