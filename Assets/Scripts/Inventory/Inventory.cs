using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singletone

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    //список инвентаря
    public List<Item> items = new List<Item>();
    //число слотов для инвентаря
    public int space = 20;

    /*
     * Добавить item в List
     * Item item - позиция инвентаря
     */
    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);

            //хз пока что здесь конкретно вызывается
            if (OnItemChangedCallback != null) // OnItemChangedCallback?.Invoke();
                OnItemChangedCallback.Invoke();
        }

        return true;
    }

    /*
    * Удалить item из List
    * Item item - позиция инвентаря
    */
    public void Remove (Item item)
    {
        items.Remove(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
