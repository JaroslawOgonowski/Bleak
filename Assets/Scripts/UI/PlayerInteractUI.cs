using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject interactPanel;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI reqText;
    [SerializeField] private Image icon;

    private void Update()
    {
        if(playerInteract.GetInteractableObject() != null)
        {
            Show(playerInteract.GetInteractableObject());
        } else
        {
            Hide();
        }
    }

    private void Show(NPCInteractable npcInteractable)
    {
        interactPanel.SetActive(true);
        interactText.text= npcInteractable.GetInteractText();
        icon.sprite = npcInteractable.GetInteractIcon();
        buttonText.text= npcInteractable.GetInteractButtonText();
        reqText.text = npcInteractable.GetInteractReqText();
    }

    private void Hide()
    {
        interactPanel.SetActive(false);
    }
}
