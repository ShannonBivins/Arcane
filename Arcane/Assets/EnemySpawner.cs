using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public float spawnTimer = 0.5f;
    public float elapsedTime = 0.0f;
    private int[] multiplier = {-1, 1};
    public float spawnDistance = 30f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > spawnTimer)
        {
            elapsedTime = 0f;


            Vector2 spawnLocation = SetLocation();
            int enemyIndex = SelectEnemy();

            GameObject newEnemy = Instantiate(enemies[enemyIndex], spawnLocation, Quaternion.identity);
            newEnemy.SetActive(true);
        }
    }

    Vector2 SetLocation()
    {
        int i = Random.Range(0, 2);
        int j = Random.Range(0, 2);

        float x = Random.Range(0f, spawnDistance);
        float y;

        if(x > spawnDistance/2)
        {
            y = Random.Range(0f, spawnDistance);
        }
        else
        {
            y = Random.Range(spawnDistance/2, spawnDistance);
        }

        x *= multiplier[i];
        y *= multiplier[j];

        return new Vector2(transform.position.x + x, transform.position.y + y);
    }

    int SelectEnemy()
    {
        float rng = Random.Range(0f, 1f);

        if(rng > 0.9f)
        {
            return 2;
        }
        else if(rng > 0.75f)
        {
            return 3;
        }
        else if(rng > 0.4f)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
