using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact(CMPlayer player)
    {
        if (player.HasKitchenItem() == false)
        {
            KitchenItem.SpawnKitchenObject(kitchenItemSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler OnPlayerGrabbedObject;
}