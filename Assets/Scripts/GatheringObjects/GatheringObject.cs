using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GatheringObject : MonoBehaviour, IPointerClickHandler, IClickInteract
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
        return $"Requaied: {firstSkillReqName}: {firstSkillReq}, {secSkillReqName}: {secSkillReq}.";
    }

    public string GetInteractText()
    {
        return name;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject myGameObject = this.gameObject;
        Debug.Log("Click detected!");
        GatheringPanelManager.instance.OpenGatheringPanel(myGameObject);
    }
}
