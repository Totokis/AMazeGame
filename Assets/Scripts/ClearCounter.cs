using UnityEngine;
using UnityEngine.Serialization;

public class ClearCounter : MonoBehaviour
{
    [FormerlySerializedAs("kitchenItem")]
    [SerializeField]
    private KitchenItemSO kitchenItemSO;

    [SerializeField] private Transform counterTopPoint;

    private KitchenItem _kitchenItem;

    public void Interact()
    {
        if (_kitchenItem == null)
        {
            var kitchenItemTransform = Instantiate(kitchenItemSO.prefab, counterTopPoint);
            kitchenItemTransform.localPosition = Vector3.zero;
            _kitchenItem = kitchenItemTransform.GetComponent<KitchenItem>();

            _kitchenItem.SetClearCounter(this);
        }
        else
        {
            Debug.Log(_kitchenItem.GetClearCounter());
        }
    }
}