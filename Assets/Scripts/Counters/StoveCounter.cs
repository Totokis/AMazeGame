namespace Counters
{
    using System;
    using ScriptableObjects;
    using UnityEngine;

    public class StoveCounter : BaseCounter
    {

        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }

        [SerializeField] private FryingRecipeSO[] fryingRecipeSoArray;
        [SerializeField] private BurningRecipeSO[] burningRecipeSoArray;
        private BurningRecipeSO _burningRecipeSo;
        private float _burningTimer;
        private FryingRecipeSO _fryingRecipeSo;

        private float _fryingTimer;
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
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            State = _state
                        });
                        break;
                    case State.Frying:

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            State = _state
                        });
                        _fryingTimer += Time.deltaTime;

                        if (_fryingTimer > _fryingRecipeSo.fryingProgressMax)
                        {
                            _fryingTimer = 0f;
                            GetKitchenItem().DestroySelf();

                            KitchenItem.SpawnKitchenObject(_fryingRecipeSo.output, this);

                            _state = State.Fried;
                            _burningTimer = 0f;

                            _burningRecipeSo = GetBurningRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());


                        }

                        break;
                    case State.Fried:
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            State = _state
                        });
                        _burningTimer += Time.deltaTime;
                        if (_burningTimer > _burningRecipeSo.burningProgressMax)
                        {
                            GetKitchenItem().DestroySelf();
                            KitchenItem.SpawnKitchenObject(_burningRecipeSo.output, this);
                        }

                        break;
                    case State.Burned:
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            State = _state
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Debug.Log(_state);

            }

        }

        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenItemSO input)
        {
            foreach (var burningRecipeSo in burningRecipeSoArray)
            {
                if (burningRecipeSo.input == input)
                {
                    return burningRecipeSo;
                }
            }

            return null;
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
                        _fryingRecipeSo = GetFryingRecipeSOWithInput(GetKitchenItem().GetKitchenItemSO());
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

        public class OnStateChangedEventArgs : EventArgs
        {
            public State State;
        }

        public class OnProgressChangedEventArgs : EventArgs
        {
            public float ProgressNormalized;
        }
    }


}