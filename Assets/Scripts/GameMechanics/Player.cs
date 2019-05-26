using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    public float maxTravelDistance;
    public float currentTravelDistance;
    public List<Feat> Feats { get; protected set; }
    public int money;
    public string characterName;

    public int statPointsToSpend;
    public int skillPointsToSpend;
    public int featPointsToSpend;

    public Dictionary<string, Condition> Diseases { get; protected set; }
    public Dictionary<NPCGroupType, Reputation> Reputations { get; protected set; }

    public int CurrentExp { get; protected set; }
    public int ExpToLevelUp { get { return GetExpToNextLevel(Level); } }
    public int Level { get; protected set; }

    public int CurrentHunger;
    public int MaxHunger;
    public int CurrentThirst;
    public int MaxThirst;

    static Player instance;

    const int BASE_MAX_TRAVEL_DISTANCE = 10;
    const int BASE_REPUTATION = 0;
    const int BASE_MONEY = 20000;
    const int BASE_STAT_POINTS = 7;
    const int BASE_SKILL_POINTS = 5;
    const int BASE_FEAT_POINTS = 2;
    const int STARTING_LEVEL = 0;
    const int BASE_MAX_HUNGER = 3;
    const int BASE_MAX_THIRST = 3;

    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = new Player();

            return instance;
        }
    }

    protected Player() : base()
    {
        maxTravelDistance = BASE_MAX_TRAVEL_DISTANCE;
        currentTravelDistance = maxTravelDistance;
        CurrentHealth = MaxHealth;
        CurrentRadiation = MaxRadiation;
        Feats = new List<Feat>();
        Reputations = new Dictionary<NPCGroupType, Reputation>
        {
            { NPCGroupType.Bandits, new Reputation(NPCGroupType.Bandits, BASE_REPUTATION)},
            { NPCGroupType.Duty, new Reputation(NPCGroupType.Duty, BASE_REPUTATION)},
            { NPCGroupType.Freedom, new Reputation(NPCGroupType.Freedom, BASE_REPUTATION)},
            { NPCGroupType.Loners, new Reputation(NPCGroupType.Loners, BASE_REPUTATION)},
            { NPCGroupType.Mercenaries, new Reputation(NPCGroupType.Mercenaries, BASE_REPUTATION)},
            { NPCGroupType.Military, new Reputation(NPCGroupType.Military, BASE_REPUTATION)},
            { NPCGroupType.Monolith, new Reputation(NPCGroupType.Monolith, BASE_REPUTATION)},
            { NPCGroupType.Scientists, new Reputation(NPCGroupType.Scientists, BASE_REPUTATION)},
        };
        Diseases = new Dictionary<string, Condition>
        {
            { "Blood poisoning", new Condition()},
            { "Radiation sickness", new Condition()},
            { "Food poisoning", new Condition()},
            { "Common cold", new Condition()}
        };
        characterName = "";
        money = BASE_MONEY;
        statPointsToSpend = BASE_STAT_POINTS;
        skillPointsToSpend = BASE_SKILL_POINTS;
        featPointsToSpend = BASE_FEAT_POINTS;
        CurrentExp = 0;
        Level = STARTING_LEVEL;
        MaxHunger = BASE_MAX_HUNGER;
        CurrentHunger = MaxHunger;
        MaxThirst = BASE_MAX_THIRST;
        CurrentThirst = MaxThirst;
    }

    void Update()
    {
    }

    public void AddFeat(Feat newFeat)
    {
        //do some inspection
        Feats.Add(newFeat);
    }
    
    public void RemoveFeat(Feat featToRemove)
    {
        //do some inspection
        try
        {
            Feats.RemoveAll(x => x.fullName == featToRemove.fullName);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public int AddExperience(int amount)
    {
        //temp
        //TODO: add proper exp evaluation
        CurrentExp += amount;
        return CurrentExp;
    }

    public int GetExpToNextLevel(int currentLevel)
    {
        //temp
        //TODO: add level calculation
        return 100;
    }
}
