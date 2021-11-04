using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    private float waitTime = 3.0f;
    private float timer = 0.0f;

    Rigidbody2D rb2d;

    private Transform fireSpot;

    [SerializeField]
    public Text TextScore;

    [SerializeField]
    private GameObject Ship;

    [SerializeField]
    AudioSource sfxAudioSource;

    [SerializeField]
    AudioClip shootClip;

    Camera mainCamera;

    Vector2 leftBottomCorner;

    Vector2 rightTopCorner;

    void Start()
    {
        mainCamera = Camera.main;

        InitCamValues();
    }

    void Update()
    {
        fireSpot = gameObject.transform;
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            //sfxAudioSource.PlayOneShot(shootClip);
            timer -= waitTime;
            GameObject goBullet = Instantiate(bulletPrefab, fireSpot.transform.position,
                fireSpot.transform.rotation);

            Vector2 direction = new Vector2(Ship.transform.position[0] - goBullet.transform.position[0],
                Ship.transform.position[1] - goBullet.transform.position[1]);

            direction.Normalize();

            goBullet.tag = gameObject.tag;
            goBullet.GetComponent<Rigidbody2D>().velocity = direction*3f;

            


        }
    }

    void FixedUpdate()
    {
        CheckCamAndUpdatePosition();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Ship.GetComponent<ShipController>().setScore(Ship.GetComponent<ShipController>().getScore() + 100);
            TextScore.text = "Score: " + Ship.GetComponent<ShipController>().getScore().ToString();

        }

    }


    void InitCamValues()
    {
        leftBottomCorner = mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f));
        rightTopCorner = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
    }


    void CheckCamAndUpdatePosition()
    {
        if (transform.position.x < leftBottomCorner.x)
            transform.position = new Vector3(rightTopCorner.x, transform.position.y, 0f);
        else if (transform.position.x > rightTopCorner.x)
            transform.position = new Vector3(leftBottomCorner.x, transform.position.y, 0f);
        else if (transform.position.y < leftBottomCorner.y)
            transform.position = new Vector3(transform.position.x, rightTopCorner.y, 0f);
        else if (transform.position.y > rightTopCorner.y)
            transform.position = new Vector3(transform.position.x, leftBottomCorner.y, 0f);
    }

    public void setShip(GameObject Ship)
    {
        this.Ship = Ship;
    }

    public void setAudio(AudioSource audio)
    {
        this.sfxAudioSource = audio;
    }
}
