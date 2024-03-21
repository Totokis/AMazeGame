using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    [SerializeField] private KitchenItemSO kitchenItem;
    private IKitchenObjectParent _kitchenObjectParent;


    public void DestroySelf()
    {
        _kitchenObjectParent.ClearKitchenItem();
        Destroy(gameObject);
    }

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

    public static KitchenItem SpawnKitchenObject(KitchenItemSO kitchenItemSO, IKitchenObjectParent parent)
    {
        var kitchenItemTransform = Instantiate(kitchenItemSO.prefab);
        var kitchenItem = kitchenItemTransform.GetComponent<KitchenItem>();
        kitchenItem.SetKitchenObjectParent(parent);
        return kitchenItem;
    }
}