using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot; //enum перечисления
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions; // Текущие отрендеренные equipment

    public int armorModifier; //модификатор брони
    public int damageModifier; //модификатор урона

    public override void Use()
    {
        base.Use();
        // Equip the item
        EquipmentManager.instance.Equip(this);
        // Remove it from inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet } //перечисление всего инвентаря
public enum EquipmentMeshRegion { Legs, Arms, Torso}; // Player Body / SkinnedMeshRender - Correspondes to BlendShapes 