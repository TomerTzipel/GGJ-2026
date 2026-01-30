using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;

    private bool _canMove = true;
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
        StartDashing();
    }
     
    private void StartDashing()
    {
        _isDashing = true;
        _canMove = false;
        StartCoroutine(DashDuration(dashDuration));
    }
    private void FinishDashing()
    {
        _canMove = true;
        _isDashing = false;
    }

    private IEnumerator DashDuration(float duration)
    {  
        yield return new WaitForSeconds(duration);
        FinishDashing();
    }
}
