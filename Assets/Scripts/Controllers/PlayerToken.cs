using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToken : MonoBehaviour
{
    public Vector2 destination;
    public delegate void DestinationReached();
    public event DestinationReached OnReachedDestination;

    float _tokenSpeed = 2;
    Player _player;
    bool _isMoving = false;

    void Start()
    {
        destination = transform.position;
        _player = Player.Instance;
    }
    
    void Update()
    {
        if ((Vector2)transform.position != destination)
        {
            Vector2 target =
                Vector2.MoveTowards(transform.position, destination, _tokenSpeed * Time.deltaTime);
            transform.position = target;
            _isMoving = true;
        }
        else if (_isMoving)
        {
            _isMoving = false;
            OnReachedDestination();
        }

        if (Input.GetKeyDown(KeyCode.F))
            _player.currentTravelDistance = _player.maxTravelDistance;
    }
}
