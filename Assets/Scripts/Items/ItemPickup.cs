using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    /*
     * Переопределение метода для работы с current transform
     */
    public override void Interacte()
    {
        base.Interacte();

        PickUp();
    }

    /*
     * Подобрать инвентарь
     */
    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        //Add to inventory
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
