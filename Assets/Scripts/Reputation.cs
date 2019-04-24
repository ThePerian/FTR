using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCGroupType
{
    Bandit, Stalker, Military
}

public class Reputation
{
    RangeInt valueRange = new RangeInt(-5, 11);

    public NPCGroupType groupType;
    public int Value { get; private set; }

    public int Increase()
    {
        Value = Mathf.Clamp(Value + 1, valueRange.start, valueRange.end);
        return Value;
    }

    public int Decrease()
    {
        Value = Mathf.Clamp(Value - 1, valueRange.start, valueRange.end);
        return Value;
    }

    public Reputation(int value, NPCGroupType group)
    {
        Value = Mathf.Clamp(value, valueRange.start, valueRange.end);
        groupType = group;
    }
}
