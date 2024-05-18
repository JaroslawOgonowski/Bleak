using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GatheringPanelManager : MonoBehaviour
{
    public static GatheringPanelManager instance;
    [SerializeField] private GameObject gatheringPanel;
    [SerializeField] private Button gatheringPanelCloseButton;
    [SerializeField] private TextMeshProUGUI gatheringPanelTitle;
    [SerializeField] private TextMeshProUGUI gatheringPanelContent;

    void Start()
    {
        instance = this;
        gatheringPanel.SetActive(false);
        gatheringPanelCloseButton.onClick.AddListener(() => { gatheringPanel.SetActive(false); });
    }
    
    public void OpenGatheringPanel(GameObject target)
    {
        GatheringObject targetInfo = target.GetComponent<GatheringObject>();
        gatheringPanelTitle.text = targetInfo.name;
        gatheringPanelContent.text = $"Requirements: {targetInfo.firstSkillReqName}{targetInfo.firstSkillReq}, tool: {targetInfo.toolReq}";
        gatheringPanel.SetActive(true);
    }
}
