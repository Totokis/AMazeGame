using System;
using ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class StoveCounter : BaseCounter
    {
        private enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }
        
        [SerializeField] private FryingRecipeSO[] fryingRecipeSoArray;

        private float _fryingTimer;
        private FryingRecipeSO _fryingRecipeSO;
        private State _state;

        private void Start()
        {
            _state = State.Idle;
        }

        private void Update()
        {
            if (HasKitchenItem())
            {
                switch (_state)
                {
                    case State.Idle:
                        break;
                    case State.Frying:
                        _fryingTimer += Time.deltaTime;
                
                        if (_fryingTimer > _fryingRecipeSO.fryingProgressMax)
                        {
                            _fryingTimer = 0f;
                            GetKitchenItem().DestroySelf();

                            KitchenItem.SpawnKitchenObject(_fryingRecipeSO.output, this);

                            _state = State.Fried;
                            Debug.Log("Object fried!");
                        }
                        break;
                    case State.Fried:
                        break;
                    case State.Burned:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Debug.Log(_state);
                
            }
            
        }

        public override void Interact(CMPlayer player)
        {
            if (HasKitchenItem() == false)
            {
                if (player.HasKitchenItem())
                {
                    if (HasRecipeWithInput(player.GetKitchenItem().GetKitchenItemSO()))
                    {
                        player.GetKitchenItem().SetKitchenObjectParent(this); 
                        _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());
                        _state = State.Frying;

                        _fryingTimer = 0f;
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
        
        private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenItemSO input)
        {
            foreach (var fryingRecipeSO in fryingRecipeSoArray)
                if (fryingRecipeSO.input == input)
                {
                    return fryingRecipeSO;
                }

            return null;
        }

        private bool HasRecipeWithInput(KitchenItemSO input)
        {
            return GetFryingRecipeSOWithInput(input) != null;
        }

        private KitchenItemSO GetOutputForInput(KitchenItemSO input)
        {
            var fryingRecipeSO = GetFryingRecipeSOWithInput(input);
            return fryingRecipeSO != null ? fryingRecipeSO.output : null;
        }

        public class OnProgressChangedEventArgs : EventArgs
        {
            public float ProgressNormalized;
        }
    }
    
    
}