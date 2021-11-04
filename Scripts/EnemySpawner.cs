using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    Rigidbody2D rb2d;

    [SerializeField]
    private GameObject Ship;

    [SerializeField]
    AudioSource sfxAudioSource;

    [SerializeField]
    private Text TextScore;

    Camera mainCamera;

    private float waitTime = 10.0f;
    private float timer = 0.0f;

    Vector2 leftTopCorner;
    Vector2 rightTopCorner;


    private void Start()
    {
        mainCamera = Camera.main;
        leftTopCorner = mainCamera.ScreenToWorldPoint(new Vector2(0f, Screen.height));
        rightTopCorner = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));



    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer -= waitTime;
            GameObject go = Instantiate(enemyPrefab);
            go.transform.position = new Vector2(Random.Range(leftTopCorner[0], rightTopCorner[0]), leftTopCorner[1]*1.1f);
            rb2d = go.GetComponent<Rigidbody2D>();


            Vector2 direction;

            direction = new Vector2(0, -1);

            direction.Normalize();
            rb2d.velocity = direction * Random.Range(1.0f, 3.0f);

            go.GetComponent<EnemyController>().setShip(Ship);
            go.GetComponent<EnemyController>().setAudio(sfxAudioSource);
            go.GetComponent<EnemyController>().TextScore = TextScore;

        }
    }
}
