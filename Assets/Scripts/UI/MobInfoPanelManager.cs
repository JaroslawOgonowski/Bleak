using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MobInfoPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mobPanel;
    [SerializeField] private Button mobPanelHideButton;
    [SerializeField] private TextMeshProUGUI mobPanelMobName;
    [SerializeField] private Slider mobPanelSliderHP;
    [SerializeField] private TextMeshProUGUI mobPanelHPText;
    [SerializeField] private TextMeshProUGUI mobPanelStats1;
    [SerializeField] private GameObject camera;
    private GameObject lastCameraTarget;

    private void OnEnable()
    {
        mobPanel.SetActive(false);
        mobPanelHideButton.onClick.AddListener(()=>mobPanelHideOnClick()); 
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
        MobStats stats = clickedGO.GetComponent<MobStats>();
        mobPanelMobName.text = stats.name;
        mobPanelHPText.text = $"{stats.hp}/{stats.maxhp}";
        mobPanelSliderHP.value = ( stats.maxhp / stats.hp );
        mobPanel.SetActive(true);
        mobPanelStats1.text = $"Rarity: {stats.rarity}\r\nGroup: {stats.group}\r\nArmor: {stats.armor}\r\nMagic Armor: {stats.magicArmor}\r\nDamage: {stats.minDmg}-{stats.maxDmg}\r\nSpeed: {stats.speed}\r\n";
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
