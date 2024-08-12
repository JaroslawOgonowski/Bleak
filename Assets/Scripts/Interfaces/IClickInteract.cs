using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickInteract 
{
    void Interact(Transform interactorTransform);

    string GetInteractText();

    Sprite GetInteractIcon();

    string GetInteractButtonText();

    string GetInteractReqText();
    
    Transform GetTransform();
}
