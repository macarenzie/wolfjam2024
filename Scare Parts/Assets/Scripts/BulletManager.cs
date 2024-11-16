using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletManager : MonoBehaviour
{
    // fields
    [SerializeField] private GameObject player;
    public List<GameObject> enemyBullets;
    public List<GameObject> playerBullets;
    [SerializeField] private GameObject bullet;
    public float speed = 6.0f;
    private InputActionReference fireAction;
    [SerializeField] private Camera cam;
    private float camWidth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
