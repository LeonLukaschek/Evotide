using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPref;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void OnClick()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPref, this.transform.position + new Vector3(Random.Range(-1, 1), this.transform.position.y, Random.Range(-1, 1)), Quaternion.identity);
    }
}