using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private MaskTypeEventChannel MaskChangeEC;
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private PlayerAttackHitbox hitboxPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private PlayerMovementHandler movementHandler;
    public bool CanAttack { get; set; } = true;

    private MaskType _currentMask;
    private PlayerAttackHitbox _currentAttackHitbox;
    private InputActions _inputActions;

    void Awake()
    {
        _inputActions = new InputActions();
        spawnPoint.transform.position = transform.position + transform.right * settings.Range;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 playerScreenPosition = mainCamera.WorldToScreenPoint(transform.position);

        Vector2 attackDirection = mousePosition - playerScreenPosition;
        attackDirection.Normalize();

        float angle = Mathf.Atan2(attackDirection.y,attackDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void OnEnable()
    {
        _inputActions.Player.Attack.Enable();
        _inputActions.Player.Attack.performed += HandleAttackInput;
        MaskChangeEC.OnEvent += HandleMaskChange;
    }

    private void OnDisable()
    {
        _inputActions.Player.Attack.Disable();
        _inputActions.Player.Attack.performed -= HandleAttackInput;
        MaskChangeEC.OnEvent -= HandleMaskChange;
    }

    private void HandleAttackInput(InputAction.CallbackContext callbackContext)
    {
        if (!CanAttack) return;
        Attack();
    }

    private void Attack()
    { 
        CanAttack = false;
        movementHandler.CanMove = false;
        movementHandler.CanDash = false;
        _currentAttackHitbox = Instantiate(hitboxPrefab,spawnPoint.position,Quaternion.identity);
        _currentAttackHitbox.CurrentMask = _currentMask;
        StartCoroutine(AttackDuration(settings.AttackDuration));
        StartCoroutine(AttackCooldown(settings.AttackCD));
    }

    private void HandleMaskChange(MaskType type)
    {
        _currentMask = type;
    }

    private IEnumerator AttackCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        CanAttack = true;
    }

    private IEnumerator AttackDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(_currentAttackHitbox.gameObject);
        movementHandler.CanMove = true;
        movementHandler.CanDash = true;
    }
}
