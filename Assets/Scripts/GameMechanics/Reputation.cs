using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCGroupType
{
    Loners, Bandits, Duty, Freedom, Mercenaries, Military, Scientists, Monolith
}

public class Reputation
{
    RangeInt valueRange = new RangeInt(-5, 11);

    public NPCGroupType group;
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

    public Reputation(NPCGroupType groupType, int value)
    {
        Value = Mathf.Clamp(value, valueRange.start, valueRange.end);
        group = groupType;
    }

    public static NPCGroupType GetTypeFromString(string groupName)
    {
        groupName = groupName.ToLower().Trim();

        switch(groupName)
        {
            case "bandits": return NPCGroupType.Bandits;
            case "duty": return NPCGroupType.Duty;
            case "freedom": return NPCGroupType.Freedom;
            case "loners": return NPCGroupType.Loners;
            case "mercenaries": return NPCGroupType.Mercenaries;
            case "military": return NPCGroupType.Military;
            case "monolith": return NPCGroupType.Monolith;
            default: return NPCGroupType.Scientists;
        }
    }
}
