using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyList;

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
        Vector2 point = Random.insideUnitCircle * 70.0f;
        Vector3 position = new Vector3(-100.0f, 0.0f, 3.5f);

        int enemyIndex = Random.Range(0, enemyList.Length);
        GameObject enemy = Instantiate(enemyList[enemyIndex], position, Quaternion.identity);
        enemy.GetComponent<EnemyCntrl>().Set(fighter);

        fighter.transform.position = new Vector3(69.0f, 0.0f, 3.5f);
    }
}
