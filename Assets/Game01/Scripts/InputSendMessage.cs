using UnityEngine;
using UnityEngine.InputSystem;

public class InputSendMessage : MonoBehaviour
{
    private const string MOVE = "Move";
    private PlayerInput playerInput;
    private Vector2 vector2;
    CharacterMove characterMove;

    private void Awake()
    {
        playerInput = TryGetComponent(out playerInput) ? playerInput : gameObject.AddComponent<PlayerInput>();
        characterMove = GetComponent<CharacterMove>();
    }

    private void OnEnable()
    {
        if (playerInput == null) return;
        playerInput.actions[MOVE].performed += OnMove;
        playerInput.actions[MOVE].canceled += OnMove;
    }

    private void OnDisable()
    {
        if (playerInput == null) return;
        playerInput.actions[MOVE].performed -= OnMove;
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
