﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SavingThrowType
{
    Fortitude, Will, Reflex
}

public class SavingThrow
{
    public SavingThrowType type;
    public int value;
    public StatType baseStat;
}

public abstract class Creature : Damageable
{
    const int BASE_STAT_VALUE = 11;
    const int BASE_SAVE_VALUE = 0;
    const int BASE_SKILL_VALUE = 0;
    const float BASE_WEIGHT = 20;
    const float WEIGHT_MULTIPLIER = 5;

    public int maxRadiation;
    public int currentRadiation;
    public Dictionary<StatType, CreatureStat> Stats { get; protected set; }
    public Dictionary<SavingThrowType, SavingThrow> SavingThrows { get; protected set; }
    public Dictionary<SkillType, Skill> Skills { get; protected set; }
    public List<InventoryItem> Inventory { get; protected set; }
    public List<CreatureCondition> Conditions { get; protected set; }
    public float MaxWeight
    {
        get { return BASE_WEIGHT + Stats[StatType.Toughness].Mod * WEIGHT_MULTIPLIER; }
    }
    public float CurrentWeight { get; protected set; }
    public int maxItemSlots = 20;

    protected int _tempHealth;
    protected int _actionPoints;
    protected int _speed;

    public Creature() : base()
    {
        Stats = new Dictionary<StatType, CreatureStat>
        {
            {StatType.Social, new CreatureStat(StatType.Social, BASE_STAT_VALUE) },
            {StatType.Toughness, new CreatureStat(StatType.Toughness, BASE_STAT_VALUE) },
            {StatType.Agility, new CreatureStat(StatType.Agility, BASE_STAT_VALUE) },
            {StatType.Lookout, new CreatureStat(StatType.Lookout, BASE_STAT_VALUE) },
            {StatType.Knowledge, new CreatureStat(StatType.Knowledge, BASE_STAT_VALUE) },
            {StatType.Endurance, new CreatureStat(StatType.Endurance, BASE_STAT_VALUE) },
            {StatType.Reaction, new CreatureStat(StatType.Reaction, BASE_STAT_VALUE) }
        };

        SavingThrows = new Dictionary<SavingThrowType, SavingThrow>
        {
            {
                SavingThrowType.Fortitude,
                new SavingThrow
                {
                    baseStat = StatType.Toughness,
                    type = SavingThrowType.Fortitude,
                    value = BASE_SAVE_VALUE
                }
            },
            {
                SavingThrowType.Reflex,
                new SavingThrow
                {
                    baseStat = StatType.Reaction,
                    type = SavingThrowType.Reflex,
                    value = BASE_SAVE_VALUE
                }
            },
            {
                SavingThrowType.Will,
                new SavingThrow
                {
                    baseStat = StatType.Social,
                    type = SavingThrowType.Will,
                    value = BASE_SAVE_VALUE
                }
            }
        };

        Skills = new Dictionary<SkillType, Skill>
        {
            { SkillType.Acrobatics, new Skill(BASE_SKILL_VALUE, StatType.Agility)},
            { SkillType.Athletics, new Skill(BASE_SKILL_VALUE, StatType.Toughness)},
            { SkillType.Deception, new Skill(BASE_SKILL_VALUE, StatType.Social)},
            { SkillType.ExoticWeapons, new Skill(BASE_SKILL_VALUE, StatType.Knowledge)},
            { SkillType.Firearms, new Skill(BASE_SKILL_VALUE, StatType.Lookout)},
            { SkillType.FistFight, new Skill(BASE_SKILL_VALUE, StatType.Toughness)},
            { SkillType.Insight, new Skill(BASE_SKILL_VALUE, StatType.Lookout)},
            { SkillType.Intimidation, new Skill(BASE_SKILL_VALUE, StatType.Social)},
            { SkillType.Looting, new Skill(BASE_SKILL_VALUE, StatType.Lookout)},
            { SkillType.Medicine, new Skill(BASE_SKILL_VALUE, StatType.Knowledge)},
            { SkillType.MeleeWeapons, new Skill(BASE_SKILL_VALUE, StatType.Toughness)},
            { SkillType.Performance, new Skill(BASE_SKILL_VALUE, StatType.Social)},
            { SkillType.Persuasion, new Skill(BASE_SKILL_VALUE, StatType.Social)},
            { SkillType.Repair, new Skill(BASE_SKILL_VALUE, StatType.Knowledge)},
            { SkillType.Science, new Skill(BASE_SKILL_VALUE, StatType.Knowledge)},
            { SkillType.SleightOfHand, new Skill(BASE_SKILL_VALUE, StatType.Agility)},
            { SkillType.Stealth, new Skill(BASE_SKILL_VALUE, StatType.Agility)},
            { SkillType.Survival, new Skill(BASE_SKILL_VALUE, StatType.Knowledge)},
            { SkillType.ZoneKnowledge, new Skill(BASE_SKILL_VALUE, StatType.Knowledge)},
        };

        Inventory = new List<InventoryItem>();
        Conditions = new List<CreatureCondition>();
    }

    public int ChangeStatValue(StatType stat, int amount)
    {
        Stats[stat].ChangeValue(amount);

        return Stats[stat].Value;
    }

    public int ChangeSkillValue(SkillType skill, int amount)
    {
        if (amount > 0)
            Skills[skill].Increase();
        else if (amount < 0)
            Skills[skill].Decrease();

        return Skills[skill].Value;
    }

    public bool AddItemToInventory(InventoryItem item)
    {
        if ((item.weight + CurrentWeight <= MaxWeight) && (Inventory.Count < maxItemSlots))
        {
            Inventory.Add(item);
            CurrentWeight += item.weight;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveItemFromInventory(InventoryItem item)
    {
        try
        {
            Inventory.Remove(item);
            CurrentWeight -= item.weight;
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }
}
