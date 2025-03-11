using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DylanCollisions : MonoBehaviour
{
    // === FIELDS ===
    [SerializeField] private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.gameObject.tag) {
            case "Obstacle":
                playerManager.Health -= 10;
                Debug.Log("you hit an OBSTACLE!");
                break;

            case "Enemy":
                playerManager.Health -= 15;
                Debug.Log("you hit an ENEMY!");
                break;

            default:
                break;
        }
    }
}
