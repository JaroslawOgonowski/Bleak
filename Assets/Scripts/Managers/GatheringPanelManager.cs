using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GatheringPanelManager : MonoBehaviour
{
    public static GatheringPanelManager instance;
    public GameObject farAwayPanel;
    [SerializeField] public GameObject gatheringPanel;
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
        farAwayPanel.SetActive(false);
        gatheringPanel.SetActive(false);
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
        if (target != Gather.instance.currentTarget)
        {
            currentTarget = target;
            GatheringObject targetInfo = target.GetComponent<GatheringObject>();
            gatheringPanelTitle.text = targetInfo.name;
            gatheringPanelContent.text = $"Requirements: {targetInfo.firstSkillReqName}: {targetInfo.firstSkillReq}, Tool: {targetInfo.toolReq}";
            gatheringButton.GetComponentInChildren<TextMeshProUGUI>().text = "Get";

          
            
            gatheringPanel.SetActive(true);
        }
    }
    public IEnumerator FarAwayPanelOpen()
    {
        farAwayPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        farAwayPanel.SetActive(false);

    }
}
