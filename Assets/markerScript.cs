using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class markerScript : MonoBehaviour
{
    public Camera orbitCamera;
    public GameObject markerPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = orbitCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name != "npc1")
                {
                    NavMeshSurface navMesh = hit.collider.gameObject.GetComponent<NavMeshSurface>();
                    if (navMesh != null)
                    {
                        // Debug.Log(hit.point);
                        GameObject marker = Instantiate(
                            markerPrefab,
                            new Vector3(hit.point.x, hit.point.y, hit.point.z),
                            Quaternion.identity
                        );
                        Destroy(marker, 0.35f);
                    }
                }
                else { }
            }
        }
    }
}
