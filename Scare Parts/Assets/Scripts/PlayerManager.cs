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
    [SerializeField] private LevelManager lm;
    [SerializeField] private float speed = 5f; // set in the inspector
    [SerializeField] private float redCooldown = 1;
    //[SerializeField] private OldCollisionManager collison;

    private int roadWidth = 8;
    private SpriteRenderer rend;
    private InputAction accelAction;
    private InputAction brakeAction;


    // === PROPERTIES ===

    public float BoostCurrent
    {
        get { return boostCurrent; }
        set { boostCurrent = value; }
    }
    public float boostCurrent = 0;

    public bool BoostIncrease
    {
        get { return boostIncrease; }
        set { boostIncrease = value; }
    }
    private bool boostIncrease = true;

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


    public Vector3 ObjectPosition
    {
        get { return objectPosition; }
        set { objectPosition = value; }
    }
    private Vector3 objectPosition; // initialized in Start() via transform


    // === METHODS ===
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();

        ObjectPosition = transform.position;
        accelAction = GetComponent<PlayerInput>().actions["Accel"];
        brakeAction = GetComponent<PlayerInput>().actions["Brake"];
    }

    // Update is called once per frame
    void Update()
    {
        //if (redCooldown > 0)
        //{
        //    redCooldown -= Time.deltaTime;
        //}
        //if (redCooldown < 0)
        //{
        //    redCooldown = 0;
        //}
        //
        //if (collison.IsHit)
        //{
        //    redCooldown = 1;
        //    rend.color = Color.red;
        //
        //}
        //else if (redCooldown <= 0)
        //{
        //    rend.color = Color.white;
        //}

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

        if (!BoostIncrease)
        {
            boostCurrent-=0.25f;
        }

        if (accelAction.inProgress)
        {
            OnAccel();
        }
        if (brakeAction.inProgress)
        {
            OnBrake();
        }
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
            AudioManager.instance.StopMusic();
        }
    }

    /// <summary>
    /// InputAction.CallbackContext context
    /// Add to the boost meter
    /// </summary>
    public void OnBoost()
    {
        if (boostIncrease)
        {
            boostCurrent++;
        }
    }

    /// <summary>
    /// InputAction.CallbackContext context
    /// Adjust vertical speed, increase
    /// </summary>
    public void OnAccel()
    {
        lm.SpeedScaleFactor += 0.001f;
        if (!boostIncrease)
        {
            if(lm.SpeedScaleFactor > 2.5f)
            {
                lm.SpeedScaleFactor = 2.5f;
            }
        }
        else
        {
            if (lm.SpeedScaleFactor > 1.5f)
            {
                lm.SpeedScaleFactor = 1.5f;
            }
        }
    }

    /// <summary>
    /// InputAction.CallbackContext context
    /// Adjust vertical speed, decrease
    /// </summary>
    public void OnBrake()
    {
        lm.SpeedScaleFactor -= 0.001f;
        if (boostIncrease)
        {
            if (lm.SpeedScaleFactor < 0.5f)
            {
                lm.SpeedScaleFactor = 0.5f;
            }
        }
        else
        {
            if (lm.SpeedScaleFactor < 1.5f)
            {
                lm.SpeedScaleFactor = 1.5f;
            }
        }
    }
}
