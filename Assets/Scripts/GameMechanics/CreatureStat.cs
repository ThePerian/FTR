using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Social, Toughness, Agility, Lookout, Knowledge, Endurance, Reaction
}

public class CreatureStat
{
    RangeInt valueRange = new RangeInt(0, 20);

    public StatType type;
    public int Value { get; private set; }
    public int Mod { get{ return Mathf.FloorToInt((Value - 10f) / 2f); } }

    public int ChangeValue(int amount)
    {
        Value = Mathf.Clamp(Value + amount, valueRange.start, valueRange.end);
        return Value;
    }

    public CreatureStat(StatType type, int value)
    {
        this.type = type;
        Value = Mathf.Clamp(value, valueRange.start, valueRange.end);
    }
}
