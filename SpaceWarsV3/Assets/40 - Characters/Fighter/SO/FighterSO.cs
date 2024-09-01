using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fighter", menuName = "CyberWars/Fighter")]
public class FighterSO : ScriptableObject
{
    public string fighterName;

    public GameObject prefab;

    public GameObject Create(Vector3 position)
    {
        GameObject go = Instantiate(prefab, position, Quaternion.identity);

        return (go);
    }
}
