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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEngagement(GameObject fighter, LevelData levelData)
    {
        Vector2 randomPoint = Random.insideUnitCircle * 70.0f;
        //Vector3 enemyPos = new Vector3(-100.0f+randomPoint.x, 0.0f, 3.5f+randomPoint.y);
        Vector3 enemyPos = new Vector3(69.0f+randomPoint.x, 0.0f, 3.5f+randomPoint.y);

        int enemyIndex = Random.Range(0, enemyList.Length);
        GameObject enemy = Instantiate(enemyList[enemyIndex], enemyPos, Quaternion.identity);
        enemy.GetComponent<EnemyCntrl>().Set(fighter);
        enemyCount++;

        fighter.transform.position = new Vector3(69.0f, 0.0f, 3.5f);
    }

    private void OnDestroyEnemy()
    {
        if (--enemyCount == 0)
        {
            EventManager.Instance.InvokeOnQuitEngagment("Congratulations");
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnDestroyEnemy += OnDestroyEnemy;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDestroyEnemy -= OnDestroyEnemy;
    }
}

