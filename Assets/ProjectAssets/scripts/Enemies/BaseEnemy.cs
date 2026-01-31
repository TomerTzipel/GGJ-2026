using UnityEngine;
using System;

public enum EnemyType { Melee, Ranged }
public enum EnemyState { Idle, Chasing, PreparingToAttack }
public class BaseEnemy : MonoBehaviour, IHealthComponent
{
    [Header("Events")]
    [SerializeField] protected PlayerPosition playerPosition;
    [SerializeField] protected EnemyTypeEventChannel enemyHurtEC;
    [Header("Base Enemy Data")]
    [SerializeField] protected MaskType _myMaskType;
    [SerializeField] private EnemyType _myEnemyType;
    [SerializeField] protected float _moveSpeed = 3f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackCooldown = 0.75f;
    [SerializeField] protected int _attackDamage = 8;
    [SerializeField] private int _maxHealth = 30;
    [SerializeField] private SpriteRenderer _maskRenderer;
    private EnemyState _myCurrentState;
    private Rigidbody2D _myRigidbody;
    private float _lastAttackTime;
    private int _currentHealth;

    private void Awake() 
    { 
        _lastAttackTime = _attackCooldown; _currentHealth = _maxHealth; _myRigidbody = GetComponent<Rigidbody2D>();
        _myMaskType = (MaskType)UnityEngine.Random.Range(0, 3);

        if(_myEnemyType == EnemyType.Melee) _maskRenderer.sprite = MaskSettings.GetMaleSpriteByType(_myMaskType);
        else _maskRenderer.sprite = MaskSettings.GetFemaleSpriteByType(_myMaskType);

    }
    private void Start()
    {
        if (playerPosition == null) { ChangeState(EnemyState.Idle); return; }

        ChangeState(EnemyState.Chasing);
    }
    private void FixedUpdate()
    {
        _myRigidbody.linearVelocity = Vector2.zero;

        switch (_myCurrentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Chasing:
                UpdateChase();
                break;
            case EnemyState.PreparingToAttack:
                UpdateAttack();
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.TryGetComponent(out IHealthComponent playerHealth))
                playerHealth.TakeDamage(_attackDamage, _myMaskType);

            if (_myEnemyType == EnemyType.Melee) { ChangeState(EnemyState.Idle); }
        }
    }

    public void TakeDamage(int damageAmount, MaskType playerMask)
    {
        enemyHurtEC.RaiseEvent(_myEnemyType);
        damageAmount = (int)CalculateDamage(damageAmount, playerMask);
        int calculatedHP = _currentHealth - damageAmount;

        if (calculatedHP <= 0) { _currentHealth = 0; Die(); }
        else { _currentHealth = calculatedHP; }
    }

    private float CalculateDamage(int damageAmount, MaskType playerMask)
    {
        float mult = 1;

        switch (playerMask)
        {
            case MaskType.Green:
                if (_myMaskType == MaskType.Red) mult = 0.5f;
                if (_myMaskType == MaskType.Blue) mult = 2;
                break;
            case MaskType.Red:
                if (_myMaskType == MaskType.Blue) mult = 0.5f;
                if (_myMaskType == MaskType.Green) mult = 2;
                break;
            case MaskType.Blue:
                if (_myMaskType == MaskType.Green) mult = 0.5f;
                if (_myMaskType == MaskType.Red) mult = 2;
                break;
        }
        return damageAmount * mult;
    }

    protected virtual void UpdateAttack() { }
    protected virtual void UpdateIdle()
    {
        _lastAttackTime -= Time.deltaTime;

        if (_lastAttackTime <= 0) { ChangeState(EnemyState.Chasing); }
    }
    protected virtual void UpdateChase()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition.PlayerDelayedPosition);

        if (distanceToPlayer <= _attackRange) { ChangeState(EnemyState.PreparingToAttack); }
        else
        {
            Vector3 direction = (playerPosition.PlayerDelayedPosition - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
        }
    }

    protected void ChangeState(EnemyState wantedState)
    {
        if (_myCurrentState == wantedState) return;

        _myCurrentState = wantedState;

        switch (_myCurrentState)
        {
            case EnemyState.Idle:
                ChangeToIdle();
                break;
            case EnemyState.Chasing:
                ChangeToChase();
                break;
            case EnemyState.PreparingToAttack:
                ChangeToAttack();
                break;
            default:
                break;
        }
    }

    protected virtual void ChangeToAttack() { }
    protected virtual void ChangeToChase() { }
    protected virtual void ChangeToIdle() { _lastAttackTime = _attackCooldown; }

    protected virtual void Die() { Destroy(gameObject); }

    

}