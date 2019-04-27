using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public Sprite unexplored;
    public Sprite explored;
    public Sprite highlighted;
    public Sprite clicked;
    public bool isExplored = false;
    public List<Vector2> neighbourPoints = new List<Vector2>();

    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
        //button.GetComponent<Image>().sprite = unexplored;
    }
    
    void Update()
    {
        
    }
    /*
    private void OnMouseEnter()
    {
        button.GetComponent<Image>().sprite = highlighted;
    }

    private void OnMouseExit()
    {
        button.GetComponent<Image>().sprite = isExplored ? explored : unexplored;
    }

    private void OnMouseDown()
    {
        button.GetComponent<Image>().sprite = clicked;
    }
    */
    void ButtonClicked()
    {
        isExplored = true;
    }
    
}
