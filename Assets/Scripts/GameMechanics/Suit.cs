using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suit : InventoryItem
{
    public int maxHealth;
    public int currentHealth;
    public int damageThreshold;
    public Dictionary<DamageType, int> damageResistance;
    public bool hasFlashlight;
    public bool hasNightVision;
    public bool isHealing; //TODO: think of something better to store buffs of mutated suits
    public bool isRigid;
    public bool isExoskeleton;
    public bool hasBreathingSystem;
    public bool hasDisguise;
    public bool hasHelmet;
    public InventoryItem helmet;
    public InventoryItem flashlight;
    public InventoryItem nightVision;
}
