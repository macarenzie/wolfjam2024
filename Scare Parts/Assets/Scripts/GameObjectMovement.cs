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
    // fields
    [SerializeField] private float movementFactor = 0.5f;

    // movement fields
    [SerializeField] private Transform transform;

    /// <summary>
    /// Move the object based on a specified movement and time
    /// </summary>
    void Update()
    {
        Vector3 newPos = transform.localPosition;
        newPos.y -= Time.deltaTime * movementFactor;

        transform.localPosition = newPos;
    }

}
