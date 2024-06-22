using System.Collections;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{

    public GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoinSpawn();
    }
    public void StartCoinSpawn(){
        StartCoroutine(CoinSpawner());
    }

    public void StopCoinSpawn(){
        StopCoroutine(CoinSpawner());
    }

    void CoinSpawn(){
        int randPos = Random.Range(1,5);
        float carPos = FindAnyObjectByType<CarSpawner>().SpawningPos(randPos);
        Instantiate(coinPrefab, new Vector3(carPos, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
    }

    IEnumerator CoinSpawner(){
        while(true){
            yield return new WaitForSeconds(1f*Eternals.coinGapMultiplier);
            CoinSpawn();
        }
    }
}
