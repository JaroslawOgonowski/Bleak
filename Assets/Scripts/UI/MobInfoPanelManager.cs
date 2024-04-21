using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobInfoPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mobPanel;
    [SerializeField] private Button mobPanelHide;
    [SerializeField] private GameObject camera;
    private GameObject lastCameraTarget;
    private void OnEnable()
    {
        mobPanel.SetActive(false);
        mobPanelHide.onClick.AddListener(()=>mobPanelHideOnClick()); 
    }
    public void OpenMobPanel(GameObject clickedGO)
    {
        if(lastCameraTarget != null)
        {
            SetLayerRecursively(lastCameraTarget, "Mob");
        }
        lastCameraTarget = clickedGO;

        SetLayerRecursively(clickedGO, "CameraTarget");


        camera.GetComponent<CameraFollow>().target = clickedGO.GetComponent<Transform>();
        mobPanel.SetActive(true);
    }

    private void mobPanelHideOnClick()
    {
        mobPanel.SetActive(false);
    }
    private void SetLayerRecursively(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layerName);
        }
    }
}
