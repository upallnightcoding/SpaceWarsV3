using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyList;

    private int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartEngagement(GameObject fighter, LevelData levelData)
    {
        
        switch(levelData.GetLevel())
        {
            case LevelType.TUTORIAL:
                CreateRandomEnemies(fighter, 1);
                break;
            case LevelType.HAVOC:
                CreateRandomEnemies(fighter, 15);
                break;
            default:
                CreateRandomEnemies(fighter, 2);
                break;
        }

        PositionFighter(fighter);
    }

    private void CreateRandomEnemies(GameObject fighter, int nEnemies)
    {
        for (int enemyIndex = 0; enemyIndex < nEnemies; enemyIndex++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * 70.0f;
            Vector3 enemyPos = new Vector3(69.0f + randomPoint.x, 0.0f, 3.5f + randomPoint.y);

            int choose = Random.Range(0, enemyList.Length);
            GameObject enemy = Instantiate(enemyList[choose], enemyPos, Quaternion.identity);
            enemy.GetComponent<EnemyCntrl>().Set(fighter);
            enemy.GetComponent<TakeDamageCntrl>().Set(enemyIndex);
            enemyCount++;
        }
    }

    private void PositionFighter(GameObject fighter)
    {
        fighter.transform.position = new Vector3(69.0f, 0.0f, 3.5f);
    }

    private void OnDestroyEnemy()
    {
        if (--enemyCount == 0)
        {
            EventManager.Instance.InvokeOnPlayerWins();
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnDestroyEnemyShip += OnDestroyEnemy;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDestroyEnemyShip -= OnDestroyEnemy;
    }
}

