using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    private float forceIntensity;

    void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            forceIntensity = 15f;
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.AddForce(transform.up * forceIntensity, ForceMode2D.Impulse);
            
        }
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.CompareTag(collision.tag))
        {
            Destroy(gameObject);
        }
    }
}
