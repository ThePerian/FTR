using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FinalPanel : MonoBehaviour
{
    public InputField nameInputField;
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
        nameInputField.onValidateInput 
            += delegate (string input, int charIndex, char addedChar) { return ValidateCharacterName(addedChar); };
    }

    void Update()
    {
        player.characterName = nameInputField.text == "" ? 
            nameInputField.placeholder.GetComponent<Text>().text : nameInputField.text;
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

    public char ValidateCharacterName(char c)
    {
        c = Regex.Replace(c.ToString(), @"[^a-za-яA-ZА-Я\'\.\-"" ]", "\0")[0];
        return c;
    }
}
