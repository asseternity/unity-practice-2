using UnityEngine;
using UnityEngine.UIElements;

public class inventoryUInew : MonoBehaviour
{
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }
}
