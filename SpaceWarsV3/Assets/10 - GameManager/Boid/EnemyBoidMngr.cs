using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoidMngr : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int nEnemies;
    [SerializeField] private float combatRadius;

    private float viewRadius = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        float flankingDeg = 360.0f / nEnemies;
        float posDeg = 0.0f;

        for (int i = 0; i < nEnemies; i++)
        {
            float x = combatRadius * Mathf.Cos(posDeg * Mathf.Deg2Rad);
            float z = combatRadius * Mathf.Sin(posDeg * Mathf.Deg2Rad);

            Vector3 enemyPos = new Vector3(x, 0.0f, z);

            GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemy.GetComponent<BoidCntrl>().Set(viewRadius);

            posDeg += flankingDeg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
