using UnityEngine;

public class CMPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool _isWalking;

    private void Update()
    {
        var inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }

        inputVector = inputVector.normalized;

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