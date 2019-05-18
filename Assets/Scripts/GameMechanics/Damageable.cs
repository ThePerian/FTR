using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Bludgeoning, Piercing, Slashing, Acid, Healing
}

public abstract class Damageable
{
    public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; protected set; }
    public int ArmorClass { get; protected set; }
    public int NaturalResistance { get { return 0; } }

    protected int _damageThreshold = 0;
    protected Dictionary<DamageType, int> _damageResistance = new Dictionary<DamageType, int>();

    public virtual int ChangeHealth(int amount, DamageType type)
    {
        amount = Mathf.Abs(amount);

        if (type == DamageType.Healing)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
            return CurrentHealth;
        }

        amount -= _damageResistance[type] + NaturalResistance;

        if (amount < _damageThreshold)
            return CurrentHealth;

        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);

        return CurrentHealth;
    }
}
