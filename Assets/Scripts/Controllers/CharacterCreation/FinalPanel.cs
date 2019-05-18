using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalPanel : MonoBehaviour
{
    public Text nameField;
    public Text armorClassField;
    public Text healthPointsField;
    public Text radiationPointsField;
    public Text actionPointsField;
    public Text savingThrowsField;
    public Text miscField;

    Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    void Update()
    {
        player.characterName = nameField.text;
        armorClassField.text = player.ArmorClass.ToString();
        healthPointsField.text = player.MaxHealth.ToString();
        radiationPointsField.text = player.MaxRadiation.ToString();
        actionPointsField.text = player.MaxActionPoints.ToString();
        savingThrowsField.text =
            $"Стойкость: {player.SavingThrows[SavingThrowType.Fortitude].value}\n\n"
            + $"Сила воли: {player.SavingThrows[SavingThrowType.Will].value}\n\n"
            + $"Рефлекс: {player.SavingThrows[SavingThrowType.Reflex].value}\n\n"
            + $"Бдительность: {player.SavingThrows[SavingThrowType.Vigilance].value}";
        miscField.text =
            $"Скорость передвижения: {player.MoveSpeed}\n\n"
            + $"Модификатор инициативы: {player.InitiativeMod}\n\n"
            + $"Естественная устойчивость: {player.NaturalResistance}";
    }
}
