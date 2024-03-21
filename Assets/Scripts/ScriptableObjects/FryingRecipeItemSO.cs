using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class FryingRecipeSO : ScriptableObject
    {
        public KitchenItemSO input;
        public KitchenItemSO output;

        public float fryingProgressMax;
    }
}