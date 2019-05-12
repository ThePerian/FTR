using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : Creature
{
    public float maxTravelDistance;
    public float currentTravelDistance;
    public List<Feat> Feats { get; private set; }

    static Player instance;
    List<Reputation> reputation;
    int currentExp;
    int expToLevelUp;
    int level;

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
        maxTravelDistance = 10;
        currentTravelDistance = maxTravelDistance;
        MaxHealth = 10;
        CurrentHealth = MaxHealth;
        maxRadiation = 10;
        currentRadiation = maxRadiation;
        Feats = new List<Feat>();
        reputation = new List<Reputation>();
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
            Feats.Remove(featToRemove);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
