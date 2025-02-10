using UnityEngine;
using UnityEngine.InputSystem;

public class InputSendMessage : MonoBehaviour
{
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private const string MOVE = "Move";
    private PlayerInput playerInput;
    private Vector2 vector2;
    private Player player;
    private Animator animator;

    private void Awake()
    {
        playerInput = TryGetComponent(out playerInput) ? playerInput : gameObject.AddComponent<PlayerInput>();
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
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
        player.Move(vector2);
        
        bool isMoving = playerInput.actions[MOVE].ReadValue<Vector2>().sqrMagnitude > 0;
        animator.SetBool(IsRun, isMoving);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        vector2 = context.ReadValue<Vector2>();
    }
}
