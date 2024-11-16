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
    [SerializeField] public ObstacleType Type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
