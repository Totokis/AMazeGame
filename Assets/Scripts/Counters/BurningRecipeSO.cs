namespace Counters
{
    using ScriptableObjects;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Create BurningRecipeSO", fileName = "BurningRecipeSO", order = 0)]
    public class BurningRecipeSO : ScriptableObject
    {
        public KitchenItemSO input;
        public KitchenItemSO output;

        public float burningProgressMax;
    }
}