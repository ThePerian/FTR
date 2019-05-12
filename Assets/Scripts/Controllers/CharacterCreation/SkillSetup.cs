using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetup : MonoBehaviour
{
    //Probably should switch to sliders in UI
    //to avoid this terrible mess in code
    [Header("Remaining points")]
    public Text pointsField;
    [Space]
    [Header("Skill values")]
    public Text athleticsField;
    public Text fistFightField;
    public Text meleeWeaponsField;
    public Text acrobaticsField;
    public Text stealthField;
    public Text sleightOfHandField;
    public Text repairField;
    public Text survivalField;
    public Text zoneKnowledgeField;
    public Text scienceField;
    public Text medicineField;
    public Text exoticWeaponsField;
    public Text persuasionField;
    public Text deceptionField;
    public Text intimidationField;
    public Text performanceField;
    public Text lootingField;
    public Text insightField;
    public Text firearmsField;
    [Space]
    [Header("Skill changing buttons")]
    public Button minusAthletics;
    public Button plusAthletics;
    public Button minusFistFight;
    public Button plusFistFight;
    public Button minusMeleeWeapons;
    public Button plusMeleeWeapons;
    public Button minusAcrobatics;
    public Button plusAcrobatics;
    public Button minusStealth;
    public Button plusStealth;
    public Button minusSleightOfHand;
    public Button plusSleightOfHand;
    public Button minusRepair;
    public Button plusRepair;
    public Button minusSurvival;
    public Button plusSurvival;
    public Button minusZoneKnowledge;
    public Button plusZoneKnowledge;
    public Button minusScience;
    public Button plusScience;
    public Button minusMedicine;
    public Button plusMedicine;
    public Button minusExoticWeapons;
    public Button plusExoticWeapons;
    public Button minusPersuasion;
    public Button plusPersuasion;
    public Button minusDeception;
    public Button plusDeception;
    public Button minusIntimidation;
    public Button plusIntimidation;
    public Button minusPerformance;
    public Button plusPerformance;
    public Button minusLooting;
    public Button plusLooting;
    public Button minusInsight;
    public Button plusInsight;
    public Button minusFirearms;
    public Button plusFirearms;

    int pointsTotal;
    int pointsSpent;
    Player player;
    Dictionary<SkillType, Button[]> allButtons;

    const int MIN_START_VALUE = 0;
    const int MAX_START_VALUE = 3;

    void Start()
    {
        player = Player.Instance;
        pointsTotal = 5 + player.Stats[StatType.Knowledge].Mod;
        pointsSpent = 0;
        allButtons = new Dictionary<SkillType, Button[]>
        {
            { SkillType.Acrobatics, new Button[] { plusAcrobatics, minusAcrobatics } },
            { SkillType.Athletics, new Button[] { plusAthletics, minusAthletics } },
            { SkillType.Deception, new Button[] { plusDeception, minusDeception } },
            { SkillType.ExoticWeapons, new Button[] { plusExoticWeapons, minusExoticWeapons } },
            { SkillType.Firearms, new Button[] { plusFirearms, minusFirearms } },
            { SkillType.FistFight, new Button[] { plusFistFight, minusFistFight } },
            { SkillType.Insight, new Button[] { plusInsight, minusInsight } },
            { SkillType.Intimidation, new Button[] { plusIntimidation, minusIntimidation } },
            { SkillType.Looting, new Button[] { plusLooting, minusLooting } },
            { SkillType.Medicine, new Button[] { plusMedicine, minusMedicine } },
            { SkillType.MeleeWeapons, new Button[] { plusMeleeWeapons, minusMeleeWeapons } },
            { SkillType.Performance, new Button[] { plusPerformance, minusPerformance } },
            { SkillType.Persuasion, new Button[] { plusPersuasion, minusPersuasion } },
            { SkillType.Repair, new Button[] { plusRepair, minusRepair } },
            { SkillType.Science, new Button[] { plusScience, minusScience } },
            { SkillType.SleightOfHand, new Button[] { plusSleightOfHand, minusSleightOfHand } },
            { SkillType.Stealth, new Button[] { plusStealth, minusStealth } },
            { SkillType.Survival, new Button[] { plusSurvival, minusSurvival } },
            { SkillType.ZoneKnowledge, new Button[] { plusZoneKnowledge, minusZoneKnowledge } }
        };

        UpdateSkillValues();
    }
    
    void Update()
    {
        foreach (var item in allButtons)
        {
            DisableButtonsIfBorderValue(item.Key, item.Value[0], item.Value[1]);
        }
        pointsTotal = 5 + player.Stats[StatType.Knowledge].Mod;
        pointsField.text = "ОСТАЛОСЬ ОЧКОВ: " + (pointsTotal - pointsSpent).ToString();
    }

    public void ChangeAcrobaticsValue(int amount)
    {
        ChangeSkillValue(SkillType.Acrobatics, amount);
    }

    public void ChangeAthleticsValue(int amount)
    {
        ChangeSkillValue(SkillType.Athletics, amount);
    }

    public void ChangeDeceptionValue(int amount)
    {
        ChangeSkillValue(SkillType.Deception, amount);
    }

    public void ChangeExoticWeaponsValue(int amount)
    {
        ChangeSkillValue(SkillType.ExoticWeapons, amount);
    }

    public void ChangeFirearmsValue(int amount)
    {
        ChangeSkillValue(SkillType.Firearms, amount);
    }

    public void ChangeFistFightValue(int amount)
    {
        ChangeSkillValue(SkillType.FistFight, amount);
    }

    public void ChangeInsightValue(int amount)
    {
        ChangeSkillValue(SkillType.Insight, amount);
    }

    public void ChangeIntimidationValue(int amount)
    {
        ChangeSkillValue(SkillType.Intimidation, amount);
    }

    public void ChangeLootingValue(int amount)
    {
        ChangeSkillValue(SkillType.Looting, amount);
    }

    public void ChangeMedicineValue(int amount)
    {
        ChangeSkillValue(SkillType.Medicine, amount);
    }

    public void ChangeMeleeWeaponsValue(int amount)
    {
        ChangeSkillValue(SkillType.MeleeWeapons, amount);
    }

    public void ChangePerformanceValue(int amount)
    {
        ChangeSkillValue(SkillType.Performance, amount);
    }

    public void ChangePersuasionValue(int amount)
    {
        ChangeSkillValue(SkillType.Persuasion, amount);
    }

    public void ChangeRepairValue(int amount)
    {
        ChangeSkillValue(SkillType.Repair, amount);
    }

    public void ChangeScienceValue(int amount)
    {
        ChangeSkillValue(SkillType.Science, amount);
    }

    public void ChangeSleightOfHandValue(int amount)
    {
        ChangeSkillValue(SkillType.SleightOfHand, amount);
    }

    public void ChangeStealthValue(int amount)
    {
        ChangeSkillValue(SkillType.Stealth, amount);
    }

    public void ChangeSurvivalValue(int amount)
    {
        ChangeSkillValue(SkillType.Survival, amount);
    }

    public void ChangeZoneKnowledgeValue(int amount)
    {
        ChangeSkillValue(SkillType.ZoneKnowledge, amount);
    }

    void ChangeSkillValue(SkillType skill, int amount)
    {
        amount = Mathf.Clamp(amount, -1, 1);

        int newValue = player.Skills[skill].Value + amount;
        if ((newValue < MIN_START_VALUE) || (newValue > MAX_START_VALUE)) return;

        player.ChangeSkillValue(skill, amount);
        pointsSpent += amount;
        UpdateSkillValues();
    }

    void DisableButtonsIfBorderValue(SkillType skill, Button plusButton, Button minusButton)
    {
        if ((player.Skills[skill].Value == MAX_START_VALUE) || (pointsSpent >= pointsTotal))
            plusButton.interactable = false;
        else
            plusButton.interactable = true;

        if (player.Skills[skill].Value == MIN_START_VALUE)
            minusButton.interactable = false;
        else
            minusButton.interactable = true;
    }

    void UpdateSkillValues()
    {
        // '-_-
        athleticsField.text = player.Skills[SkillType.Athletics].Value.ToString();
        fistFightField.text = player.Skills[SkillType.FistFight].Value.ToString();
        meleeWeaponsField.text = player.Skills[SkillType.MeleeWeapons].Value.ToString();
        acrobaticsField.text = player.Skills[SkillType.Acrobatics].Value.ToString();
        stealthField.text = player.Skills[SkillType.Stealth].Value.ToString();
        sleightOfHandField.text = player.Skills[SkillType.SleightOfHand].Value.ToString();
        repairField.text = player.Skills[SkillType.Repair].Value.ToString();
        survivalField.text = player.Skills[SkillType.Survival].Value.ToString();
        zoneKnowledgeField.text = player.Skills[SkillType.ZoneKnowledge].Value.ToString();
        scienceField.text = player.Skills[SkillType.Science].Value.ToString();
        medicineField.text = player.Skills[SkillType.Medicine].Value.ToString();
        exoticWeaponsField.text = player.Skills[SkillType.ExoticWeapons].Value.ToString();
        persuasionField.text = player.Skills[SkillType.Persuasion].Value.ToString();
        deceptionField.text = player.Skills[SkillType.Deception].Value.ToString();
        intimidationField.text = player.Skills[SkillType.Intimidation].Value.ToString();
        performanceField.text = player.Skills[SkillType.Performance].Value.ToString();
        lootingField.text = player.Skills[SkillType.Looting].Value.ToString();
        insightField.text = player.Skills[SkillType.Insight].Value.ToString();
        firearmsField.text = player.Skills[SkillType.Firearms].Value.ToString();
    }
}
