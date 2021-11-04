using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoController : MonoBehaviour
{
    [SerializeField]
    private GameObject Ship;

    private void Start()
    {
        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if(Ship.GetComponent<ShipController>().ammo <= 5)
            {
                Ship.GetComponent<ShipController>().ammo = Ship.GetComponent<ShipController>().ammo + 5;
            }
            else
            {
                Ship.GetComponent<ShipController>().ammo = 10;
            }
            Destroy(gameObject);
        }
    }
    public void setShip(GameObject Ship)
    {
        this.Ship = Ship;
    }
}
