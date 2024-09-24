using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fighter", menuName = "CyberWars/Fighter")]
public class FighterSO : ScriptableObject
{
    public string fighterName;

    public GameObject fighterPrefab;

    public GameObject Create(Vector3 position)
    {
        GameObject go = Instantiate(fighterPrefab, position, Quaternion.identity);

        return (go);
    }
}
