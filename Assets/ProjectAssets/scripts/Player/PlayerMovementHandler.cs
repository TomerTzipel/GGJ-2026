using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private PlayerPosition playerPosition;
    [SerializeField] private Rigidbody2D rb;


    [SerializeField] private PlayerAttackHandler attackHandler;
    [SerializeField] private PlayerSettings settings;

    public bool CanMove { get; set; } = true;
    public bool CanDash { get; set; } = true;
    private bool _isDashing;

    private Vector2 _dashDirection = Vector2.right;
    private Vector2 _moveDirection;
    private InputActions _inputActions;

    void Awake()
    {
        _inputActions = new InputActions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Move.Enable();
        _inputActions.Player.Dash.Enable();
        _inputActions.Player.Move.performed += HandleMoveInput;
        _inputActions.Player.Move.canceled += HandleMoveInput;
        _inputActions.Player.Dash.performed += HandleDashInput;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.Disable();
        _inputActions.Player.Dash.Disable();
        _inputActions.Player.Move.performed -= HandleMoveInput;
        _inputActions.Player.Move.canceled -= HandleMoveInput;
        _inputActions.Player.Dash.performed -= HandleDashInput;
    }

    /*private void Update()
    {
        if (!_canMove) return;
        transform.Translate(moveSpeed * Time.deltaTime * _moveDirection);
    }*/

    private void Start()
    {
        StartCoroutine(DelayPosition());
    }

    private void FixedUpdate()
    {
        if (CanMove) Move();
        if (_isDashing) Dash();       
    }

    private void Move()
    {
        Vector3 move = settings.MoveSpeed * Time.fixedDeltaTime * _moveDirection;
        rb.MovePosition(transform.position + move);
    }
    private void Dash()
    {
        Vector3 move = settings.DashSpeed * Time.fixedDeltaTime * _dashDirection;
        rb.MovePosition(transform.position + move);
    }
    private void HandleMoveInput(InputAction.CallbackContext callbackContext)
    {
        Vector2 direction = callbackContext.ReadValue<Vector2>().normalized;

        if (direction != Vector2.zero) _dashDirection = direction;

       _moveDirection = direction;
    }

    private void HandleDashInput(InputAction.CallbackContext callbackContext)
    {
        if (!CanDash) return;
        StartDashing();
    }
     
    private void StartDashing()
    {
        attackHandler.CanAttack = false;
        attackHandler.CanAbility = false;
        _isDashing = true;
        CanMove = false;
        CanDash = false;
        StartCoroutine(DashDuration(settings.DashDuration));
    }
    private void FinishDashing()
    {
        attackHandler.CanAttack = true;
        attackHandler.CanAbility = true;
        CanMove = true;
        _isDashing = false;
        StartCoroutine(DashCooldown(settings.DashCD));
    }

    private IEnumerator DashDuration(float duration)
    {  
        yield return new WaitForSeconds(duration);
        FinishDashing();
    }
    private IEnumerator DashCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        CanDash = true;
    }

    private IEnumerator DelayPosition()
    {
        while (true)
        {
            playerPosition.PlayerDelayedPosition = transform.position;
            yield return new WaitForSeconds(playerPosition.SampleDelay);
        }    
    }
}
