using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreen : MonoBehaviour
{
    //TODO: add procedural generation of stat and skill fields
    //should be easier to control, too
    [Header("Заголовок экрана")]
    public Text nameText;
    public Text levelText;
    public Image levelBarMask;
    public Transform conditionsArea;
    public GameObject conditionPrefab;

    [Header("Характеристики")]
    public Text statPointsLeft;
    public Text socialValue;
    public Text socialMod;
    public Button plusSocial;
    public Button minusSocial;
    public Text toughnessValue;
    public Text toughnessMod;
    public Button plusToughness;
    public Button minusToughness;
    public Text agilityValue;
    public Text agilityMod;
    public Button plusAgility;
    public Button minusAgility;
    public Text lookoutValue;
    public Text lookoutMod;
    public Button plusLookout;
    public Button minusLookout;
    public Text knowledgeValue;
    public Text knowledgeMod;
    public Button plusKnowledge;
    public Button minusKnowledge;
    public Text enduranceValue;
    public Text enduranceMod;
    public Button plusEndurance;
    public Button minusEndurance;
    public Text reactionValue;
    public Text reactionMod;
    public Button plusReaction;
    public Button minusReaction;

    [Header("Навыки")]
    public Text skillPointsLeft;
    public Text athleticsValue;
    public Button plusAthletics;
    public Button minusAthletics;
    public Text fistFightValue;
    public Button plusFistFight;
    public Button minusFistFight;
    public Text meleeWeaponsValue;
    public Button plusMeleeWeapons;
    public Button minusMeleeWeapons;
    public Text acrobaticsValue;
    public Button plusAcrobatics;
    public Button minusAcrobatics;
    public Text stealthValue;
    public Button plusStealth;
    public Button minusStealth;
    public Text sleightOfHandValue;
    public Button plusSleightOfHand;
    public Button minusSleightOfHand;
    public Text repairValue;
    public Button plusRepair;
    public Button minusRepair;
    public Text survivalValue;
    public Button plusSurvival;
    public Button minusSurvival;
    public Text zoneKnowledgeValue;
    public Button plusZoneKnowledge;
    public Button minusZoneKnowledge;
    public Text scienceValue;
    public Button plusScience;
    public Button minusScience;
    public Text medicineValue;
    public Button plusMedicine;
    public Button minusMedicine;
    public Text exoticWeaponsValue;
    public Button plusExoticWeapons;
    public Button minusExoticWeapons;
    public Text persuasionValue;
    public Button plusPersuasion;
    public Button minusPersuasion;
    public Text deceptionValue;
    public Button plusDeception;
    public Button minusDeception;
    public Text intimidationValue;
    public Button plusIntimidation;
    public Button minusIntimidation;
    public Text performanceValue;
    public Button plusPerformance;
    public Button minusPerformance;
    public Text lootingValue;
    public Button plusLooting;
    public Button minusLooting;
    public Text insightValue;
    public Button plusInsight;
    public Button minusInsight;
    public Text firearmsValue;
    public Button plusFirearms;
    public Button minusFirearms;

    [Header("Черты")]
    public Text featPointsLeft;
    public Transform featArea;
    public GameObject featPrefab;

    [Header("Болезни")]
    public Text bloodPoisoningField;
    public Text radiationSicknessField;
    public Text foodPoisoningField;
    public Text commonColdField;

    [Header("Репутация")]
    public Text lonersField;
    public Text banditsField;
    public Text dutyField;
    public Text freedomField;
    public Text mercenariesField;
    public Text militaryField;
    public Text scientistsField;
    public Text monolithField;

    [Header("Прочие параметры")]
    public Text ACValue;
    public Text HPValue;
    public Text RPValue;
    public Text APValue;
    public Text fatigueValue;
    public Text hungerValue;
    public Text thirstValue;
    public Text speedValue;
    public Text initiativeValue;
    public Text resistanceValue;
    public Text fortitudeValue;
    public Text willValue;
    public Text reflexValue;
    public Text vigilanceValue;
    public Text weightValue;

    Player player;
    ProgressBar levelBar;
    Dictionary<StatType, Button[]> statButtons;
    Dictionary<StatType, Text> statValueFields;
    Dictionary<StatType, Text> statModFields;
    Dictionary<SkillType, Button[]> skillButtons;
    Dictionary<SkillType, Text> skillValueFields;
    Dictionary<StatType, int> startStatValues;
    Dictionary<SkillType, int> startSkillValues;
    List<Feat> availibleFeats;

    private void Start()
    {
    }

    private void Awake()
    {
        player = Player.Instance;
        startStatValues = new Dictionary<StatType, int>();
        startSkillValues = new Dictionary<SkillType, int>();
        levelBar = new ProgressBar(levelBarMask, player.CurrentExp, player.ExpToLevelUp);
        //TODO: add proper initialization
        availibleFeats = new List<Feat>()
        {
            new Feat(){fullName = "feat1", description = "desc1"},
            new Feat(){fullName = "feat2", description = "desc2"},
            new Feat(){fullName = "feat3", description = "desc3"}
        };
        statButtons = new Dictionary<StatType, Button[]>
        {
            { StatType.Agility, new Button[] { plusAgility, minusAgility } },
            { StatType.Endurance, new Button[] { plusEndurance, minusEndurance } },
            { StatType.Knowledge, new Button[] { plusKnowledge, minusKnowledge } },
            { StatType.Lookout, new Button[] { plusLookout, minusLookout } },
            { StatType.Reaction, new Button[] { plusReaction, minusReaction } },
            { StatType.Social, new Button[] { plusSocial, minusSocial } },
            { StatType.Toughness, new Button[] { plusToughness, minusToughness } }
        };
        statValueFields = new Dictionary<StatType, Text>
        {
            { StatType.Agility, agilityValue },
            { StatType.Endurance, enduranceValue },
            { StatType.Knowledge, knowledgeValue },
            { StatType.Lookout, lookoutValue },
            { StatType.Reaction, reactionValue },
            { StatType.Social, socialValue },
            { StatType.Toughness, toughnessValue }
        };
        statModFields = new Dictionary<StatType, Text>
        {
            { StatType.Agility, agilityMod },
            { StatType.Endurance, enduranceMod },
            { StatType.Knowledge, knowledgeMod },
            { StatType.Lookout, lookoutMod },
            { StatType.Reaction, reactionMod },
            { StatType.Social, socialMod },
            { StatType.Toughness, toughnessMod }
        };
        skillButtons = new Dictionary<SkillType, Button[]>
        {
            { SkillType.Acrobatics, new Button[]{ plusAcrobatics, minusAcrobatics } },
            { SkillType.Athletics, new Button[]{ plusAthletics, minusAthletics } },
            { SkillType.Deception, new Button[]{ plusDeception, minusDeception } },
            { SkillType.ExoticWeapons, new Button[]{ plusExoticWeapons, minusExoticWeapons } },
            { SkillType.Firearms, new Button[]{ plusFirearms, minusFirearms } },
            { SkillType.FistFight, new Button[]{ plusFistFight, minusFistFight } },
            { SkillType.Insight, new Button[]{ plusInsight, minusInsight } },
            { SkillType.Intimidation, new Button[]{ plusIntimidation, minusIntimidation } },
            { SkillType.Looting, new Button[]{ plusLooting, minusLooting } },
            { SkillType.Medicine, new Button[]{ plusMedicine, minusMedicine } },
            { SkillType.MeleeWeapons, new Button[]{ plusMeleeWeapons, minusMeleeWeapons } },
            { SkillType.Performance, new Button[]{ plusPerformance, minusPerformance } },
            { SkillType.Persuasion, new Button[]{ plusPersuasion, minusPersuasion } },
            { SkillType.Repair, new Button[]{ plusRepair, minusRepair } },
            { SkillType.Science, new Button[]{ plusScience, minusScience } },
            { SkillType.SleightOfHand, new Button[]{ plusSleightOfHand, minusSleightOfHand } },
            { SkillType.Stealth, new Button[]{ plusStealth, minusStealth } },
            { SkillType.Survival, new Button[]{ plusSurvival, minusSurvival } },
            { SkillType.ZoneKnowledge, new Button[]{ plusZoneKnowledge, minusZoneKnowledge } }
        };
        skillValueFields = new Dictionary<SkillType, Text>
        {
            { SkillType.Acrobatics, acrobaticsValue },
            { SkillType.Athletics, athleticsValue },
            { SkillType.Deception, deceptionValue },
            { SkillType.ExoticWeapons, exoticWeaponsValue },
            { SkillType.Firearms, firearmsValue },
            { SkillType.FistFight, fistFightValue },
            { SkillType.Insight, insightValue },
            { SkillType.Intimidation, intimidationValue },
            { SkillType.Looting, lootingValue },
            { SkillType.Medicine, medicineValue },
            { SkillType.MeleeWeapons, meleeWeaponsValue },
            { SkillType.Performance, performanceValue },
            { SkillType.Persuasion, persuasionValue },
            { SkillType.Repair, repairValue },
            { SkillType.Science, scienceValue },
            { SkillType.SleightOfHand, sleightOfHandValue },
            { SkillType.Stealth, stealthValue },
            { SkillType.Survival, survivalValue },
            { SkillType.ZoneKnowledge, zoneKnowledgeValue }
        };
    }

    private void OnEnable()
    {
        startStatValues = new Dictionary<StatType, int>();
        foreach (var item in player.Stats)
            startStatValues.Add(item.Key, item.Value.Value);
        startSkillValues = new Dictionary<SkillType, int>();
        foreach (var item in player.Skills)
            startSkillValues.Add(item.Key, item.Value.Value);

        DisableButtons();
        ResetScreen();
    }

    private void OnDisable()
    {
        ApplyChanges();
    }

    void ResetScreen()
    {
        //set up screen header
        nameText.text = $"Данные персонажа\n{player.characterName}";
        levelText.text = $"Уровень {player.Level}. До следующего уровня:";
        //clear the area
        foreach (Image i in conditionsArea.GetComponentsInChildren<Image>())
            Destroy(i.gameObject);
        foreach (Condition c in player.Conditions)
        {
            GameObject newCondition = Instantiate(conditionPrefab);
            if (c.conditionIcon != null)
                newCondition.GetComponent<Image>().sprite = c.conditionIcon;
            newCondition.transform.parent = conditionsArea;
        }

        //set up stat area
        statPointsLeft.text = "Характеристики";
        if (player.statPointsToSpend > 0)
            statPointsLeft.text += $"\nОчков осталось: {player.statPointsToSpend}";
        foreach (var item in statValueFields)
        {
            item.Value.text = player.Stats[item.Key].Value.ToString();
        }
        foreach(var item in statModFields)
        {
            item.Value.text = player.Stats[item.Key].Mod.ToString();
        }
        foreach (var item in statButtons)
        {
            UpdateStatButtons(item.Key, item.Value[0], item.Value[1]);
        }

        //set up skill area
        skillPointsLeft.text = "Навыки";
        if (player.skillPointsToSpend > 0)
            skillPointsLeft.text += $"\nОчков осталось: {player.skillPointsToSpend}";
        foreach (var item in skillValueFields)
        {
            item.Value.text = player.Skills[item.Key].Value.ToString();
        }
        foreach (var item in skillButtons)
        {
            UpdateSkillButtons(item.Key, item.Value[0], item.Value[1]);
        }

        //set up feat area
        featPointsLeft.text = "Черты";
        if (player.featPointsToSpend > 0)
            featPointsLeft.text += $"\nОчков осталось: {player.featPointsToSpend}";
        //clear area before adding buttons
        foreach (Button b in featArea.GetComponentsInChildren<Button>())
        {
            Destroy(b.gameObject);
        }
        foreach (var feat in player.Feats)
        {
            GameObject newFeat = Instantiate(featPrefab);
            newFeat.GetComponentInChildren<Text>().text = feat.fullName;
            newFeat.GetComponentInChildren<Button>().interactable = false;
            newFeat.transform.SetParent(featArea);
        }
        if (player.featPointsToSpend > 0)
        {
            foreach (Feat f in availibleFeats)
            {
                if (f.isRepeatable || !player.Feats.Exists(x => x.fullName == f.fullName))
                {
                    GameObject newFeat = Instantiate(featPrefab);
                    newFeat.GetComponentInChildren<Text>().text = f.fullName;
                    Feat temp = f;
                    newFeat.GetComponentInChildren<Button>().onClick.AddListener(() => AddFeat(temp));
                    newFeat.transform.SetParent(featArea);
                }
            }
        }

        //set up diseases area
        SetDiseaseField(bloodPoisoningField, "Заражение крови", "Blood poisoning");
        SetDiseaseField(radiationSicknessField, "Лучевая болезнь", "Radiation sickness");
        SetDiseaseField(foodPoisoningField, "Пищевое отравление", "Food poisoning");
        SetDiseaseField(commonColdField, "Простуда", "Common cold");

        //set up reputation area
        SetReputationField(banditsField, NPCGroupType.Bandits, "Бандиты");
        SetReputationField(dutyField, NPCGroupType.Duty, "Долг");
        SetReputationField(freedomField, NPCGroupType.Freedom, "Свобода");
        SetReputationField(lonersField, NPCGroupType.Loners, "Одиночки");
        SetReputationField(mercenariesField, NPCGroupType.Mercenaries, "Наемники");
        SetReputationField(militaryField, NPCGroupType.Military, "Военные");
        SetReputationField(monolithField, NPCGroupType.Monolith, "Монолит");
        SetReputationField(scientistsField, NPCGroupType.Scientists, "Ученые");

        //set up other parameters
        SetMiscParameters();
    }

    void SetMiscParameters()
    {
        ACValue.text = player.ArmorClass.ToString();
        HPValue.text = $"{player.CurrentHealth}/{player.MaxHealth}";
        RPValue.text = $"{player.CurrentRadiation}/{player.MaxRadiation}";
        APValue.text = $"{player.CurrentActionPoints}/{player.MaxActionPoints}";
        fatigueValue.text = $"{player.currentTravelDistance}/{player.maxTravelDistance}";
        hungerValue.text = $"{player.CurrentHunger}/{player.MaxHunger}";
        thirstValue.text = $"{player.CurrentThirst}/{player.MaxThirst}";
        speedValue.text = player.MoveSpeed.ToString();
        initiativeValue.text = player.InitiativeMod.ToString();
        resistanceValue.text = player.NaturalResistance.ToString();
        fortitudeValue.text = player.SavingThrows[SavingThrowType.Fortitude].Value.ToString();
        willValue.text = player.SavingThrows[SavingThrowType.Will].Value.ToString(); ;
        reflexValue.text = player.SavingThrows[SavingThrowType.Reflex].Value.ToString(); ;
        vigilanceValue.text = player.SavingThrows[SavingThrowType.Vigilance].Value.ToString(); ;
        weightValue.text = player.MaxWeight.ToString();
    }

    void SetDiseaseField(Text field, string displayName, string dictionaryName)
    {
        field.text = $"{displayName}: ";
        if (player.Diseases[dictionaryName].stage == 0)
        {
            field.text += "не болен";
        }
        else
        {
            field.text += $"{player.Diseases[dictionaryName].stage} стадия";
        }
    }

    void SetReputationField(Text field, NPCGroupType group, string displayName)
    {
        field.text = $"{displayName}: {player.Reputations[group].Value}";
    }

    void ApplyChanges()
    {

    }

    public void IncreaseStat(string statName)
    {
        player.Stats[Stat.GetTypeFromString(statName)].ChangeValue(1);
        player.statPointsToSpend--;

        ResetScreen();
    }

    public void DecreaseStat(string statName)
    {
        player.Stats[Stat.GetTypeFromString(statName)].ChangeValue(-1);
        player.statPointsToSpend++;

        ResetScreen();
    }

    public void IncreaseSkill(string skillName)
    {
        player.Skills[Skill.GetTypeFromString(skillName)].Increase();
        player.skillPointsToSpend--;

        ResetScreen();
    }

    public void DecreaseSkill(string skillName)
    {
        player.Skills[Skill.GetTypeFromString(skillName)].Decrease();
        player.skillPointsToSpend++;

        ResetScreen();
    }

    public void AddFeat(Feat feat)
    {
        player.AddFeat(feat);
        player.featPointsToSpend--;

        ResetScreen();
    }

    public void RemoveFeat(Feat feat)
    {
        player.RemoveFeat(feat);
        player.featPointsToSpend++;

        ResetScreen();
    }

    void UpdateStatButtons(StatType stat, Button plusButton, Button minusButton)
    {
        if ((player.Stats[stat].Value == Stat.MAX_PLAYER_VALUE) || (player.statPointsToSpend == 0))
            plusButton.interactable = false;
        else
            plusButton.interactable = true;

        if (player.Stats[stat].Value == startStatValues[stat])
            minusButton.interactable = false;
        else
            minusButton.interactable = true;
    }

    void UpdateSkillButtons(SkillType skill, Button plusButton, Button minusButton)
    {
        if ((player.Skills[skill].Value == Skill.MAX_PLAYER_VALUE) || (player.skillPointsToSpend == 0))
            plusButton.interactable = false;
        else
            plusButton.interactable = true;

        if (player.Skills[skill].Value == startSkillValues[skill])
            minusButton.interactable = false;
        else
            minusButton.interactable = true;
    }

    void DisableButtons()
    {
        if (player.statPointsToSpend > 0)
        {
            foreach (var item in statButtons)
            {
                item.Value[0].gameObject.SetActive(true);
                item.Value[1].gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var item in statButtons)
            {
                item.Value[0].gameObject.SetActive(false);
                item.Value[1].gameObject.SetActive(false);
            }
        }


        if (player.skillPointsToSpend > 0)
        {
            foreach (var item in skillButtons)
            {
                item.Value[0].gameObject.SetActive(true);
                item.Value[1].gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var item in skillButtons)
            {
                item.Value[0].gameObject.SetActive(false);
                item.Value[1].gameObject.SetActive(false);
            }
        }
    }
}
