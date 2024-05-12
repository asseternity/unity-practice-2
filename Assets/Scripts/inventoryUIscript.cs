using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class inventoryUIscript : MonoBehaviour
{
    public InputAction inventoryButton;
    bool active = false;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inventoryButton.Enable();
    }

    private void OnDisable()
    {
        inventoryButton.Disable();
    }

    void Update()
    {
        float input = inventoryButton.ReadValue<float>();
        if (input != 0f)
        {
            if (active == false)
            {
                gameObject.SetActive(true);
                active = true;
            }
            else if (active == true)
            {
                gameObject.SetActive(false);
                active = false;
            }
        }
    }
}
