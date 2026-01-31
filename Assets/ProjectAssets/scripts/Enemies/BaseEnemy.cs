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

    private EnemyState _myCurrentState;
    private Rigidbody2D _myRigidbody;
    private float _lastAttackTime;
    private int _currentHealth;

    private void Awake() { _lastAttackTime = _attackCooldown; _currentHealth = _maxHealth; _myRigidbody = GetComponent<Rigidbody2D>(); }
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
                playerHealth.TakeDamage(_attackDamage);

            if (_myEnemyType == EnemyType.Melee) { ChangeState(EnemyState.Idle); }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHurtEC.RaiseEvent(_myEnemyType);
        int calculatedHP = _currentHealth - damageAmount;
        Debug.Log($"Enemy {gameObject.name} Taking {damageAmount} damage, HP left {calculatedHP}");

        if (calculatedHP <= 0) { _currentHealth = 0; Die(); }
        else { _currentHealth = calculatedHP; }
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