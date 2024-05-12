using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mainUIscript : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Label gemLabel = root.Q<Label>("GemNumber");
        Debug.Log(gemLabel);
        FightingSphere playerScript = player.GetComponent<FightingSphere>();
        int gems = playerScript.stars;
        Debug.Log(gems);
        gemLabel.text = "You have " + gems + " stars!";
    }

    void Update()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Label gemLabel = root.Q<Label>("GemNumber");
        FightingSphere playerScript = player.GetComponent<FightingSphere>();
        int gems = playerScript.stars;
        gemLabel.text = "You have " + gems + " stars!";
    }
}
