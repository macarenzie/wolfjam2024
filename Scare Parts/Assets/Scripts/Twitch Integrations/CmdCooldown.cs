using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Name: Cmd Coldown
/// Purpose: Set twitch command cooldowns to prevent event spamming and overloading the api
/// Author(s): Katie Hellmann, GucioDevs Unity3D Quick Tips: Cooldown Timers, Gator Flack
/// </summary>

public class CmdCooldown : MonoBehaviour
{
    [SerializeField] private int boostThreshold = 5; // Number of inputs required to trigger boost
    private int boostCount = 0; // Tracks the number of `!boost` inputs

    [SerializeField] private float boostCooldownTime = 10f; // Cooldown period after boost is triggered
    private float boostTimer = 0f;
    private bool canUseBoost = true;

    private void Update()
    {
        // Handle boost cooldown timer
        if (!canUseBoost)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0)
            {
                canUseBoost = true;
                boostCount = 0; // Reset boost count after cooldown
            }
        }
    }

    /// Called when a chat message is received.
    public void OnChatMessage(string chatter, string message)
    {
        // Handle boost command
        if (message.ToLower().Contains("!boost") && canUseBoost)
        {
            boostCount++;

            // Check if the boost threshold has been reached
            if (boostCount >= boostThreshold)
            {
                TriggerBoost();
                canUseBoost = false;
                boostTimer = boostCooldownTime;
            }
        }
        else if (!canUseBoost)
        {
            Debug.Log("Boost on cooldown.");
        }
    }

    /// <summary>
    /// Triggers the boost effect when the threshold is reached.
    /// </summary>
    private void TriggerBoost()
    {
        Debug.Log("BOOST ACTIVATED!");
        // Implement boost effect logic here
    }

    /// <summary>
    /// Checks if boost is available.
    /// </summary>
    public bool CanBoost()
    {
        return canUseBoost;
    }

    /// <summary>
    /// Returns the current count of boost inputs.
    /// </summary>
    public int GetBoostCount()
    {
        return boostCount;
    }

    /// <summary>
    /// Returns the required threshold to trigger boost.
    /// </summary>
    public int GetBoostThreshold()
    {
        return boostThreshold;
    }
}