using System.Collections;
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

    public static SkillType GetTypeFromString(string skillName)
    {
        skillName = skillName.ToLower().Trim();

        switch(skillName)
        {
            case "acrobatics": return SkillType.Acrobatics;
            case "athletics": return SkillType.Athletics;
            case "deception": return SkillType.Deception;
            case "exoticweapons": return SkillType.ExoticWeapons;
            case "firearms": return SkillType.Firearms;
            case "fistfight": return SkillType.FistFight;
            case "insight": return SkillType.Insight;
            case "intimidation": return SkillType.Intimidation;
            case "looting": return SkillType.Looting;
            case "medicine": return SkillType.Medicine;
            case "meleeweapons": return SkillType.MeleeWeapons;
            case "performance": return SkillType.Performance;
            case "persuasion": return SkillType.Persuasion;
            case "repair": return SkillType.Repair;
            case "science": return SkillType.Science;
            case "sleightofhand": return SkillType.SleightOfHand;
            case "stealth": return SkillType.Stealth;
            case "survival": return SkillType.Survival;
            default: return SkillType.ZoneKnowledge;
        }
    }
}
