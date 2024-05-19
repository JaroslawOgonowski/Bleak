using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GatheringPanelManager : MonoBehaviour
{
    public static GatheringPanelManager instance;
    [SerializeField] private GameObject gatheringPanel;
    [SerializeField] private Button gatheringPanelCloseButton;
    [SerializeField] private TextMeshProUGUI gatheringPanelTitle;
    [SerializeField] private TextMeshProUGUI gatheringPanelContent;
    [SerializeField] private Button gatheringButton;
    [SerializeField] private Image gatheringImage;
    [SerializeField] private Sprite minningSprite;
    [SerializeField] private Sprite lumberSprite;
    [SerializeField] private Sprite harvestingSprite;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gatheringPanel.SetActive(false);
        gatheringPanelCloseButton.onClick.AddListener(() => closeGatheringPanel());
    }
    
    private void closeGatheringPanel()
    {
        gatheringPanel.SetActive(false);
        gatheringButton.onClick.RemoveAllListeners();
    }
    public void OpenGatheringPanel(GameObject target)
    {
        GatheringObject targetInfo = target.GetComponent<GatheringObject>();
        gatheringPanelTitle.text = targetInfo.name;
        gatheringPanelContent.text = $"Requirements: {targetInfo.firstSkillReqName}: {targetInfo.firstSkillReq}, Tool: {targetInfo.toolReq}";

        if(targetInfo.type == 1)
        {
            gatheringImage.sprite = minningSprite;
            gatheringButton.onClick.AddListener(() => Mining.instance.onMiningButtonClick(target));
        } 
        else if( targetInfo.type == 2)
        {
            gatheringImage.sprite = lumberSprite;
        }
        else if( targetInfo.type == 3)
        {
            gatheringImage.sprite = harvestingSprite;
        }
        else
        {
            Debug.LogError("Bad type");
        }
        gatheringPanel.SetActive(true);
    }
}