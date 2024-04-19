using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobInfoPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mobPanel;
    [SerializeField] private Button mobPanelHide;
    [SerializeField] private GameObject camera;
    private void OnEnable()
    {
        mobPanel.SetActive(false);
        mobPanelHide.onClick.AddListener(()=>mobPanelHideOnClick()); 
    }
    public void OpenMobPanel(GameObject clickedGO)
    {
        camera.GetComponent<CameraFollow>().target = clickedGO.GetComponent<Transform>();
        mobPanel.SetActive(true);
    }

    private void mobPanelHideOnClick()
    {
        mobPanel.SetActive(false);
    }
}
