using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour,IHealthComponent
{
    [SerializeField] VoidEventChannel playerDeathEC;
    [SerializeField] FloatEventChannel playerHurtEC;
    [SerializeField] PlayerSettings settings;

    private int _hp;

    private void Awake()
    {
        _hp = settings.MaxHP;
    }

    public void TakeDamage(int damageAmount, MaskType _)
    {
        _hp -= damageAmount;
        if(_hp <= 0)
        {
            playerDeathEC.RaiseEvent();
        }

        playerHurtEC.RaiseEvent((float)_hp/settings.MaxHP);
    }
}
