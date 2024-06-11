using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{

    public GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoinSpawner());
    }

    void CoinSpawn(){
        float rand = UnityEngine.Random.Range(-1.8f, 1.8f);
        Instantiate(coinPrefab, new Vector3(rand, transform.position.y, transform.position.z),Quaternion.identity);
    }

    IEnumerator CoinSpawner(){
        while(true){
            int interval = UnityEngine.Random.Range(10,20);
            yield return new WaitForSeconds(interval);
            CoinSpawn();
        }
    }
}
