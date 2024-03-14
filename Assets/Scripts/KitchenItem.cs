using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    [SerializeField] private KitchenItemSO kitchenItem;
    private ClearCounter _clearCounter;

    public ClearCounter GetClearCounter()
    {
        return _clearCounter;
    }


    public KitchenItemSO GetKitchenItemSO()
    {
        return kitchenItem;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        _clearCounter = clearCounter;
    }
}