using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : Creature
{
    int _maxFatigue;
    int _currentFatigue;
    List<Skill> skills = new List<Skill>();
    List<Reputation> reputation = new List<Reputation>();
    List<Feat> feats = new List<Feat>();
    int currentExp;
    int expToLevelUp;
    int level;
}
