﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SkillType
{
    Athletics, FistFight, MeleeWeapons,
    Acrobatics, Stealth, SleightOfHand,
    Repair, Survival, ZoneKnowledge, Science, Medicine, ExoticWeapons,
    Persuasion, Deception, Intimidation, Performance,
    Looting, Insight, Firearms
}

public class Skill
{
    RangeInt valueRange = new RangeInt(0, 6);

    public SkillType name;
    public int Value { get; private set; }
    public StatType baseStat;

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

    public Skill(int value, StatType stat)
    {
        Value = Mathf.Clamp(value, valueRange.start, valueRange.end);
        baseStat = stat;
    }
}
