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
    // fields
    public float Health = 100;
    public float Gas = 100;
    public bool IsSlipping = false;

    private Vector3 objectPosition;         // Initialized in Start() via transform
    [SerializeField] private float speed = 5f;      // Set in the inspector
    private int roadWidth = 4;
    [SerializeField] private float redCooldown = 1;

    public SpriteRenderer rend;
    [SerializeField] private CollisionManager collison;

    // Direction object is facing, must be normalized
    private Vector3 direction;
    internal Vector3 Direction
    {
        get { return direction; } // Provide it if needed
        set // Only set a normalized copy!
        {
            direction = value.normalized;
        }
    }

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

        if (collison.isHit)
        {
            redCooldown = 1;
            rend.color = Color.red;

        }
        else if (redCooldown <= 0)
        {
            rend.color = Color.white;
        }

        #region MOVEMENT
        if (IsSlipping)
        {
            return;
        }

        // Velocity is direction * speed * deltaTime 
        Vector3 velocity = direction * speed * Time.deltaTime;

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
        #endregion

        #region RESOURCES
        // decrease gas over time
        Gas -= Time.deltaTime * 1;
        if(Gas < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
        //Debug.Log(Gas);

        #endregion
    }

    // The method that gets called to handle any player movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        // Get latest value for input from Input System for direction
        Direction = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Change a specific resource by a certain number
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
