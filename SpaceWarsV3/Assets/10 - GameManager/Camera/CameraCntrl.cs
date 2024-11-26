using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;

    private Vector3 delta;
    private Transform fighter;
    private Vector3 movePosition;
    private Vector3 velocity = Vector3.zero;
    private float damping = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /**
     * StartEngagement() - Sets up the camera for engagement.  Only one
     * camera is used and must be position and re-position throughout
     * game play.
     */
    public void StartEngagement(Transform fighter)
    {
        transform.position = gameData.cameraEngagementPosition;
        transform.Rotate(gameData.cameraEngagementRotation);

        this.fighter = fighter;
        delta = fighter.position - transform.position;
    }

    public void PositionCameraAtIdle()
    {
        transform.position = gameData.cameraIdlePosition;
        transform.Rotate(gameData.cameraIdleRotation, Space.World);
    }

    void LateUpdate()
    {
        if (fighter != null)
        {
            movePosition = fighter.position - delta;

            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        }
    }
}
