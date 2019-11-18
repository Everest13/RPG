using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent; //all slots in inventory panel
    public GameObject inventoryUI; //inventory panel

    Inventory inventory;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance; //Singetone
        inventory.OnItemChangedCallback += UpdateUI; //delegate in inventory Add, Remove

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    /*
     * Get i/b Inventory buttons - hide/show inventory panel
     */
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    /*
     * Find all of the inventory slots
     * and update inventary panel
     */
    void UpdateUI()
    {
        for (int i=0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
