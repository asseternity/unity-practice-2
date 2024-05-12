using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    public bool fired = false;

    void Start()
    {
        if (fired == true)
        {
            Destroy(gameObject, 2f);
        }
    }

    void Update() { }
}
