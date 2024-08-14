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
    private GameObject currentInteractableObject;

    private void Start()
    {
        InvokeRepeating("Search", 0, 0.2f);
    }

    private void Search()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            if(currentInteractableObject != playerInteract.GetInteractableObject().GetGameObject())
            {
                currentInteractableObject = playerInteract.GetInteractableObject().GetGameObject();
                Show(playerInteract.GetInteractableObject());
            }
        }
        else
        {
            currentInteractableObject = null;
            Hide();
        }
    }

    private void Show(IClickInteract iteractable)
    {
        Debug.Log("show");
        interactPanel.SetActive(true);
        interactText.text= iteractable.GetInteractText();
        icon.sprite = iteractable.GetInteractIcon();
        buttonText.text= iteractable.GetInteractButtonText();
        reqText.text = iteractable.GetInteractReqText();
        LookAt.instance.StartMove(iteractable.GetGameObject());

    }

    private void Hide()
    {
        Debug.Log("hide");
        interactPanel.SetActive(false);
        LookAt.instance.ReturnToStart();

    }
}
