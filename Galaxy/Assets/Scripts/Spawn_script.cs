using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_script : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPowerUpPrefab;
    [SerializeField]
    private GameObject _speedPowerUpPrefab;
    [SerializeField]
    private GameObject _shieldPowerUpPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AsteroidDestroyed()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (isAlive)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(2f,5f));
        }

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (isAlive)
        {
            yield return new WaitForSeconds(Random.Range(6f, 12f));
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int powerUpSelector = Random.Range(0, 4);
            if(powerUpSelector == 0)
            {
                GameObject tripleShot = Instantiate(_tripleShotPowerUpPrefab, spawnPosition, Quaternion.identity);
            }
            else if (powerUpSelector == 1)
            {
                GameObject speedBoost = Instantiate(_speedPowerUpPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                GameObject shieldPowerUp = Instantiate(_shieldPowerUpPrefab, spawnPosition, Quaternion.identity);
            }

        }

    }

    public void KillPlayer()
    {
        isAlive = false;
    }
}
