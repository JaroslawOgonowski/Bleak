using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GatheringObject : MonoBehaviour, IPointerClickHandler
{
    public string name;
    [Tooltip("1 - Mining,\n2 - Lumber\n3 - Harvesting")]
    public byte type;
    [Space(5)]
    public string firstSkillReqName;
    public float firstSkillReq;
    [Space(5)]
    public string secSkillReqName;
    public float secSkillReq;
    [Space(5)]
    public string toolReq;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject myGameObject = this.gameObject;
        Debug.Log("Click detected!");
        GatheringPanelManager.instance.OpenGatheringPanel(myGameObject);
    }
}
