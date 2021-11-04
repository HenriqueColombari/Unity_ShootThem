using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] asteroidPrefabs;

    [SerializeField]
    public Text TextScore;

    [SerializeField]
    private GameObject Ship;

    bool canBeBronken = true;

    Camera mainCamera;

    Vector2 leftBottomCorner;

    Vector2 rightTopCorner;

    void Start()
    {

        mainCamera = Camera.main;

        leftBottomCorner = mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f));

        rightTopCorner = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void FixedUpdate()
    {
        CheckCamAndUpdatePosition();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player") || collision.CompareTag("Bullet"))
        {
            Ship.GetComponent<ShipController>().setScore(Ship.GetComponent<ShipController>().getScore() + 50);
            TextScore.text = "Score: " + Ship.GetComponent<ShipController>().getScore().ToString();

            if (canBeBronken)
            {
                Vector3[] directions = new Vector3[3];
                directions[0] = -collision.transform.right;
                directions[1] = collision.transform.up; 
                directions[2] = collision.transform.right; 

                for (int i = 0; i < 3; i++)
                {
                    GameObject go = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]);
                    go.transform.up = directions[i];
                    go.transform.position = transform.position + directions[i];
                    go.GetComponent<Rigidbody2D>().velocity = directions[i] * 3f;
                    go.transform.localScale = new Vector3(0.7f, 0.7f, 0f);
                    go.GetComponent<AsteroidController>().SetCanBeBroken(false);
                    go.GetComponent<AsteroidController>().setShip(Ship);
                    go.GetComponent<AsteroidController>().TextScore = TextScore;
                    Destroy(go, 5f);
                    
                }
            }
            Destroy(gameObject);
        }
    }

    public void SetCanBeBroken(bool value)
    {
        canBeBronken = value;
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

}
