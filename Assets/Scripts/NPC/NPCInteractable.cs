using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private Sprite icon;
    [SerializeField] private string buttonText;
    [SerializeField] private string reqText;

    public void Interact()
    {
        GetComponent<NPCRoutine>().StopAndTalk();
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Sprite GetInteractIcon()
    {
        return icon;
    }

    public string GetInteractButtonText()
    {
        return buttonText;
    }


    public string GetInteractReqText()
    {
        return reqText;
    }
}

