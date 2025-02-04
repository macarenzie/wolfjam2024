using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Kill,
    Capture,
}
public class BulletManager : MonoBehaviour
{
    // === FIELDS ===

    private GameObject gun;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    private float speed = 6.0f;

    [SerializeField] private Sprite spiritGun;
    [SerializeField] private Sprite cryptidGun;
    [SerializeField] private Sprite spiritBullet;
    [SerializeField] private Sprite cryptidBullet;

    // === PROPERTIES ===

    public GunType Type
    {
        get { return _type; }
        set { _type = value; }
    }
    private GunType _type = GunType.Capture;


    public List<GameObject> bullets
    {
        get { return _bullets; }
        set { _bullets = value; }
    }
    private List<GameObject> _bullets = new List<GameObject>();

    // === METHODS ===

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject currentBullet in bullets)
        {
            currentBullet.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// InputAction.CallbackContext context
    /// Fire a bullet
    /// </summary>
    public void OnFire()
    {
        bullets.Add(Instantiate(bullet, player.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity));
    }

    /// <summary>
    /// InputAction.CallbackContext context
    /// Switch gun type
    /// </summary>
    public void OnSwitch()
    {

        // switch gun types
        switch (Type)
        {
            case GunType.Kill:
                gun.GetComponent<SpriteRenderer>().sprite = spiritGun;
                bullet.GetComponent<SpriteRenderer>().sprite = spiritBullet;
                Type = GunType.Capture;
                break;
            default:
                gun.GetComponent<SpriteRenderer>().sprite = cryptidGun;
                bullet.GetComponent<SpriteRenderer>().sprite = cryptidBullet;
                Type = GunType.Kill;
                break;
        }
    }
}
