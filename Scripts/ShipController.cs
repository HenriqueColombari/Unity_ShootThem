using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShipController : MonoBehaviour
{
    [SerializeField]
    VirtualJoystick joystick;

    [SerializeField]
    private Transform spawnPoint;
    
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject[] lifes;

    [SerializeField]
    private Text TextScore;

    [SerializeField]
    private Text TextAmmo;

    [SerializeField]
    private Transform fireSpot;

    [SerializeField]
    AudioSource sfxAudioSource;

    [SerializeField]
    AudioClip shootClip;

    private bool inputShoot = false;

    private int dano = 3;

    private int score = 0;

    public float ammo = 10;

    Rigidbody2D rb2d;

    Camera mainCamera;


    Vector2 leftBottomCorner;

    Vector2 rightTopCorner;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;

        TextScore.text = "Score: " + score.ToString();

        

        InitCamValues();

    }

    void Update()
    {
        if(inputShoot)
        {
            if(ammo > 0)
            {
                ammo -= 1;

                GameObject goBullet = Instantiate(bulletPrefab, fireSpot.position, fireSpot.rotation);
                sfxAudioSource.PlayOneShot(shootClip);
                goBullet.tag = gameObject.tag;
            }

            inputShoot = false;
        }

        TextAmmo.text = "Ammo: " + ammo.ToString();
        transform.Translate(joystick.GetInput() * 5f * Time.deltaTime);


    }

    void FixedUpdate()
    {
        CheckCamAndUpdatePosition();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Ammo") && !collision.CompareTag("Bullet"))
        {
            dano -= 1;
            lifes[dano].SetActive(false);

            if (dano == 0)
            {
                ResetState();
                dano = 3;
                for (int i = 0; i < lifes.Length; i++)
                {
                    lifes[i].SetActive(true);
                }
            }
        }
        

    }

    void ResetState()
    {
        transform.position = spawnPoint.position;
        rb2d.velocity = Vector2.zero; 
        rb2d.angularVelocity = 0f;
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
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
        else if(transform.position.x > rightTopCorner.x)
            transform.position = new Vector3(leftBottomCorner.x, transform.position.y, 0f);
        else if (transform.position.y < leftBottomCorner.y)
            transform.position = new Vector3(transform.position.x, rightTopCorner.y, 0f);
        else if (transform.position.y > rightTopCorner.y)
            transform.position = new Vector3(transform.position.x, leftBottomCorner.y, 0f);
    }

    public void setInputShoot(bool shoot)
    {
        this.inputShoot = shoot;
    }

    public void setScore(int score)
    {
        this.score = score;
    }

    public int getScore()
    {
        return this.score;
    }

    public void setVelocity(Vector2 velocity)
    {
        rb2d.velocity = Vector2.zero;
        rb2d.velocity = velocity;
    }
}
