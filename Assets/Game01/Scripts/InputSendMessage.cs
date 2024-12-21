using UnityEngine;
using UnityEngine.InputSystem;

public class InputSendMessage : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 vector2;
    private Rigidbody rb;
    CharacterMove characterMove;

    private void Awake()
    {
        rb = TryGetComponent(out rb) ? rb : gameObject.AddComponent<Rigidbody>();
        playerInput = TryGetComponent(out playerInput) ? playerInput : gameObject.AddComponent<PlayerInput>();
        characterMove = GetComponent<CharacterMove>();
    }

    private void OnEnable()
    {
        if (playerInput == null) return;
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
    }

    private void OnDisable()
    {
        if (playerInput == null) return;
        playerInput.actions["Move"].performed -= OnMove;
    }

    private void Update()
    {
        characterMove.Move(vector2);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        vector2 = context.ReadValue<Vector2>();
    }
}
