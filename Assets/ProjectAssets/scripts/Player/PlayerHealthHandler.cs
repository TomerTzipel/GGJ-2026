using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour,IHealthComponent
{
    [SerializeField] PlayerSettings settings;

    private int _hp;

    private void Awake()
    {
        _hp = settings.MaxHP;
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log($"Player Taking {damageAmount} damage");
        _hp -= damageAmount;
        if(_hp <= 0)
        {
            //death
        }

        //UI Update
    }
}
