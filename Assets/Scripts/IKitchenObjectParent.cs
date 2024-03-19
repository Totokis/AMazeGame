using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenItemFollowTransform();

    public void SetKitchenItem(KitchenItem kitchenItem);

    public KitchenItem GetKitchenItem();

    public void ClearKitchenItem();

    public bool HasKitchenObject();
}