using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    float speed, magnetRange = 3f;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        speed = 3f ;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (Eternals.isMagnetOn && distance < magnetRange)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 3f * Time.deltaTime);
        }
        else{
            transform.position -= new Vector3(0, speed * Time.deltaTime * Eternals.speedBoostValue);            
        }

        if (transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }
    }
}
