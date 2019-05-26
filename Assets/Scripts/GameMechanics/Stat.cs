using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Social, Toughness, Agility, Lookout, Knowledge, Endurance, Reaction
}

public class Stat
{
    RangeInt valueRange = new RangeInt(0, 20);

    public StatType type;
    public int Value { get; private set; }
    public int Mod { get{ return Mathf.FloorToInt((Value - 10f) / 2f); } }
    public const int MAX_PLAYER_VALUE = 20;
    public const int MIN_PLAYER_VALUE = 0;

    public int ChangeValue(int amount)
    {
        Value = Mathf.Clamp(Value + amount, valueRange.start, valueRange.end);
        return Value;
    }

    public Stat(StatType type, int value)
    {
        this.type = type;
        Value = Mathf.Clamp(value, valueRange.start, valueRange.end);
    }

    public static StatType GetTypeFromString(string statName)
    {
        statName = statName.ToLower().Trim();
        switch(statName)
        {
            case "social": return StatType.Social;
            case "toughness": return StatType.Toughness;
            case "agility": return StatType.Agility;
            case "lookout": return StatType.Lookout;
            case "knowledge": return StatType.Knowledge;
            case "endurance": return StatType.Endurance;
            default: return StatType.Reaction;
        }
    }
}
