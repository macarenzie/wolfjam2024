using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // fields
    private List<GameObject> enemies;
    private List<GameObject> obstacles;
    [SerializeField] private GameObject player;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Camera cam;
    [SerializeField] private Sprite capturedSpirit;
    [SerializeField] private Sprite capturedCryptid;
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

    public List<GameObject> Obstacles
    {
        get
        {
            return obstacles;
        }
        set
        {
            obstacles = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
        camHeight = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            // Collision occuring
            if (AABBCollision(player, obstacles[i]) && !obstacles[i].GetComponent<Obstacle>().WasHit)
            {
                // if the obstacle is a rock or log
                if (obstacles[i].GetComponent<Obstacle>().Type != ObstacleType.Puddle)
                {
                    // turn off hitbox
                    obstacles[i].GetComponent<Obstacle>().WasHit = true;

                    // lower health
                    switch (obstacles[i].GetComponent<Obstacle>().Type)
                    {
                        case ObstacleType.Rock:
                            playerManager.ResourceChange(true, -15f);
                            break;
                        default:
                            playerManager.ResourceChange(true, -10f);
                            break;
                    }

                }
                // if the obstacle is a puddle
                else
                {
                    player.GetComponent<PlayerManager>().IsSlipping = true;
                }
            }
            else if(obstacles[i].GetComponent<Obstacle>().Type == ObstacleType.Puddle)
            {
                player.GetComponent<PlayerManager>().IsSlipping = false;
            }
        }

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
                    switch (enemies[i].GetComponent<Enemy>().Type)
                    {
                        case EnemyType.Cryptid:
                            playerManager.ResourceChange(true, -10f);
                            break;
                        default:
                            playerManager.ResourceChange(true, -5f);
                            break;
                    }

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
                    switch (enemies[i].GetComponent<Enemy>().Type)
                    {
                        case EnemyType.Cryptid:
                            playerManager.ResourceChange(true, 10f);
                            break;
                        default:
                            playerManager.ResourceChange(false, 10f);
                            break;
                    }

                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                    if (i != 0)
                    {
                        i--;
                    }
                }
            }

            // destroy enemy if off bottom of screen
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
                    if ((bulletManager.Type == GunType.Capture && enemies[i].GetComponent<Enemy>().Type == EnemyType.Spirit) ||
                        (bulletManager.Type == GunType.Kill && enemies[i].GetComponent<Enemy>().Type == EnemyType.Cryptid))
                    {
                        // capture enemy, change sprites
                        enemies[i].GetComponent<Enemy>().IsCaught = true;
                        switch (enemies[i].GetComponent<Enemy>().Type)
                        {
                            case EnemyType.Spirit:
                                enemies[i].GetComponent<SpriteRenderer>().sprite = capturedSpirit;
                                break;
                            default:
                                enemies[i].GetComponent<SpriteRenderer>().sprite = capturedCryptid;
                                break;
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