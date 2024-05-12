using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class inventory2 : MonoBehaviour
{
    public InputAction openInventoryButton;
    public bool inventoryOpen = false;

    public void OnEnable()
    {
        openInventoryButton.Enable();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
    }

    public void OnDisable()
    {
        openInventoryButton.Disable();
    }

    public void Update()
    {
        if (openInventoryButton.triggered)
        {
            if (inventoryOpen == true)
            {
                CloseInventory();
                inventoryOpen = false;
            }
            else
            {
                OpenInventory();
                inventoryOpen = true;
            }
        }
    }

    public void OpenInventory()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.Flex;
    }

    public void CloseInventory()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
    }
}
