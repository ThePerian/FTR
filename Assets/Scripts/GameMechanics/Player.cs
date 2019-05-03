using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : Creature
{
    public float maxTravelDistance;
    public float currentTravelDistance;

    static Player instance;
    List<Skill> skills = new List<Skill>();
    List<Reputation> reputation = new List<Reputation>();
    List<Feat> feats = new List<Feat>();
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

    protected Player()
    {
        maxTravelDistance = 10;
        currentTravelDistance = maxTravelDistance;
        MaxHealth = 10;
        CurrentHealth = MaxHealth;
        maxRadiation = 10;
        currentRadiation = maxRadiation;
    }

    void Update()
    {
    }
}
