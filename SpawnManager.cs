using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUp;

    private bool _stopSpawning = false;
    // Start is called before the first frame update

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        //yield return null; //wait for 1 frame
        yield return new WaitForSeconds(2.0f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.1f,9.1f),8,0);
            GameObject newEnemy = Instantiate(_enemy,posToSpawn,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while(_stopSpawning == false)
        {

            Vector3 posToSpawn = new Vector3(Random.Range(-9.1f,9.1f),8,0);
            int randomPowerup = Random.Range(0,3); 
            GameObject newPowerup = Instantiate(_powerUp[randomPowerup],posToSpawn,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f,8.0f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
