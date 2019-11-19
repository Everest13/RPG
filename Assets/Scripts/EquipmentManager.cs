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

    Equipment[] currentEquipment; //Items we currenlty have equipped
    SkinnedMeshRenderer[] currentMeshes;

    Inventory inventory;

    public Equipment[] defaultItems; //дефолтная экипировка
    public SkinnedMeshRenderer targetMesh; //Player Body
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem); // Callback for when an item is equippped/unequipped
    public OnEquipmentChanged onEquipmentChanged;

    void Start()
    {
        inventory = Inventory.instance; // Reference to our inventory

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //получить длину enum массива
        currentEquipment = new Equipment[numSlots]; //сформировать массив
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems(); //дефолтно экипироваться
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    /*
     * Взять снаряжение из инвентаря, 
     * заменить старое на новое (при совпадении slotIndex)
     */
    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot; //получить числовое значение EquipmentSlot enum
        //снять выбр экипировку
        Equipment oldItem = Unequip(slotIndex); ;

        //Переместить актуальный инвентарь на панель инвентаря, заменить на выбранный
        //чтот грохнуть решили
        /*if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }*/

        /*if (onEquipmentChanged != null)
        {*/
        onEquipmentChanged?.Invoke(newItem, oldItem);
        //}
        SetEquipmentBlendShapes(newItem, 100); //Set body blendship to new item equipment

        currentEquipment[slotIndex] = newItem; //set item in current active player equipment
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh); //получить меш из active player equipment
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    /*
     * Выкинуть одну позицию в инвентарь
     * вернуть Equipment oldItem
     */
    public Equipment Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject); // Unrender old equipment
            }

            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0); //come back default Body blendship

            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;

            onEquipmentChanged?.Invoke(null, oldItem);

            return oldItem;
        }

        return null;
    }

    /*
     * Выкинуть всю экипировку из активного в инвентарь
     */
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    //Установить размеры Player Body под Equipment
    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions) //перебираем blendShape в отрендеренных equipment
        {
            //подгоняем body по weight equipment
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }
}
