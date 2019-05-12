using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSetup : MonoBehaviour
{
    [Header("Remaining points")]
    public Text pointsField;
    [Space]
    [Header("Stat values")]
    public Text socialField;
    public Text toughnessField;
    public Text agilityField;
    public Text lookoutField;
    public Text knowledgeField;
    public Text enduranceField;
    public Text reactionField;
    [Space]
    [Header("Stat changing buttons")]
    public Button minusSocial;
    public Button plusSocial;
    public Button minusToughness;
    public Button plusToughness;
    public Button minusAgility;
    public Button plusAgility;
    public Button minusLookout;
    public Button plusLookout;
    public Button minusKnowledge;
    public Button plusKnowledge;
    public Button minusEndurance;
    public Button plusEndurance;
    public Button minusReaction;
    public Button plusReaction;

    Player player;
    int pointsLeft = 0;
    Dictionary<StatType, Button[]> allButtons;

    const int MIN_START_VALUE = 7;
    const int MAX_START_VALUE = 15;

    void Start()
    {
        player = Player.Instance;
        allButtons = new Dictionary<StatType, Button[]>()
        {
            { StatType.Social, new Button[] { plusSocial, minusSocial } },
            { StatType.Toughness, new Button[] { plusToughness, minusToughness } },
            { StatType.Agility, new Button[] { plusAgility, minusAgility } },
            { StatType.Lookout, new Button[] { plusLookout, minusLookout } },
            { StatType.Knowledge, new Button[] { plusKnowledge, minusKnowledge } },
            { StatType.Endurance, new Button[] { plusEndurance, minusEndurance } },
            { StatType.Reaction, new Button[] { plusReaction, minusReaction } }
        };

        UpdateStatValues();
    }

    private void Update()
    {
        foreach (var item in allButtons)
        {
            DisableButtonsIfBorderValue(item.Key, item.Value[0], item.Value[1]);
        }
    }

    public void ChangeSocialValue(int amount)
    {
        ChangeStatValue(StatType.Social, amount);
    }

    public void ChangeToughnessValue(int amount)
    {
        ChangeStatValue(StatType.Toughness, amount);
    }

    public void ChangeAgilityValue(int amount)
    {
        ChangeStatValue(StatType.Agility, amount);
    }

    public void ChangeLookoutValue(int amount)
    {
        ChangeStatValue(StatType.Lookout, amount);
    }

    public void ChangeKnowledgeValue(int amount)
    {
        ChangeStatValue(StatType.Knowledge, amount);
    }

    public void ChangeEnduranceValue(int amount)
    {
        ChangeStatValue(StatType.Endurance, amount);
    }

    public void ChangeReactionValue(int amount)
    {
        ChangeStatValue(StatType.Reaction, amount);
    }

    void ChangeStatValue(StatType type, int amount)
    {
        int newValue = player.Stats[type].Value + amount;
        if ((newValue < MIN_START_VALUE) || (newValue > MAX_START_VALUE)) return;

        player.ChangeStatValue(type, amount);
        pointsLeft -= amount;
        UpdateStatValues();
    }

    void DisableButtonsIfBorderValue(StatType type, Button plusButton, Button minusButton)
    {
        if ((player.Stats[type].Value == MAX_START_VALUE) || (pointsLeft == 0))
            plusButton.interactable = false;
        else
            plusButton.interactable = true;

        if (player.Stats[type].Value == MIN_START_VALUE)
            minusButton.interactable = false;
        else
            minusButton.interactable = true;
    }

    void UpdateStatValues()
    {
        pointsField.text = "ОСТАЛОСЬ ОЧКОВ: " + pointsLeft.ToString();
        socialField.text = player.Stats[StatType.Social].Value.ToString();
        toughnessField.text = player.Stats[StatType.Toughness].Value.ToString();
        agilityField.text = player.Stats[StatType.Agility].Value.ToString();
        lookoutField.text = player.Stats[StatType.Lookout].Value.ToString();
        knowledgeField.text = player.Stats[StatType.Knowledge].Value.ToString();
        enduranceField.text = player.Stats[StatType.Endurance].Value.ToString();
        reactionField.text = player.Stats[StatType.Reaction].Value.ToString();
    }
}
