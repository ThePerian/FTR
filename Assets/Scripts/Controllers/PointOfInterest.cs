using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointOfInterest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite unexplored;
    public Sprite explored;
    public Sprite highlighted;
    public Sprite clicked;
    public bool isExplored = false;
    public GameObject junctionPrefab;
    public List<Vector2> neighbourPoints = new List<Vector2>();
    public GameObject playerToken;

    Player player;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
        player = Player.Instance;
    }
    
    void Update()
    {
    }

    void DrawJunctionLines()
    {
        foreach (Vector2 point in neighbourPoints)
        {
            Vector2 playerPosition = playerToken.transform.position;
            float travelDistance = Vector2.Distance(transform.position, point);

            if (travelDistance > player.maxTravelDistance)
                continue;

            GameObject line = Instantiate(junctionPrefab);
            line.transform.parent = gameObject.transform;
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, point);

            if (((Vector2)transform.position != playerPosition) && (point != playerPosition))
            {
                lineRenderer.startColor = Color.grey;
                lineRenderer.endColor = Color.grey;
            }
            else if (travelDistance > player.currentTravelDistance)
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
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
        Vector2 playerPosition = playerToken.transform.position;
        float travelDistance = Vector2.Distance(transform.position, playerPosition);
        bool isNeighbour = false;

        foreach (Vector2 point in neighbourPoints)
        {
            if (point == playerPosition)
            {
                isNeighbour = true;
                break;
            }
        }

        if ((travelDistance <= player.currentTravelDistance)
            && isNeighbour)
        {
            isExplored = true;
            playerToken.GetComponent<PlayerToken>().destination = transform.position;
            player.currentTravelDistance -= travelDistance;
        }
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
