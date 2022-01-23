using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public float count;
    }
    public Wave[] waves;
    private float time;
    private int maxEnemies= 1;
    private int maxArcher = 1;
    private int NextWafe;
    private void Start()
    {
        NextWafe = 10;
    }
    private void Update()
    {
        
        time += Time.deltaTime;
        if (GameObject.FindGameObjectsWithTag("melee").Length < maxEnemies)
        {
            SpawnWave(waves[1]);
        }
        if(GameObject.FindGameObjectsWithTag("archer").Length < maxArcher)
        {
            SpawnWave(waves[0]);
        }
        if ((time > NextWafe)&&(maxEnemies<6))
        {
            IncreaseEnemies();
            time = 0f;
        }
    }
      
    public void SpawnWave(Wave _wave)
    {
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
        }
    }
    void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, new Vector3(11f,Random.Range(-2.93f,0.57f),0),Quaternion.identity);
    }
    public void IncreaseEnemies()
    {
        
        maxEnemies+=2;
        maxArcher++;
        NextWafe *= 3;
        Debug.Log(maxEnemies);
    }
}