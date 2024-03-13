using UnityEngine;

public class CMPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;

    private bool _isWalking;

    private void Update()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        _isWalking = moveDir != Vector3.zero;

        transform.position += moveDir * (moveSpeed * Time.deltaTime);

        var rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        Debug.Log(inputVector);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}