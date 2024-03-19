using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact(CMPlayer player)
    {
        if (player.HasKitchenItem() == false)
        {
            var kitchenItemTransform = Instantiate(kitchenItemSO.prefab);
            kitchenItemTransform.GetComponent<KitchenItem>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler OnPlayerGrabbedObject;
}