using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpaceCntrl : MonoBehaviour
{
    [SerializeField] private Transform clickingPlane;
    [SerializeField] private GameObject[] spaceClouds;
    [SerializeField] private GameObject[] spacePortal;
    [SerializeField] private GameObject[] panets;

    private float cloudSpace = 500.0f;
    private float portalSpace = 200.0f;
    private float panetsSpace = 100.0f;

    private bool startSpaceCntrl = false;

    private float destroySec = 120.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartEnvironment()
    {
        startSpaceCntrl = true;

        StartCoroutine(DisplayClouds());
        StartCoroutine(DisplayPortal());
        //StartCoroutine(DisplayPanets());
    }

    public void EndEnvironment()
    {
        startSpaceCntrl = false;
    }

    private IEnumerator DisplayClouds()
    {
        while (startSpaceCntrl)
        {
            if (Random.Range(0, 10) == 0)
            {
                Vector2 pos = Random.insideUnitCircle * cloudSpace;
                Vector3 position = new Vector3(pos.x, 0.0f, pos.y) + clickingPlane.position;

                int choice = Random.Range(0, spaceClouds.Length);
                GameObject go = Instantiate(spaceClouds[choice], position, Quaternion.identity);
                Destroy(go, destroySec);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator DisplayPortal()
    {
        while (startSpaceCntrl)
        {
            if (Random.Range(0, 15) == 0)
            {
                Vector2 pos = Random.insideUnitCircle * portalSpace;
                Vector3 position = new Vector3(pos.x, 0.0f, pos.y) + clickingPlane.position;

                int choice = Random.Range(0, spacePortal.Length);
                GameObject go = Instantiate(spacePortal[choice], position, Quaternion.identity);
                Destroy(go, destroySec);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator DisplayPanets()
    {
        while (startSpaceCntrl)
        {
            if (Random.Range(0, 10) == 0)
            {
                Vector2 Rp = Random.insideUnitCircle;
                Vector3 R = new Vector3(Rp.x, 0.0f, Rp.y) * panetsSpace;
                Vector3 C = clickingPlane.position;
                Vector3 position = (R - C).normalized * panetsSpace;
                position.y = -1000.0f;

                Debug.Log($"Position: {position}");

                int choice = Random.Range(0, panets.Length);
                GameObject go = Instantiate(panets[choice], position, Quaternion.identity);
                Destroy(go, destroySec);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
