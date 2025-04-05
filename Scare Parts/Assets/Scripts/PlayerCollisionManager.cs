using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    // === FIELDS ===
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private float redCooldown = 1;
    [SerializeField] private LevelLoader spawner;
    private List<GameObject> enemyList;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        enemyList = spawner.Objects;
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
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                playerManager.ResourceChange(true, -10);
                redCooldown = 1;
                rend.color = Color.red;
                AudioManager.instance.PlaySFX("Damage");
                Debug.Log("you hit an OBSTACLE!");
                break;

            case "Enemy":
                playerManager.ResourceChange(true, -15);
                redCooldown = 1;
                rend.color = Color.red;
                AudioManager.instance.PlaySFX("Damage");
                Debug.Log("you hit an ENEMY!");

                // delete enemy
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i] == collision.gameObject)
                    {
                        Destroy(enemyList[i]);
                        enemyList.RemoveAt(i);
                        i--;
                    }
                }
                break;

            default:
                break;
        }
    }
}