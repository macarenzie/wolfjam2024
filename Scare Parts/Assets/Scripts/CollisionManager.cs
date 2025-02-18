using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // == FIELDS ===

    private float camHeight;

    [SerializeField] private GameObject player;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Camera cam;
    [SerializeField] private Sprite capturedSpirit;
    [SerializeField] private Sprite capturedCryptid;


    // === PROPERTIES ===

    public bool IsHit
    {
        get { return _isHit;  }
        set { _isHit = value; }
    }
    private bool _isHit = false;

    public List<GameObject> Enemies
    {
        get { return _enemies; }
        set { _enemies = value; }
    }
    private List<GameObject> _enemies;

    public List<GameObject> Obstacles
    {
        get { return _obstacles; }
        set { _obstacles = value; }
    }
    private List<GameObject> _obstacles;


    // === METHODS ===

    // Start is called before the first frame update
    void Start()
    {
        Enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        Obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
        camHeight = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        IsHit = false;
        for (int i = 0; i < Obstacles.Count; i++)
        {
            // Collision occuring
            if (AABBCollision(player, Obstacles[i]) && !Obstacles[i].GetComponent<Obstacle>().WasHit)
            {
                // if the obstacle is a rock or log
                if (Obstacles[i].GetComponent<Obstacle>().Type != ObstacleType.Puddle)
                {
                    // turn off hitbox
                    Obstacles[i].GetComponent<Obstacle>().WasHit = true;

                    // lower health
                    switch (Obstacles[i].GetComponent<Obstacle>().Type)
                    {
                        case ObstacleType.Rock:
                            playerManager.ResourceChange(true, -15f);
                            print("hit");
                            IsHit = true;
                            break;
                        default:
                            playerManager.ResourceChange(true, -10f);
                            IsHit = true;
                            break;
                    }
                }
                // if the obstacle is a puddle
                else
                {
                    player.GetComponent<PlayerManager>().IsSlipping = true;
                    IsHit =true;
                    print("hit");
                }
            }
            else if(Obstacles[i].GetComponent<Obstacle>().Type == ObstacleType.Puddle)
            {
                player.GetComponent<PlayerManager>().IsSlipping = false;
            }
        }

        // AABB
        for (int i = 0; i < Enemies.Count; i++)
        {
            // Collision occuring
            if (AABBCollision(player, Enemies[i]))
            {
                // if the enemy is not caught
                if (!Enemies[i].GetComponent<Enemy>().IsCaught)
                {
                    // lower health
                    switch (Enemies[i].GetComponent<Enemy>().Type)
                    {
                        case EnemyType.Cryptid:
                            playerManager.ResourceChange(true, -10f);
                            IsHit = true;
                            print("hit");
                            break;
                        default:
                            playerManager.ResourceChange(true, -5f);
                            IsHit = true;
                            print("hit");
                            break;
                    }

                    Destroy(Enemies[i]);
                    Enemies.RemoveAt(i);
                    if (i != 0)
                    {
                        i--;
                    } 
                }
                // if the enemy is caught
                else
                {
                    // add gas/health
                    switch (Enemies[i].GetComponent<Enemy>().Type)
                    {
                        case EnemyType.Cryptid:
                            playerManager.ResourceChange(true, 10f);
                            break;
                        default:
                            playerManager.ResourceChange(false, 10f);
                            break;
                    }

                    Destroy(Enemies[i]);
                    Enemies.RemoveAt(i);
                    if (i != 0)
                    {
                        i--;
                    }
                }
            }

            // destroy enemy if off bottom of screen
            if (Enemies[i].GetComponent<SpriteRenderer>().bounds.max.y < -camHeight)
            {
                Destroy(Enemies[i]);
                Enemies.RemoveAt(i);
                if (i != 0)
                {
                    i--; 
                }
            }
        }
        
        for (int j = 0; j < bulletManager.bullets.Count; j++)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                // Collision occuring
                if (AABBCollision(bulletManager.bullets[j], Enemies[i]))
                {
                    if ((bulletManager.Type == GunType.Capture && Enemies[i].GetComponent<Enemy>().Type == EnemyType.Spirit) ||
                        (bulletManager.Type == GunType.Kill && Enemies[i].GetComponent<Enemy>().Type == EnemyType.Cryptid))
                    {
                        // capture enemy, change sprites
                        Enemies[i].GetComponent<Enemy>().IsCaught = true;
                        switch (Enemies[i].GetComponent<Enemy>().Type)
                        {
                            case EnemyType.Spirit:
                                Enemies[i].GetComponent<SpriteRenderer>().sprite = capturedSpirit;
                                break;
                            default:
                                Enemies[i].GetComponent<SpriteRenderer>().sprite = capturedCryptid;
                                Enemies[i].GetComponent<Animation>().enabled = false;
                                Enemies[i].GetComponent<Animator>().enabled = false;
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
        //isHit = false;
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