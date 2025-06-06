using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private GameObject[] enemyList;
    [SerializeField] private LayerMask boidMask;

    private int enemyCount = 0;

    private float combatRadius;

    // Start is called before the first frame update
    void Start()
    {
        combatRadius = gameData.combatRadius;
    }

    public void StartEngagement(GameObject fighter, LevelData levelData)
    {
        
        switch(levelData.GetLevelType())
        {
            case LevelType.TUTORIAL:
                CreateRandomEnemies(fighter, 4);
                break;
            case LevelType.HAVOC:
            case LevelType.BERSERK:
                CreateRandomEnemies(fighter, 15);
                break;
            case LevelType.LEVEL:
                CreateRandomEnemies(fighter, levelData.Level);
                break;
        }

        PositionFighter(fighter);
    }

    /**
     * CreateRandomEnemies() - 
     */
    private void CreateRandomEnemies(GameObject fighter, int nEnemies)
    {
        float flankingDeg = 360.0f / nEnemies;
        float posDeg = 0.0f;

        for (int enemyIndex = 0; enemyIndex < nEnemies; enemyIndex++)
        {
            float x = combatRadius * Mathf.Cos(posDeg * Mathf.Deg2Rad);
            float z = combatRadius * Mathf.Sin(posDeg * Mathf.Deg2Rad);

            Vector3 enemyPos = new Vector3(x, 0.0f, z);

            int choose = Random.Range(0, enemyList.Length);
            GameObject enemy = Instantiate(enemyList[choose], enemyPos, Quaternion.identity);
            enemy.GetComponent<EnemyCntrl>().Set(fighter, enemyIndex, boidMask);
            enemy.GetComponent<TakeDamageCntrl>().Set(enemyIndex);
            enemy.GetComponentInChildren<NearDeathCntrl>().Set(enemyIndex);

            posDeg += flankingDeg;
        }

        enemyCount = nEnemies;

        EventManager.Instance.InvokeOnSetEnemyCount(nEnemies);
    }

    /**
     * PositionFighter() - 
     */
    private void PositionFighter(GameObject fighter)
    {
        fighter.transform.position = new Vector3(69.0f, 0.0f, 3.5f);
    }

    /**
     * OnDestroyEnemy() - When an enemy has been destroyed, one is subtracted
     * from the emeny counter.  If the enemy counter has reached zero, the 
     * player is determined as the winner and the InvokeOnPlayerWins event
     * is invoked.
     */
    private void OnDestroyEnemy()
    {
        EventManager.Instance.InvokeOnSetEnemyCount(--enemyCount);

        if (enemyCount == 0)
        {
            EventManager.Instance.InvokeOnPlayerWins();
        }
    }

    /**
     * OnPlayerLooses() - When the fighter looses, the enemy of the
     * counter must be reset to zero.
     */
    private void OnPlayerLooses()
    {
        enemyCount = 0;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnDestroyEnemyShip += OnDestroyEnemy;
        EventManager.Instance.OnPlayerLooses += OnPlayerLooses;
        EventManager.Instance.OnDisengage += OnPlayerLooses;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDestroyEnemyShip -= OnDestroyEnemy;
        EventManager.Instance.OnPlayerLooses -= OnPlayerLooses;
        EventManager.Instance.OnDisengage -= OnPlayerLooses;
    }
}

