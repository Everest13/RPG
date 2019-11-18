using UnityEngine;
using UnityEngine.UI;

/*
 * Work with current slot in Inventory panel UI position
 */
public class InventorySlot : MonoBehaviour
{
    public Image icon; //icon in ItemButton(InventarySlot)
    public Button removeButton;

    Item item; //add pickuped object

    /*
     * Добавить item в слот
     */
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon; //
        icon.enabled = true;
        removeButton.interactable = true; //set remove button is visible
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    /*
     * Remove item from inventory slot (OnClick())
     */
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem ()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
