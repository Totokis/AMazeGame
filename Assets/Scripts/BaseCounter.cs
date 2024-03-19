using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenItem _kitchenItem;

    public void ClearKitchenItem()
    {
        _kitchenItem = null;
    }

    public KitchenItem GetKitchenItem()
    {
        return _kitchenItem;
    }

    public Transform GetKitchenItemFollowTransform()
    {
        return counterTopPoint;
    }

    public bool HasKitchenItem()
    {
        return _kitchenItem != null;
    }

    public virtual void Interact(CMPlayer cmPlayer)
    {
        Debug.LogError("BaseCounter.Interact()");
    }

    public void SetKitchenItem(KitchenItem kitchenItem)
    {
        _kitchenItem = kitchenItem;
    }
}