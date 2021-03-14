using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject VolantEnemy, StaticEnemy, StaticShooterEnemy, VolantShooterEnemy;
    GameManager gm;
    private int volantShooterCounter, staticCounter = 0;

    void Start()
    {
        gm = GameManager.GetInstance();
        GameManager.changeStateDelegate += SpawnEnemies;
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
       if(gm.gameState != GameManager.GameState.GAME) return;

       Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
       Vector3 position = new Vector3(Random.Range(playerPosition.x + 5.0f, playerPosition.x + 10.0f), Random.Range(playerPosition.y - 3.0f, playerPosition.y + 6.0f));
       
       if(GameObject.FindGameObjectsWithTag("Enemy").Length < 10){
            
            Instantiate(Random.Range(0, 2) == 0 ? StaticShooterEnemy: VolantEnemy, position, Quaternion.identity, transform);
        }
        if(volantShooterCounter == 0){
            Instantiate(VolantShooterEnemy, new Vector3(2.0f, 3.0f, 0), Quaternion.identity);
            volantShooterCounter++;
        }
        if(staticCounter < 3){
            Instantiate(StaticEnemy, position, Quaternion.identity);
            staticCounter++;
        }
    }

    void Update() 
    {    
        if(gm.gameState == GameManager.GameState.END || gm.gameState == GameManager.GameState.MENU)
        {
            ResetObjects();
            return;
        }
        SpawnEnemies();
    }

    private void ResetObjects()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }
        volantShooterCounter = 0;
        staticCounter = 0;
       
    }
}
