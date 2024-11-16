using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Rock,
    Log,
    Puddle,
}

public class Obstacle : MonoBehaviour
{
    // fields
    public ObstacleType Type;
    public bool WasHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
