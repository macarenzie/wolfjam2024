using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    // === FIELDS ===
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private float redCooldown = 1;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (redCooldown > 0)
        {
            redCooldown -= Time.deltaTime;
        }
        else if (redCooldown <= 0)
        {
            rend.color = Color.white;
            redCooldown = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
        // testing
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                playerManager.ResourceChange(true, -10);
                redCooldown = 1;
                rend.color = Color.red;
                Debug.Log("you hit an OBSTACLE!");
                break;

            case "Enemy":
                playerManager.ResourceChange(true, -15);
                redCooldown = 1;
                rend.color = Color.red;
                Debug.Log("you hit an ENEMY!");
                break;

            default:
                break;
        }
    }
}