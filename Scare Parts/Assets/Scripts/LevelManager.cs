using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // === FIELDS ===

    [SerializeField] private PlayerManager player;

    private float boostTotal = 100;


    // === PROPERTIES ===

    public float SpeedScaleFactor
    {
        get { return speedScaleFactor; }
        set { speedScaleFactor = value; }
    }
    [SerializeField] private float speedScaleFactor = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if health is 0, end game

        // if boost
        if (player.BoostCurrent > 100 && player.BoostIncrease)
        {
            speedScaleFactor += 1;
            player.BoostIncrease = false;
            AudioManager.instance.PlaySFX("TurboBoost_Activate");
        }
        else if (player.BoostCurrent < 0 && !player.BoostIncrease)
        {
            speedScaleFactor -= 1;
            player.BoostIncrease = true;
        }
    }
}
