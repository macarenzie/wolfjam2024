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
    // === PROPERTIES ===

    public EnemyType Type
    {
        get { return _enemyType; }
        set { _enemyType = value; }
    }
    [SerializeField] private EnemyType _enemyType;

    public bool IsCaught
    {
        get { return _isCaught; }
        set { _isCaught = value; }
    }
    private bool _isCaught = false;
    

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
