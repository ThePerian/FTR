using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class RandomEventController : MonoBehaviour
{
    public GameObject eventPanel;
    public GameObject playerChoiceArea;
    public GameObject playerChoiceButton;
    public Text npcText;
    public KeyCode continueButton;

    VIDE_Assign dialogueComponent;
    //here be the dialogs to choose from
    //TODO: add procedural choice of dialogs
    string[] dialogues = { "Charlie", "TriggeredDude"};

    void Start()
    {
        dialogueComponent = GetComponent<VIDE_Assign>();
    }

    void Update()
    {
        if (VD.isActive && !VD.nodeData.isPlayer && Input.GetKeyDown(continueButton))
        {
            VD.Next();
        }
    }

    public void StartEvent()
    {
        eventPanel.SetActive(true);

        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += EndDialogue;
        int randomDialog = GetRandomDialog();
        VD.BeginDialogue(dialogues[randomDialog]);

        Time.timeScale = 0;
    }

    int GetRandomDialog()
    {
        return Random.Range(0, dialogues.Length);
    }

    void UpdateUI(VD.NodeData data)
    {
        if (!data.isPlayer)
        {
            foreach (var button in playerChoiceArea.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }

            npcText.text = data.comments[data.commentIndex];
        }
        else
        {
            for (int i = 0; i < data.comments.Length; i++)
            {
                GameObject button = Instantiate(playerChoiceButton);
                button.transform.SetParent(playerChoiceArea.GetComponent<RectTransform>(), true);
                button.GetComponentInChildren<Text>().text = data.comments[i];
                int choice = i;
                button.GetComponent<Button>().onClick.AddListener(
                    () => SelectChoiceAndGoToNext(choice));
            }
        }
    }

    void EndDialogue(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= EndDialogue;
        VD.EndDialogue();

        eventPanel.gameObject.SetActive(false);
        npcText.text = "";
        foreach (var button in playerChoiceArea.GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }

        Time.timeScale = 1;
    }

    public void SelectChoiceAndGoToNext(int playerChoice)
    {
        VD.nodeData.commentIndex = playerChoice;
        VD.Next();
    }
}
