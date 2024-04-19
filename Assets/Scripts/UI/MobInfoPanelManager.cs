using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInfoPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mobPanel;

    private void Start()
    {
        mobPanel.SetActive(false);
    }

    public void OpenMobPanel()
    {
        mobPanel.SetActive(true);
    }
}
