using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SavingThrowType
{
    Fortitude, Will, Reflex, Vigilance
}

public class SavingThrow
{
    public SavingThrowType type;
    public int Value { get { return Player.Instance.Stats[baseStat].Mod + mod; } }
    public int mod;
    public StatType baseStat;
}
