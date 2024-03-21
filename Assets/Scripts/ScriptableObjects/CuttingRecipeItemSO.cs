using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class CuttingRecipeItemSO : ScriptableObject
    {
        public KitchenItemSO input;
        public KitchenItemSO output;

        public int cuttingProgressMax;
    }
}