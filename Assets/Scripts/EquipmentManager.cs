using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singletone

    public static EquipmentManager instance;

    private void Awake ()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        inventory = Inventory.instance;

        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //получить длину enum массива
        currentEquipment = new Equipment[numberOfSlots]; //сформировать массив
    }

    /*
     * Взять снаряжение из инвентаря, 
     * заменить старое на новое (при совпадении slotIndex)
     */
    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot; //получить числовое значение EquipmentSlot enum

        Equipment oldItem = null;

        //Переместить актуальный инвентарь на панель инвентаря, заменить на выбранный
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        /*if (onEquipmentChanged != null)
        {*/
        onEquipmentChanged?.Invoke(newItem, oldItem);
        //}

        currentEquipment[slotIndex] = newItem; //set item in current active player equipment
    }

    /*
     * Выкинуть одну позицию в инвентарь
     */
    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            onEquipmentChanged?.Invoke(null, oldItem);

            currentEquipment[slotIndex] = null;
        }
    }

    /*
     * Выкинуть всю экипировку из активного в инвентарь
     */
    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
