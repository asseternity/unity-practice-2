using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            Destroy(gameObject, 0.3f);
        }
    }
}
