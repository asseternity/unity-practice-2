using UnityEngine;
using UnityEditor.UIElements;

public class InventoryScript : MonoBehaviour
{
    public GameObject player;
    int stars;
    void Update()
    {
        FightingSphere playerScript = player.GetComponent<FightingSphere>();

    }
}
