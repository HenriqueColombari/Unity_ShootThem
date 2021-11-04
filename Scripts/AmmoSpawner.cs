using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Ammo;

    [SerializeField]
    private GameObject Ship;

    Rigidbody2D rb2d;

    Camera mainCamera;


    private float waitTime = 10.0f;
    private float timer = 0.0f;

    Vector2 leftBottomCorner;

    Vector2 leftTopCorner;

    Vector2 rightBottomCorner;


    private void Start()
    {
        mainCamera = Camera.main;

        leftBottomCorner = mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f));

        leftTopCorner = mainCamera.ScreenToWorldPoint(new Vector2(0f, Screen.height));

        rightBottomCorner = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0f));




    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer -= waitTime;
            GameObject ammo = Instantiate(Ammo);
            ammo.transform.position = new Vector2(Random.Range(leftBottomCorner[0], rightBottomCorner[0]),
            Random.Range(leftBottomCorner[1], leftTopCorner[1]));
            rb2d = ammo.GetComponent<Rigidbody2D>();

            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            direction.Normalize();
            rb2d.velocity = direction * 0f;

            ammo.GetComponent<AmmoController>().setShip(Ship);
        }
    }
}
