using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    public bool fired = false;
    // Start is called before the first frame update
    void Start()
    {
        if (fired == true) {
            Destroy(gameObject, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
