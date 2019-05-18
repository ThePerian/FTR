using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatSetup : MonoBehaviour
{
    public Button featButtonPrefab;
    public Button transferButton;
    public Transform availibleFeatsArea;
    public Transform purchasedFeatsArea;
    public Text featTitleText;
    public Text featDescriptionText;

    Player player;
    List<Feat> availibleFeats;
    Feat selectedFeat;

    void Start()
    {
        player = Player.Instance;

        //TODO: add proper initialization
        availibleFeats = new List<Feat>()
        {
            new Feat(){fullName = "feat1", description = "desc1"},
            new Feat(){fullName = "feat2", description = "desc2"},
            new Feat(){fullName = "feat3", description = "desc3"}
        };

        ResetScreen();
    }

    public void ShowDetails(Feat feat, bool buying)
    {
        featTitleText.text = feat.fullName;
        featDescriptionText.text = feat.description;
        selectedFeat = feat;
        transferButton.GetComponentInChildren<Text>().text = buying ? "Взять черту" : "Отказаться от черты";
        transferButton.onClick.RemoveAllListeners();
        transferButton.onClick.AddListener(Transfer);
    }

    public void Transfer()
    {
        if (player.Feats.Contains(selectedFeat))
        {
            availibleFeats.Add(selectedFeat);
            player.RemoveFeat(selectedFeat);
            player.featPointsToSpend++;
        }
        else if (availibleFeats.Contains(selectedFeat))
        {
            player.AddFeat(selectedFeat);
            availibleFeats.Remove(selectedFeat);
            player.featPointsToSpend--;
        }
        ResetScreen();
    }

    void ResetScreen()
    {
        foreach (var child in availibleFeatsArea.GetComponentsInChildren<Button>())
            Destroy(child.gameObject);
        foreach (var child in purchasedFeatsArea.GetComponentsInChildren<Button>())
            Destroy(child.gameObject);

        foreach (var feat in availibleFeats)
        {
            Button featButton = Instantiate(featButtonPrefab, availibleFeatsArea);
            featButton.GetComponentInChildren<Text>().text = feat.fullName;
            Feat featToTransfer = feat;
            featButton.onClick.AddListener(() => ShowDetails(featToTransfer, true));
            if (player.featPointsToSpend <= 0)
                featButton.interactable = false;
            else
                featButton.interactable = true;
        }
        foreach (var feat in player.Feats)
        {
            Button featButton = Instantiate(featButtonPrefab, purchasedFeatsArea);
            featButton.GetComponentInChildren<Text>().text = feat.fullName;
            Feat featToTransfer = feat;
            featButton.onClick.AddListener(() => ShowDetails(featToTransfer, false));
        }

        featTitleText.text = "";
        featDescriptionText.text = "";
        transferButton.onClick.RemoveAllListeners();
    }
}
