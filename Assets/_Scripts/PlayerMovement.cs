using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CarSpawner carSpawnerScript;
    public CoinsSpawner coinsSpawnerScript;
    public float speed = 5f, rotationSpeed = 4f;
    public GameObject explosionPrefab;
    public Sprite bujji, burnCar;

    public float shakeIntensity = 0.005f;
    public float shakeSpeed = 1f;

    private Vector3 originalScale;
    float prevSpeedBoost, speedShowValue;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(UpdatePlayerSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerShake();
        if(Input.GetKey(KeyCode.LeftArrow)){MoveLeft();}
        if(Input.GetKey(KeyCode.RightArrow)){MoveRight();}
        if(Input.GetKey(KeyCode.UpArrow)){MoveUp();}
        if(Input.GetKey(KeyCode.DownArrow)){MoveDown();}
        if(Input.GetKeyDown(KeyCode.Space)){Retry();}
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

    void MoveUp(){
        transform.position += new Vector3(0, speed*Time.deltaTime, 0);
    }

    void MoveDown(){
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }

        void Clamp(){
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x,-1.3f, 1.3f);
        pos.y = Mathf.Clamp(pos.y, -4f, 4f);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Collision Occured " + Eternals.isShieldOn);
        if(other.gameObject.tag == "car" && Eternals.isShieldOn == true){
            Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.tag == "car"){
            prevSpeedBoost = Eternals.speedBoostValue;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().sprite = burnCar;
            Eternals.speedBoostValue = 0f;
            FlowManager.Instance.AfterExplosion();
            carSpawnerScript.StopSpawning();
            coinsSpawnerScript.StopCoinSpawn();
            Eternals.isCarBroke = true;
        }

        string oName = other.gameObject.name;
        if(oName.Contains("coin")){Eternals.coins += 1;}
        if(oName.Contains("shield")){Eternals.shields += 1;}
        if(oName.Contains("fuel")){Eternals.fuels += 1;}
        if(oName.Contains("sprint")){Eternals.sprints += 1;}
        if(oName.Contains("magnet")){Eternals.magnets += 1;}
        FlowManager.Instance.SavePlayerPrefs();
        Destroy(other.gameObject);
    }

    void Retry(){
        GetComponent<SpriteRenderer>().sprite = bujji;
        Eternals.speedBoostValue = prevSpeedBoost;
        carSpawnerScript.StartSpawning();
        coinsSpawnerScript.StartCoinSpawn();
        FlowManager.Instance.UpdateSmokeLifetime(1f);
    }

    IEnumerator UpdatePlayerSpeed(){
        while(true){
            float carSpeed = Eternals.minSpeed - 50f + transform.position.y+Eternals.sprintSpeed;
            float fillValue = carSpeed/(Eternals.minSpeed + 50f);
            FlowManager.Instance.speedometer.fillAmount = fillValue;
            FlowManager.Instance.speedometerText.text = carSpeed.ToString("F0");
            // Debug.Log("speed is showing now " + carSpeed + " and " + fillValue);
            yield return new WaitForSeconds(0.05f);
        }
    }

}
