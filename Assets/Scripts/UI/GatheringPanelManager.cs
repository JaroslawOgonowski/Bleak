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
    void Start()
    {
        instance = this;
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
            gatheringButton.onClick.AddListener(() => target.GetComponent<Mining>().onMiningButtonClick());
        }
        gatheringPanel.SetActive(true);
    }
}
