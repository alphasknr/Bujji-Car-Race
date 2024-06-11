using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public ScoreManager scoreManagerScript;
    public float speed = 5f, rotationSpeed = 4f;
    public GameObject gameOverPanel;
    bool isMovingLeft = false, isMovingRight = false;

    public float shakeIntensity = 0.005f;
    public float shakeSpeed = 1f;

    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;

        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerShake();
        if(Input.GetKey(KeyCode.LeftArrow)){MoveLeft();}
        if(Input.GetKey(KeyCode.RightArrow)){MoveRight();}
        if(Input.GetKeyDown(KeyCode.Space)){Time.timeScale = 1f;}
        MovementRotation();
        Clamp();
        
    }

    void PlayerShake(){
        float scaleX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) * 2f - 1f;
        float scaleY = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) * 2f - 1f;
        Vector3 shakeScale = new Vector3(scaleX, scaleY, 0) * shakeIntensity;
        transform.localScale = originalScale + shakeScale;
    }

    void MovementRotation(){
        if(transform.position.z != 90){
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,0),10f*Time.deltaTime);
        }
    }

    void MoveLeft(){
        transform.position -= new Vector3(speed*Time.deltaTime,0,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,43),rotationSpeed*Time.deltaTime);
    }

    void MoveRight()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -43), rotationSpeed * Time.deltaTime);
    }

    public void MovingLeft(bool pLeft){isMovingLeft = pLeft;}
    public void MOvingRight(bool pRight){isMovingRight = pRight;}

    void Clamp(){
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x,-1.3f, 1.3f);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        Debug.Log("Collision Occured");
        if(other.gameObject.tag == "car" && Eternals.isShieldOn == false){
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if(other.gameObject.tag == "Coin"){
            scoreManagerScript.score += 10;
            Destroy(other.gameObject);
        }
    }

}
