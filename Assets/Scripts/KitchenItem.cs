using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    [SerializeField] private KitchenItemSO kitchenItem;
    private IKitchenObjectParent _kitchenObjectParent;


    public KitchenItemSO GetKitchenItemSO()
    {
        return kitchenItem;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return _kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (_kitchenObjectParent != null)
        {
            _kitchenObjectParent.ClearKitchenItem();
        }

        _kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenItem())
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenItem");
        }

        kitchenObjectParent.SetKitchenItem(this);

        transform.parent = kitchenObjectParent.GetKitchenItemFollowTransform();
        transform.localPosition = Vector3.zero;
    }
}