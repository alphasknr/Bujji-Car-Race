using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    public GameObject[] cars;
    public GameObject treePrefab, sensorPrefab;
    public Sprite[] treeSprites, bushSprites;
    public float[] treePositions, carPositions;
    public float gapValue, bushSpeed = 1f;
    float treePos1, treePos2, yRotation1, yRotation2, carPos;
    int laneNumber, prevLaneNumber = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCars());
        StartCoroutine(SpawnTrees());
    }

    private void Update() {
        Eternals.bushSpeed = bushSpeed;
    }

    void Cars(){
        int rand = Random.Range(0, cars.Length);
        int randPos = Random.Range(1, 5);
        if(randPos == 1){carPos = Random.Range(carPositions[0], carPositions[1] + 0.01f); laneNumber = 1;}
        else if(randPos == 2){carPos = Random.Range(carPositions[2], carPositions[3] + 0.01f); laneNumber = 2;}
        else if(randPos == 3){carPos = Random.Range(carPositions[4], carPositions[5] + 0.01f); laneNumber = 3;}
        else if(randPos == 4){carPos = Random.Range(carPositions[6], carPositions[7] + 0.01f); laneNumber = 4;}
        GameObject vehicle = Instantiate(cars[rand], new Vector3(carPos, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
        Vector3 sensorPos = new Vector3(vehicle.transform.position.x, 5f, 0);
        GameObject sensor = Instantiate(sensorPrefab, sensorPos, Quaternion.Euler(0, 0,0), vehicle.transform);
        sensor.transform.localPosition = new Vector3(0, 7, 0);
        vehicle.name += " - " + laneNumber.ToString();
    }

    IEnumerator SpawnCars(){
        if(prevLaneNumber == laneNumber){ Debug.Log("same Lane"); yield return null;} else {Eternals.gapValue = gapValue;}
        prevLaneNumber = laneNumber;
        while(true){
            yield return new WaitForSeconds(Eternals.gapValue/Eternals.speedBoostValue);
            Cars();
        }
    }

    IEnumerator SpawnTrees(){
        while(true){
            float interval = Random.Range(0.2f, 0.7f);
            yield return new WaitForSeconds(interval/Eternals.speedBoostValue);
            Trees();
        }
    }

    void Trees(){
        treePos1 = Random.Range(treePositions[0], treePositions[1] + 0.01f);
        treePos2 = Random.Range(treePositions[2], treePositions[3] + 0.01f);
        GameObject treeObj1 = Instantiate(treePrefab, new Vector3(treePos1, transform.position.y, transform.position.z), Quaternion.Euler(0, yRotation1, 0));
        GameObject treeObj2 = Instantiate(treePrefab, new Vector3(treePos2, transform.position.y, transform.position.z), Quaternion.Euler(0, yRotation2, 0));
        int random1 = Random.Range(0, 2);
        int random2 = Random.Range(0, 2);
        if (random1 == 0) { treeObj1.GetComponent<SpriteRenderer>().flipX = true; }
        if (random2 == 0) { treeObj2.GetComponent<SpriteRenderer>().flipX = true; }
        int randTree1 = Random.Range(0, treeSprites.Length);
        int randTree2 = Random.Range(0, treeSprites.Length);
        treeObj1.GetComponent<SpriteRenderer>().sprite = treeSprites[randTree1];
        treeObj2.GetComponent<SpriteRenderer>().sprite = treeSprites[randTree2];
        // int randBush1 = Random.Range(0, bushSprites.Length);
        // int randBush2 = Random.Range(0, bushSprites.Length);
        // treeObj1.GetComponent<TreesManager>().bushSprite.sprite = bushSprites[randBush1];
        // treeObj2.GetComponent<TreesManager>().bushSprite.sprite = bushSprites[randBush2];
    }
}
