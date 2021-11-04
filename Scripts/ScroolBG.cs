using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScroolBG : MonoBehaviour
{
    Camera mainCamera;
    Rigidbody2D rb2d;
    BoxCollider2D bc2d;
    float groundVerticalLenght;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -3f);

        bc2d = GetComponent<BoxCollider2D>();
        groundVerticalLenght = bc2d.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -groundVerticalLenght - 2*mainCamera.ScreenToWorldPoint(new Vector2(0f, Screen.height)).y)
        {
            repositionBackground();
        }
    }

    private void repositionBackground()
    {
        Vector2 groundOffset = new Vector2(0, groundVerticalLenght * 2f);
        float y = groundOffset.y;
        transform.position = new Vector2(0, 2* y - 3* mainCamera.ScreenToWorldPoint(new Vector2(0f, Screen.height)).y) ;
    }
}
