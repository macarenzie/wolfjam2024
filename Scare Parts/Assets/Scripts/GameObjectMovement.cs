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
    [SerializeField] private float movementFactor;

    /// <summary>
    /// Move the object based on a specified movement and time
    /// </summary>
    /// <param name="delta">time</param>
    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.y -= delta * movementFactor;

        transform.localPosition = newPos;
    }

}
