using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpaceCntrl : MonoBehaviour
{
    [SerializeField] private GameObject[] spaceClouds;
    [SerializeField] private GameObject[] spacePortal;
    [SerializeField] private GameObject[] panets;

    private float cloudSpace = 50.0f;
    private float portalSpace = 200.0f;

    private bool startSpaceCntrl = false;

    private float destroySec = 60.0f;

    private GameObject fighter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartEnvironment(GameObject fighter)
    {
        this.fighter = fighter;

        startSpaceCntrl = true;

        StartCoroutine(DisplayClouds());
        StartCoroutine(DisplayPortal());
    }

    public void EndEnvironment()
    {
        startSpaceCntrl = false;
    }

    /**
     * DisplayClouds() - The clouds are created as the fighter traverses 
     * through space.  Based on a frequency a cloud is created and then
     * distroyed after a predetermined length of time.  A pause period
     * is set, since the coroutine requires a yield return statement.
     */
    private IEnumerator DisplayClouds()
    {
        while (startSpaceCntrl)
        {
            if (Random.Range(0, 15) == 0)
            {
                Vector2 pos = Random.insideUnitCircle * cloudSpace;
                Vector3 position = new Vector3(pos.x, 0.0f, pos.y) + fighter.transform.position;

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
                Vector3 position = new Vector3(pos.x, 0.0f, pos.y) + fighter.transform.position;

                int choice = Random.Range(0, spacePortal.Length);
                GameObject go = Instantiate(spacePortal[choice], position, Quaternion.identity);
                Destroy(go, destroySec);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
