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
    [SerializeField] public EnemyType Type;
    public bool IsCaught = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
