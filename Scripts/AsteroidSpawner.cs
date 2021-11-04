using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] asteroidPrefabs;

    [SerializeField]
    private GameObject Ship;

    [SerializeField]
    private Text TextScore;

    Rigidbody2D rb2d;

    Camera mainCamera;

    private float waitTime = 7.0f;
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
            GameObject go = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]);
            go.transform.position = new Vector2(Random.Range(leftBottomCorner[0], rightBottomCorner[0]),
            Random.Range(leftBottomCorner[1], leftTopCorner[1]));
            go.GetComponent<AsteroidController>().SetCanBeBroken(true);
            go.GetComponent<AsteroidController>().setShip(Ship);
            go.GetComponent<AsteroidController>().TextScore = TextScore;
            rb2d = go.GetComponent<Rigidbody2D>();

            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            direction.Normalize();
            rb2d.velocity = direction * Random.Range(1.0f, 3.0f);

            

        }
    }
}
