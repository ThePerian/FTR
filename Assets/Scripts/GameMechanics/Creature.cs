using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : Damageable
{
    const int BASE_STAT_VALUE = 10;
    const int BASE_SKILL_VALUE = 0;
    const float BASE_WEIGHT = 20;
    const float WEIGHT_MULTIPLIER = 5;
    const int BASE_ARMOR_CLASS = 10;
    const int BASE_HEALTH = 30;
    const int BASE_RADIATION = 30;
    const int HEALTH_MULTIPLIER = 5;
    const int RADIATION_MULTIPLIER = 5;
    const int BASE_ACTION_POINTS = 6;
    const int STARTING_ITEM_SLOTS = 20;
    const int BASE_SPEED = 12;
    const int SPEED_MULTIPLIER = 2;
    
    public Dictionary<StatType, Stat> Stats { get; protected set; }
    public Dictionary<SavingThrowType, SavingThrow> SavingThrows { get; protected set; }
    public Dictionary<SkillType, Skill> Skills { get; protected set; }
    public Inventory Inventory { get; protected set; }
    public List<Condition> Conditions { get; protected set; }
    public float MaxWeight
    {
        get { return BASE_WEIGHT + Stats[StatType.Toughness].Mod * WEIGHT_MULTIPLIER; }
    }
    public float CurrentWeight { get; protected set; }
    public int maxItemSlots = STARTING_ITEM_SLOTS;
    public new int ArmorClass { get { return BASE_ARMOR_CLASS + Stats[StatType.Reaction].Mod; } }
    public new int MaxHealth
    {
        get { return BASE_HEALTH + Stats[StatType.Endurance].Mod * HEALTH_MULTIPLIER; }
    }
    public int MaxRadiation
    {
        get { return BASE_RADIATION + Stats[StatType.Endurance].Mod * RADIATION_MULTIPLIER; }
    }
    public int CurrentRadiation { get; protected set; }
    public int MaxActionPoints { get { return BASE_ACTION_POINTS + Stats[StatType.Agility].Mod; } }
    public int CurrentActionPoints { get; protected set; }
    public int MoveSpeed { get { return BASE_SPEED + Stats[StatType.Agility].Mod * SPEED_MULTIPLIER; } }
    public int DistanceMoved { get; protected set; }
    public new int NaturalResistance { get { return Stats[StatType.Endurance].Mod; } }
    public int InitiativeMod { get { return Stats[StatType.Reaction].Mod; } }

    protected int _tempHealth;

    public Creature() : base()
    {
        Stats = new Dictionary<StatType, Stat>
        {
            {StatType.Social, new Stat(StatType.Social, BASE_STAT_VALUE) },
            {StatType.Toughness, new Stat(StatType.Toughness, BASE_STAT_VALUE) },
            {StatType.Agility, new Stat(StatType.Agility, BASE_STAT_VALUE) },
            {StatType.Lookout, new Stat(StatType.Lookout, BASE_STAT_VALUE) },
            {StatType.Knowledge, new Stat(StatType.Knowledge, BASE_STAT_VALUE) },
            {StatType.Endurance, new Stat(StatType.Endurance, BASE_STAT_VALUE) },
            {StatType.Reaction, new Stat(StatType.Reaction, BASE_STAT_VALUE) }
        };

        SavingThrows = new Dictionary<SavingThrowType, SavingThrow>
        {
            {
                SavingThrowType.Fortitude,
                new SavingThrow
                {
                    baseStat = StatType.Endurance,
                    type = SavingThrowType.Fortitude,
                }
            },
            {
                SavingThrowType.Reflex,
                new SavingThrow
                {
                    baseStat = StatType.Reaction,
                    type = SavingThrowType.Reflex,
                }
            },
            {
                SavingThrowType.Will,
                new SavingThrow
                {
                    baseStat = StatType.Social,
                    type = SavingThrowType.Will,
                }
            },
            {
                SavingThrowType.Vigilance,
                new SavingThrow
                {
                    baseStat = StatType.Lookout,
                    type = SavingThrowType.Vigilance,
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

        Inventory = new Inventory();
        Conditions = new List<Condition>();
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
