using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpaceCntrl : MonoBehaviour
{
    [SerializeField] private GameObject[] spaceClouds;

    // Start is called before the first frame update
    void Start()
    {
        Displace(spaceClouds, 200.0f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Displace(GameObject[] items, float area, int n)
    {
        for (int i = 0; i < n; i++)
        {
            Vector2 pos = Random.insideUnitCircle;
            Vector3 position = new Vector3(pos.x, 0.0f, pos.y);
            int choice = Random.Range(0, items.Length);

            GameObject go = Instantiate(items[choice], position, Quaternion.identity);
            go.transform.SetParent(transform);
        }
    }
}
