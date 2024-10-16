using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay parentDisplay { get; private set; }
    private void Awake()
    {
        ClearSlot();
        itemSprite.preserveAspect = true;
        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        parentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if(slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.icon;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null)  UpdateUISlot(assignedInventorySlot);
    }
    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        
        itemCount.text = "";
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;

    }
    public void OnUISlotClick()
    {
          parentDisplay?.SlotClicked(this);
    }
}
