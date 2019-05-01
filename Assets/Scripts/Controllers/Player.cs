using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : Creature
{
    public Vector2 destination;
    public float maxTravelDistance = 10;
    public float currentTravelDistance = 10;
    List<Skill> skills = new List<Skill>();
    List<Reputation> reputation = new List<Reputation>();
    List<Feat> feats = new List<Feat>();
    int currentExp;
    int expToLevelUp;
    int level;
    float tokenSpeed = 2;

    void Start()
    {
        destination = transform.position;
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
            currentTravelDistance = maxTravelDistance;
    }
}
