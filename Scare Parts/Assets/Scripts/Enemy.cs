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
    // === FIELDS ===

    [SerializeField] private EnemyType _enemyType;
    private bool _isCaught = false;


    // === PROPERTIES ===

    public EnemyType Type
    {
        get { return _enemyType; }
        set { _enemyType = value; }
    }

    public bool IsCaught
    {
        get { return _isCaught; }
        set { _isCaught = value; }
    }


    // === METHODS ===

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
