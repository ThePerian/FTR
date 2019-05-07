using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class RandomEventController : MonoBehaviour
{
    //public Canvas startCanvas;
    public Canvas eventCanvas;
    public GameObject eventWindow;
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
        //startCanvas.gameObject.SetActive(false);
        eventCanvas.gameObject.SetActive(true);

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
            foreach (var button in eventCanvas.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }

            npcText.text = data.comments[data.commentIndex];
        }
        else
        {
            for (int i = 0; i < data.comments.Length; i++)
            {
                // get position of each button for player's choice
                // buttons are set in the middle of the canvas
                // in a vertical row with gaps between them
                RectTransform parentTransform = eventWindow.GetComponent<RectTransform>();
                float x = parentTransform.rect.center.x;
                float gap = ((parentTransform.rect.height / 2f)
                    - (playerChoiceButton.GetComponent<RectTransform>().rect.height * data.comments.Length))
                    / (data.comments.Length + 1);
                float y = parentTransform.rect.center.y
                    - (playerChoiceButton.GetComponent<RectTransform>().rect.height * i)
                    - (gap * (i + 1));
                Vector2 buttonPosition = new Vector2(x, y);
                GameObject button = Instantiate(playerChoiceButton);
                button.transform.SetParent(parentTransform, true);
                button.transform.localPosition = buttonPosition;
                Debug.Log("Choice: " + data.comments[i]);
                button.GetComponentInChildren<Text>().text = data.comments[i];
                Debug.Log("Option: " + i);
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

        eventCanvas.gameObject.SetActive(false);
        //startCanvas.gameObject.SetActive(true);
        npcText.text = "";
        foreach (var button in eventCanvas.GetComponentsInChildren<Button>())
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
