using UnityEngine;

//создавать кастомные асеты из меню unity
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

//Для хранения данных общих для объектов
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    /*
     * Click inventory slot with equipment
     */
    public virtual void Use ()
    {
        //Use the item
        //Smth might happen
        Debug.Log("Using " + name);
    }

    /*
     * Remove from inventory if set this on player
     */
    public void RemoveFromInventory ()
    {
        Inventory.instance.Remove(this);
    }

}
