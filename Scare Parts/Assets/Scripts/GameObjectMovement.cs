using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Object Movement
/// Purpose: Handles the vertical movement of a game object
/// Author(s): McKenzie Lam
/// </summary>
public class GameObjectMovement : MonoBehaviour
{
    // === FIELDS ===

    [SerializeField] private float movementFactor = 0.5f;
    [SerializeField] private Transform transform;

    private LevelManager lm;


    // Start is called before the first frame update
    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
    }

    /// <summary>
    /// Move the object based on a specified movement and time
    /// </summary>
    void Update()
    {
        Vector3 newPos = transform.localPosition;
        newPos.y -= Time.deltaTime * movementFactor * lm.SpeedScaleFactor;

        transform.localPosition = newPos;
    }

}
