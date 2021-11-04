using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashScript :  MonoBehaviour, IPointerDownHandler
{    [SerializeField]
    private GameObject Ship;

    [SerializeField]
    private GameObject dash;

    [SerializeField]
    private GameObject dashCollider;

    private float waitTime = 35.0f;
    private float waitTimer;
    private float stateTime = 2.0f;
    private float stateTimer;
    

    [SerializeField]
    AudioSource sfxAudioSource;

    [SerializeField]
    AudioClip YOUSHALLNOTPASS;

    void Start()
    {
        dashCollider.SetActive(false);
        waitTimer = waitTime;
        stateTimer = 0;
    }

    void Update()
    {
        waitTimer += Time.deltaTime;
        stateTimer += Time.deltaTime;
        if (waitTimer >= waitTime)
        {
            dash.SetActive(true);
        }
        if(stateTimer >= stateTime)
        {
            dashCollider.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
   
    public void OnDrag(PointerEventData eventData)
    {
        if(Input.touchCount == 2)
        {
            if (waitTimer > waitTime)
            {
                waitTimer = 0;
                stateTimer = 0;
                Ship.GetComponent<ShipController>().setVelocity(new Vector2(0, 30f));
                sfxAudioSource.PlayOneShot(YOUSHALLNOTPASS);
                dash.SetActive(false);
                dashCollider.SetActive(true);
                
            }
        }
    }
}
