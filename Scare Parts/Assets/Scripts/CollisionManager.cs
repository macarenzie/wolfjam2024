using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // fields
    private List<GameObject> enemies;
    [SerializeField] private GameObject player;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Camera cam;
    private float camHeight;

    public List<GameObject> Enemies
    {
        get
        {
            return enemies;
        }
        set
        {
            enemies = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        camHeight = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // AABB
        for (int i = 0; i < enemies.Count; i++)
        {
            // Collision occuring
            if (AABBCollision(player, enemies[i]))
            {
                // if the enemy is not caught
                if (!enemies[i].GetComponent<Enemy>().IsCaught)
                {
                    // lower health
                    // Destroy(playerManager.playerLives[playerManager.playerLives.Count - 1]);
                    // playerManager.playerLives.RemoveAt(playerManager.playerLives.Count - 1);

                    // if health is 0, end game

                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                    if (i != 0)
                    {
                        i--;
                    } 
                }
                // if the enemy is caught
                else
                {
                    // add gas/health

                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                    if (i != 0)
                    {
                        i--;
                    }
                }
            }

            //destroy enemy if off bottom of screen
            if (enemies[i].GetComponent<SpriteRenderer>().bounds.max.y < -camHeight)
            {
                Destroy(enemies[i]);
                enemies.RemoveAt(i);
                if (i != 0)
                {
                    i--; 
                }
            }
        }
        
        for (int j = 0; j < bulletManager.bullets.Count; j++)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                // Collision occuring
                if (AABBCollision(bulletManager.bullets[j], enemies[i]))
                {
                    if (bulletManager.Type == GunType.Capture)
                    {
                        // capture enemy
                        enemies[i].GetComponent<Enemy>().IsCaught = true;
                    }
                    else if (bulletManager.Type == GunType.Kill)
                    {
                        // kill enemy
                        Destroy(enemies[i]);
                        enemies.RemoveAt(i);
                        if (i != 0)
                        {
                            i--; 
                        }
                    }

                    // destroy the bullet that collided
                    Destroy(bulletManager.bullets[j]);
                    bulletManager.bullets.RemoveAt(j);
                    if (j != 0)
                    {
                        j--;
                    }
                }
            }

            //destroy bullet if off top of screen
            if (bulletManager.bullets[j].GetComponent<SpriteRenderer>().bounds.min.y > camHeight)
            {
                Destroy(bulletManager.bullets[j]);
                bulletManager.bullets.RemoveAt(j);
                j--;
            }

        }
    }

    bool AABBCollision(GameObject move, GameObject stop)
    {
        bool yIntersect = false;
        bool xIntersect = false;

        if ((move.GetComponent<SpriteRenderer>().bounds.max.y > stop.GetComponent<SpriteRenderer>().bounds.min.y &&
            move.GetComponent<SpriteRenderer>().bounds.max.y < stop.GetComponent<SpriteRenderer>().bounds.max.y) ||
            (stop.GetComponent<SpriteRenderer>().bounds.max.y > move.GetComponent<SpriteRenderer>().bounds.min.y &&
            stop.GetComponent<SpriteRenderer>().bounds.max.y < move.GetComponent<SpriteRenderer>().bounds.max.y))
        {
            yIntersect = true;
        }
        if (
            (move.GetComponent<SpriteRenderer>().bounds.max.x > stop.GetComponent<SpriteRenderer>().bounds.min.x &&
            move.GetComponent<SpriteRenderer>().bounds.max.x < stop.GetComponent<SpriteRenderer>().bounds.max.x) ||
            (stop.GetComponent<SpriteRenderer>().bounds.max.x > move.GetComponent<SpriteRenderer>().bounds.min.x &&
            stop.GetComponent<SpriteRenderer>().bounds.max.x < move.GetComponent<SpriteRenderer>().bounds.max.x))
        {
            xIntersect = true;
        }

        if (yIntersect && xIntersect)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}