using System;
using TotokisUtils.Utils;
using UnityEngine;

public class CMPlayer : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;


    private bool _isWalking;
    private KitchenItem _kitchenItem;
    private Vector3 _lastInteractionDir;
    private BaseCounter _selectedCounter;
    public static CMPlayer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnOnInteractAction;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

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
        return kitchenObjectHoldPoint;
    }

    public bool HasKitchenItem()
    {
        return _kitchenItem != null;
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    public void SetKitchenItem(KitchenItem kitchenItem)
    {
        _kitchenItem = kitchenItem;
    }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    private void GameInputOnOnInteractAction(object sender, EventArgs e)
    {
        if (_selectedCounter)
        {
            _selectedCounter.Interact(this);
        }
    }

    private void HandleInteractions()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractionDir = moveDir;
        }

        var interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractionDir, out var raycastHit, interactDistance,
                counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = .7f;
        var playerHeight = 2f;
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            var moveDirX = moveDir.x.ToVector3X().normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                var moveDirZ = moveDir.z.ToVector3Z().normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        _isWalking = moveDir != Vector3.zero;

        var rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this,
            new OnSelectedCounterChangedEventArgs { SelectedCounter = _selectedCounter });
    }

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }
}