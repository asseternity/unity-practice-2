using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pointMoveAnimation : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent = null;
    public bool movementBlocked = false;
    public Camera orbitCamera;

    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private Rigidbody m_rigidBody = null;
    private float currentMoveSpeed;

    private void Awake()
    {
        if (!m_animator)
        {
            gameObject.GetComponent<Animator>();
        }
        if (!m_rigidBody)
        {
            gameObject.GetComponent<Animator>();
        }
    }

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
                if (hit.collider.name != "npc1" && movementBlocked == false)
                {
                    //  If it does, we then need to call
                    // a function within our nav mesh agent variable, _agent.SetDestination()
                    // and pass in the hit variable as hit.point.
                    _agent.SetDestination(hit.point);
                }
                if (hit.collider.name != "Terrain" && hit.transform)
                {
                    CalculatePathLength(hit.transform);
                }
            }
        }
        // get our speed
        currentMoveSpeed = _agent.velocity.magnitude;
        m_animator.SetFloat("MoveSpeed", currentMoveSpeed);
        // Check if the agent is moving
        if (_agent.hasPath && _agent.remainingDistance > _agent.stoppingDistance)
        {
            // Agent is moving
            // Debug.Log("Agent is moving");
            m_animator.SetFloat("MoveSpeed", currentMoveSpeed);
        }
        else
        {
            // Agent is not moving
            // Debug.Log("Agent is not moving");
            m_animator.SetFloat("MoveSpeed", currentMoveSpeed);
            m_animator.SetBool("Grounded", true);
        }
    }

    public float CalculatePathLength(Transform target)
    {
        Debug.Log("CalculatePathLength called!");
        NavMeshPath Path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, target.position, _agent.areaMask, Path))
        {
            Debug.Log("NavMesh.CalculatePath == true!");
            float distance = Vector3.Distance(transform.position, Path.corners[0]);
            Debug.Log(Path.corners);
            for (int i = 1; i < Path.corners.Length; i++)
            {
                distance += Vector3.Distance(Path.corners[i - 1], Path.corners[i]);
            }
            Debug.Log(distance);
            return distance;
        }
        else
        {
            return 0;
        }
    }
}
