using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class mainMenuScript : MonoBehaviour
{
    void Update()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement startGameButton = root.Q<Button>("NewGameButton");
        startGameButton.RegisterCallback<ClickEvent>((evt) => StartGame());
    }

    void StartGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
}
