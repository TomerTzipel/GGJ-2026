using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private PlayerPosition playerPosition;
    [SerializeField] private Rigidbody2D rb;

    [Header("Move Stats")]
    [SerializeField] private float moveSpeed;

    [Header("Dash Stats")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCD;

    private bool _canMove = true;
    private bool _canDash = true;
    private bool _isDashing;

    private Vector2 _dashDirection = Vector2.right;
    private Vector2 _moveDirection;
    private InputActions _inputActions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (_canMove) Move();
        if (_isDashing) Dash();       
    }

    private void Move()
    {
        Vector3 move = moveSpeed * Time.fixedDeltaTime * _moveDirection;
        rb.MovePosition(transform.position + move);
    }
    private void Dash()
    {
        Vector3 move = dashSpeed * Time.fixedDeltaTime * _dashDirection;
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
        if (!_canDash) return;
        StartDashing();
    }
     
    private void StartDashing()
    {
        _isDashing = true;
        _canMove = false;
        _canDash = false;
        StartCoroutine(DashDuration(dashDuration));
    }
    private void FinishDashing()
    {
        _canMove = true;
        _isDashing = false;
        StartCoroutine(DashCooldown(dashCD));
    }

    private IEnumerator DashDuration(float duration)
    {  
        yield return new WaitForSeconds(duration);
        FinishDashing();
    }
    private IEnumerator DashCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        _canDash = true;
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
