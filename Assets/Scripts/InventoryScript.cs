using UnityEditor.UIElements;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public GameObject player;
    int stars;

    void Update()
    {
        FightingSphere playerScript = player.GetComponent<FightingSphere>();
    }
}
