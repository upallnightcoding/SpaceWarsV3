using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearDeathCntrl : MonoBehaviour
{
    [SerializeField] private Material nearDeathMaterial;

    private int enemyId = -1;

    public void Set(int enemyId)
    {
        this.enemyId = enemyId;
    }

    private void NearDeath(int enemyId)
    {
        if (nearDeathMaterial && (this.enemyId == enemyId))
        {
            GetComponent<MeshRenderer>().material = nearDeathMaterial;
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnNearEnemyDeath += NearDeath;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnNearEnemyDeath -= NearDeath;
    }
}