using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Spirit,
    Cryptid,
}

/// <summary>
/// Name: Enemy
/// Purpose: Handles everything related to enemies
/// Author(s): Leah Torregiano, McKenzie Lam
/// </summary>
public class Enemy : MonoBehaviour
{
    //fields
    [SerializeField] private EnemyType Type;
    public bool IsCaught = false;

    // movement fields
    [SerializeField] private Transform transform;
    [SerializeField] private float downSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // adjust the position of the enemy
        Vector3 newPos = transform.localPosition;
        newPos.y -= downSpeed * Time.deltaTime;
        transform.localPosition = newPos;
    }
}
