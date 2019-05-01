using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite unexplored;
    public Sprite explored;
    public Sprite highlighted;
    public Sprite clicked;
    public bool isExplored = false;
    public GameObject junctionPrefab;
    public List<Vector2> neighbourPoints = new List<Vector2>();
    public float maxJunctionDistance;

    Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }
    
    void Update()
    {
        
    }


    void DrawJunctionLines()
    {
        foreach (Vector2 point in neighbourPoints)
        {
            if (Vector2.Distance(gameObject.transform.position, point) > maxJunctionDistance)
                continue;
            GameObject line = Instantiate(junctionPrefab);
            line.transform.parent = gameObject.transform;
            line.GetComponent<LineRenderer>().SetPosition(0, gameObject.transform.position);
            line.GetComponent<LineRenderer>().SetPosition(1, point);
        }
    }

    void EraseJunctionLines()
    {
        foreach(Component obj in gameObject.GetComponentsInChildren<LineRenderer>())
        {
            Destroy(obj.gameObject);
        }
    }
    
    void ButtonClicked()
    {
        isExplored = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DrawJunctionLines();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EraseJunctionLines();
    }
}
