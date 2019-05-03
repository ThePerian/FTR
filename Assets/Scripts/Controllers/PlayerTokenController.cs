using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTokenController : MonoBehaviour
{
    public Vector2 destination;

    float tokenSpeed = 2;
    Player player;

    void Start()
    {
        destination = transform.position;
        player = Player.Instance;
    }
    
    void Update()
    {
        if ((Vector2)transform.position != destination)
        {
            Vector2 target =
                Vector2.MoveTowards(transform.position, destination, tokenSpeed * Time.deltaTime);
            transform.position = target;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            player.currentTravelDistance = player.maxTravelDistance;
    }
}
