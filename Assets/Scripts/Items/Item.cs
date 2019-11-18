using UnityEngine;

//создавать кастомные асеты из меню unity
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

//Для хранения данных общих для объектов
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use ()
    {
        //Use the item
        //Smth might happen
        Debug.Log("Using " + name);
    }

}
