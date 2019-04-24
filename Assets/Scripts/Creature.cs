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
    int _tempHealth;
    int _actionPoints;
    int _maxRadiation;
    int _currentRadiation;
    int _speed;
    CreatureStat[] _stats = new CreatureStat[6];
    SavingThrow[] _savingThrows = new SavingThrow[3];
    List<InventoryItem> _inventory = new List<InventoryItem>();
    List<CreatureCondition> _conditions = new List<CreatureCondition>();
}
