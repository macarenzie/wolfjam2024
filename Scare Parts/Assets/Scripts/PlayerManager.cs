using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Name: Player Manager
/// Purpose: Handles everything related to the player
/// Author(s): Leah Torregiano, McKenzie Lam
/// </summary>
public class PlayerManager : MonoBehaviour
{
    // === FIELDS ===

    [SerializeField] private float speed = 5f; // set in the inspector
    [SerializeField] private float redCooldown = 1;
    [SerializeField] private CollisionManager collison;

    private Vector3 objectPosition; // initialized in Start() via transform
    private int roadWidth = 4; // TODO: confirm correct width when we have the sprite
    private SpriteRenderer rend;


    // === PROPERTIES ===

    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }
    private float _health = 100;

    public float Gas
    {
        get { return _gas; }
        set { _gas = value; }
    }
    private float _gas = 100;

    public bool IsSlipping
    {
        get { return _isSlipping; }
        set { _isSlipping = value; }
    }
    private bool _isSlipping = false;

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value.normalized; }
    }
    private Vector3 _direction;


    // === METHODS ===
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();

        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (redCooldown > 0)
        {
            redCooldown -= Time.deltaTime;
        }
        if (redCooldown < 0)
        {
            redCooldown = 0;
        }

        if (collison.IsHit)
        {
            redCooldown = 1;
            rend.color = Color.red;

        }
        else if (redCooldown <= 0)
        {
            rend.color = Color.white;
        }

        // --- Movement ---
        if (IsSlipping)
        {
            return;
        }

        // Velocity is direction * speed * deltaTime 
        Vector3 velocity = Direction * speed * Time.deltaTime;

        // New position is current position + velocity
        objectPosition += velocity;

        // Draw the vehicle at that new position
        transform.position = objectPosition;

        // edge of road logic
        if (objectPosition.x > roadWidth)
        {
            objectPosition.x = roadWidth;
        }
        if (objectPosition.x < -roadWidth)
        {
            objectPosition.x = -roadWidth;
        }

        // TODO: 
        // if (objectPosition.x > smallerRoadWidth)
        // {
        //     speedScale -= enoughToSlowDown;
        // }

        // --- Resources ---
        // decrease gas over time
        Gas -= Time.deltaTime * 1;
        if(Gas < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
        //Debug.Log(Gas);
    }

    /// <summary>
    /// handles player movement input
    /// </summary>
    /// <param name="context">external input manager</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        // translates the latest input to vehicle direction
        Direction = new Vector2(context.ReadValue<Vector2>().x, 0);
    }

    /// <summary>
    /// updates given resource values
    /// </summary>
    /// <param name="isHealth">determines what resource is affected</param>
    /// <param name="num">amount to change resource by</param>
    public void ResourceChange(bool isHealth, float num)
    {
        if(isHealth)
        {
            Health += num;
            if(Health > 100)
            {
                Health = 100;
            }
        }
        else
        {
            Gas += num;
        }

        if (Gas < 0 || Health < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}
