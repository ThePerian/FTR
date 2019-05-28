using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : InventoryItem
{
    public int damage;
    public DamageType damageType;
    public int maxHealth;
    public int currentHealth;
    public RangeInt jammingRange;
    public float firingDistance;
    public int maxAmmo;
    public int currentAmmo;
    public string caliber;
    public int accuracy;
}
