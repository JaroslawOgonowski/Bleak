using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GatheringPanelManager : MonoBehaviour
{
    public static GatheringPanelManager instance;
    [SerializeField] public GameObject gatheringPanel;
    [SerializeField] private Button gatheringPanelCloseButton;
    [SerializeField] private TextMeshProUGUI gatheringPanelTitle;
    [SerializeField] private TextMeshProUGUI gatheringPanelContent;
    [SerializeField] public Button gatheringButton;
    [SerializeField] private Image gatheringImage;
    [SerializeField] private Sprite minningSprite;
    [SerializeField] private Sprite lumberSprite;
    [SerializeField] private Sprite harvestingSprite;
    public GameObject currentTarget;
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
        currentTarget = null;
    }
    public void OpenGatheringPanel(GameObject target)
    {
        MobInfoPanelManager.Instance.mobPanel.SetActive(false);
        gatheringPanel.SetActive(false);
        gatheringButton.onClick.RemoveAllListeners();
        if (target != currentTarget && target != Gather.instance.currentTarget)
        {
            currentTarget = target;
            GatheringObject targetInfo = target.GetComponent<GatheringObject>();
            gatheringPanelTitle.text = targetInfo.name;
            gatheringPanelContent.text = $"Requirements: {targetInfo.firstSkillReqName}: {targetInfo.firstSkillReq}, Tool: {targetInfo.toolReq}";

            if (targetInfo.type == 1)
            {
                gatheringImage.sprite = minningSprite;
                gatheringButton.onClick.AddListener(() => Gather.instance.onMiningButtonClick(target));
            }
            else if (targetInfo.type == 2)
            {
                gatheringImage.sprite = lumberSprite;
                gatheringButton.onClick.AddListener(() => Gather.instance.onLumberButtonClick(target));
            }
            else if (targetInfo.type == 3)
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

}
