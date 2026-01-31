using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttackHandler : MonoBehaviour
{
    [SerializeField] private MaskTypeEventChannel playerAttackedEC;
    [SerializeField] private MaskTypeEventChannel playerAbilityEC;
    [SerializeField] private MaskTypeEventChannel MaskChangeEC;
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private PlayerAttackHitbox hitboxPrefab;
    [SerializeField] private ProjectileHandler projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private PlayerMovementHandler movementHandler;
    public bool CanAttack { get; set; } = true;
    public bool CanAbility { get; set; } = true;
    private Vector2 _attackDirection;
    private MaskType _currentMask;
    private PlayerAttackHitbox _currentAttackHitbox;
    private InputActions _inputActions;

    void Awake()
    {
        _inputActions = new InputActions();
        spawnPoint.transform.position = transform.position + transform.right * settings.Range;    
    }

    private void Start()
    {
        MaskChangeEC.RaiseEvent(settings.StartingMask);
    }
    private void OnEnable()
    {
        _inputActions.Player.Attack.Enable();
        _inputActions.Player.Ability.Enable();
        _inputActions.Player.Attack.performed += HandleAttackInput;
        _inputActions.Player.Ability.performed += HandleAbilityInput;
        MaskChangeEC.OnEvent += HandleMaskChange;
    }

    private void OnDisable()
    {
        _inputActions.Player.Attack.Disable();
        _inputActions.Player.Ability.Disable();
        _inputActions.Player.Attack.performed -= HandleAttackInput;
        _inputActions.Player.Ability.performed -= HandleAbilityInput;
        MaskChangeEC.OnEvent -= HandleMaskChange;
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
        _attackDirection = attackDirection;
        float angle = Mathf.Atan2(attackDirection.y,attackDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void HandleAttackInput(InputAction.CallbackContext callbackContext)
    {
        if (!CanAttack) return;
        Attack();
    }

    private void HandleAbilityInput(InputAction.CallbackContext callbackContext)
    {
        if (!CanAbility) return;
        FireProjectile();
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
        playerAttackedEC.RaiseEvent(_currentMask);
    }
    private void FireProjectile()
    {
        CanAbility = false;
        movementHandler.CanMove = false;
        movementHandler.CanDash = false;

        ProjectileHandler projectile = Instantiate(MaskSettings.GetDataByType(_currentMask).ProjectilePrefab, spawnPoint.position, Quaternion.identity);
        projectile.MoveDirection = _attackDirection;
        projectile.Damage = settings.Damage;
        
        StartCoroutine(AbilityCooldown(settings.AbilityCD));
        StartCoroutine(AbilityDuration(settings.AbilityDuration));
        playerAbilityEC.RaiseEvent(_currentMask);
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
    private IEnumerator AbilityCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        CanAbility = true;
    }

    private IEnumerator AttackDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(_currentAttackHitbox.gameObject);
        movementHandler.CanMove = true;
        movementHandler.CanDash = true;
    }
    private IEnumerator AbilityDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        movementHandler.CanMove = true;
        movementHandler.CanDash = true;
    }
}
