using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pointMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent = null;
    public Camera orbitCamera;

    void Update()
    {
        // If the player clicks on the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // we want to fire a ray toward where we place the mouse cursor
            // So we need to store the camera to access the ability to screen point a ray using the mouse position
            Ray ray = orbitCamera.ScreenPointToRay(Input.mousePosition);
            // We also need to store the location “where we clicked” into a “RaycastHit” variable called hit
            RaycastHit hit;
            // We then check to see if hit has any data for the player to move to
            if (Physics.Raycast(ray, out hit))
            {
                //  If it does, we then need to call
                // a function within our nav mesh agent variable, _agent.SetDestination()
                // and pass in the hit variable as hit.point.
                _agent.SetDestination(hit.point);
            }
        }
    }
}
