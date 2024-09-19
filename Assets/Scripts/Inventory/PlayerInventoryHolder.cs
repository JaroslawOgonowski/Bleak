using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem=> secondaryInventorySystem;


    protected override void Awake()
    {
        base.Awake();

        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }
    void Update()
    {
         if(Keyboard.current.bKey.wasPressedThisFrame)
        {
            OnDynamicInventoryDisplayRequested?.Invoke(secondaryInventorySystem); 
        }
    }
}
