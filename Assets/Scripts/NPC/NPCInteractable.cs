using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : MonoBehaviour, IClickInteract
{
    [SerializeField] private string interactText;
    [SerializeField] private Sprite icon;
    [SerializeField] private string buttonText;
    [SerializeField] private string reqText;

    public void Interact(Transform interactorTransform)
    {
        // Pobranie komponentu Interactor z obiektu interaguj¹cego (np. gracza)
        Interactor interactor = interactorTransform.GetComponent<Interactor>();

        if (interactor != null)
        {
            // Wywo³anie metody Interact w ShopKeeper
            bool interactSuccessful;
            GetComponent<ShopKeeper>().Interact(interactor, out interactSuccessful);

            if (interactSuccessful)
            {
                Debug.Log("Interakcja zakoñczona sukcesem!");
            }
            else
            {
                Debug.Log("Interakcja nie powiod³a siê.");
            }
        }
        else
        {
            Debug.LogError("Nie znaleziono komponentu Interactor na obiekcie.");
        }
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

    public Transform GetTransform()
    {
        return transform;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

