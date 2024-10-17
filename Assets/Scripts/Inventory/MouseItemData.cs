using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot AssingnedInventorySlot;
    private bool isDragging = false;
    private Vector3 offset;
    private Transform _playerTransform;
    [SerializeField] private float throwDist = 8f;
    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
        itemSprite.preserveAspect = true;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        AssingnedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    }

    public void UpdateMouseSlot()
    {
        itemSprite.sprite = AssingnedInventorySlot.ItemData.icon;
        itemCount.text = AssingnedInventorySlot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    private void Update()
    {
        if (AssingnedInventorySlot.ItemData != null)
        {
            if (isDragging)
            {
                Vector3 inputPosition = Vector3.zero;

                if (Mouse.current != null)
                {
                    inputPosition = Mouse.current.position.ReadValue();
                }
                else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
                {
                    inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                }

                if (inputPosition != Vector3.zero)
                {
                    inputPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
                    transform.position = worldPosition + offset;
                }
            }

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                if(AssingnedInventorySlot.ItemData.prefab != null) Instantiate(AssingnedInventorySlot.ItemData.prefab, _playerTransform.position + _playerTransform.forward * throwDist, Quaternion.identity);
                
                if(AssingnedInventorySlot.StackSize > 1)
                {
                    AssingnedInventorySlot.AddToStack(-1);
                    UpdateMouseSlot();
                }
                else
                {
                    ClearSlot();
                }
             
            }
        }
    }

    public void ClearSlot()
    {
        AssingnedInventorySlot.ClearSlot();
        itemCount.text = "";
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
    }
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        if (Input.touchCount > 0)
        {
            // Mobile
            eventDataCurrentPosition.position = Input.GetTouch(0).position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //PC
            eventDataCurrentPosition.position = Input.mousePosition;
        }
        else
        {
            return false;
        }

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        Debug.Log(results.Count);
        return results.Count > 4;
    }
    void OnMouseDown()
    {
        if (AssingnedInventorySlot.ItemData != null)
        {
            StartDragging();
        }
    }

    void OnMouseUp()
    {
        StopDragging();
    }

    void OnTouchDown()
    {
        if (AssingnedInventorySlot.ItemData != null)
        {
            StartDragging();
        }
    }

    void OnTouchUp()
    {
        StopDragging();
    }

    private void StartDragging()
    {
        isDragging = true;
        Vector3 inputPosition = Vector3.zero;

        if (Mouse.current != null)
        {
            inputPosition = Mouse.current.position.ReadValue();
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        if (inputPosition != Vector3.zero)
        {
            inputPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
            offset = transform.position - worldPosition;
        }
    }

    private void StopDragging()
    {
        isDragging = false;
    }
}
