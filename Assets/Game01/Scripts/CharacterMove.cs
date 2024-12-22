using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float playerRotateSpeed = 5f;
    [SerializeField] private float playerMoveSpeed = 3f;
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = TryGetComponent(out rb) ? rb : gameObject.AddComponent<Rigidbody>();
    }

    public void Move(Vector2 inputVector)
    {
        Vector3 MoveDir = new Vector3(inputVector.x, 0, inputVector.y);
        MoveDir = MoveDir.normalized;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * playerRotateSpeed);
        transform.position += MoveDir * playerMoveSpeed * Time.deltaTime;
    }
}
