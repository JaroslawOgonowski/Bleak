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
            gatheringButton.onClick.AddListener(() => Mining.instance.onMiningButtonClick(target));
        }
        else
        {
            Debug.LogError("Bad type");
        }
        gatheringPanel.SetActive(true);
    }
}
