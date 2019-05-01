using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Social, Toughness, Agility, Lookout, Knowledge, Endurance, Reaction
}

public struct CreatureStat
{
    public StatType name;
    public int value;
    public int Mod { get { return (value - 10) / 2; } }
}

public struct SavingThrow
{
    public string name;
    public int value;
    public StatType baseStat;
}

public abstract class Creature : Damageable
{
    protected int _tempHealth;
    protected int _actionPoints;
    protected int _maxRadiation;
    protected int _currentRadiation;
    protected int _speed;
    protected CreatureStat[] _stats = new CreatureStat[6];
    protected SavingThrow[] _savingThrows = new SavingThrow[3];
    protected List<InventoryItem> _inventory = new List<InventoryItem>();
    protected List<CreatureCondition> _conditions = new List<CreatureCondition>();
}
