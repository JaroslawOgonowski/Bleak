using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GatheringObject : MonoBehaviour, IPointerClickHandler, IClickInteract
{
    public string name;
    [Tooltip("1 - Mining,\n2 - Lumber\n3 - Harvesting\n4 - Picklock")]
    public GatherSkillList type;
    [Space(5)]
    public GatherSkillList firstSkillReqName;
    public float firstSkillReq;
    [Space(5)]
    public GatherSkillList secSkillReqName;
    public float secSkillReq;
    [Space(5)]
    public string toolReq;
    public Sprite sprite;
    public string GetInteractButtonText()
    {
        return "GET";
    }

    public Sprite GetInteractIcon()
    {
      return sprite;
    }

    public string GetInteractReqText()
    {
        string sndSkill;

        if(secSkillReq==0 || secSkillReq == null)
        {
            sndSkill = ".";
        } else
        {
            sndSkill = $", {secSkillReqName}: {secSkillReq}.";
        }

        return $"Requaied: {firstSkillReqName}: {firstSkillReq}{sndSkill}";
    }

    public string GetInteractText()
    {
        return name;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Interact(Transform interactorTransform)
    {
       Gather.instance.GatherByType(gameObject, type);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject myGameObject = this.gameObject;
        Debug.Log("Click detected!");
        GatheringPanelManager.instance.OpenGatheringPanel(myGameObject);
    }
}
