using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class dialogueNPC : MonoBehaviour
{
    public GameObject npc;
    public GameObject player;
    public Camera orbitCamera;
    public InputAction openDialogueButton;
    public bool dialogueOpen = false;

    public void OnEnable()
    {
        openDialogueButton.Enable();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
    }

    public void OnDisable()
    {
        openDialogueButton.Disable();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = orbitCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "npc1")
                {
                    if (dialogueOpen != true)
                    {
                        OpenInventory();
                        dialogueOpen = true;
                    }
                }
            }
        }
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement closeButton = root.Q<Button>("CloseDialogue");
        closeButton.RegisterCallback<ClickEvent>((evt) => CloseInventory());
    }

    public void OpenInventory()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.Flex;
        pointMoveAnimation playerScript = player.GetComponent<pointMoveAnimation>();
        playerScript.movementBlocked = true;
    }

    public void CloseInventory()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
        dialogueOpen = false;
        pointMoveAnimation playerScript = player.GetComponent<pointMoveAnimation>();
        playerScript.movementBlocked = false;
    }
}
