using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Spirit,
    Cryptid,
}

public class Enemy : MonoBehaviour
{
    //fields
    [SerializeField] private EnemyType Type;
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
