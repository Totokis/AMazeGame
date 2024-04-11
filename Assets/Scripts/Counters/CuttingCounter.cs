using System;
using ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private CuttingRecipeItemSO[] cutKitchenItemSOArray;

        private int _cuttingProgress;

        public override void Interact(CMPlayer player)
        {
            if (HasKitchenItem() == false)
            {
                if (player.HasKitchenItem())
                {
                    if (HasRecipeWithInput(player.GetKitchenItem().GetKitchenItemSO()))
                    {
                        player.GetKitchenItem().SetKitchenObjectParent(this);
                        _cuttingProgress = 0;

                        var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());

                        OnProgressChanged?.Invoke(this,
                            new OnProgressChangedEventArgs
                                { ProgressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax });
                    }
                }
            }
            else
            {
                if (player.HasKitchenItem())
                {
                }
                else
                {
                    GetKitchenItem().SetKitchenObjectParent(player);
                }
            }
        }

        public override void InteractAlternate(CMPlayer player)
        {
            if (HasKitchenItem() && HasRecipeWithInput(GetKitchenItem().GetKitchenItemSO()))
            {
                _cuttingProgress++;
                var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());
                OnCut?.Invoke(this, EventArgs.Empty);
                OnProgressChanged?.Invoke(this,
                    new OnProgressChangedEventArgs
                        { ProgressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax });

                if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    var input = GetKitchenItem().GetKitchenItemSO();
                    GetKitchenItem().DestroySelf();
                    KitchenItem.SpawnKitchenObject(GetOutputForInput(input), this);
                }
            }
        }

        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnCut;

        private CuttingRecipeItemSO GetCuttingRecipeSOWithInput(KitchenItemSO input)
        {
            foreach (var recipeItemSO in cutKitchenItemSOArray)
                if (recipeItemSO.input == input)
                {
                    return recipeItemSO;
                }

            return null;
        }

        private bool HasRecipeWithInput(KitchenItemSO input)
        {
            return GetCuttingRecipeSOWithInput(input) != null;
        }

        private KitchenItemSO GetOutputForInput(KitchenItemSO input)
        {
            var recipeItemSO = GetCuttingRecipeSOWithInput(input);
            return recipeItemSO != null ? recipeItemSO.output : null;
        }

        public class OnProgressChangedEventArgs : EventArgs
        {
            public float ProgressNormalized;
        }
    }
}