using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Bludgeoning, Piercing, Slashing, Acid, Healing
}

public abstract class Damageable : MonoBehaviour
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public int ArmorClass { get; private set; }

    int _damageThreshold = 0;
    Dictionary<DamageType, int> _damageResistance = new Dictionary<DamageType, int>();

    public virtual int ChangeHealth(int amount, DamageType type)
    {
        amount = Mathf.Abs(amount);

        if (type == DamageType.Healing)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
            return CurrentHealth;
        }

        if ((amount < _damageThreshold) || (amount < _damageResistance[type]))
            return CurrentHealth;

        amount -= _damageResistance[type];
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);

        return CurrentHealth;
    }
}
