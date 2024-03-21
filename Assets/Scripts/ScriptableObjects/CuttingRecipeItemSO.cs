using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeItemSO : ScriptableObject
{
    public KitchenItemSO input;
    public KitchenItemSO output;

    public int cuttingProgressMax;
}