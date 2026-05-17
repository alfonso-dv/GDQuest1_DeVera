using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Localization;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private LocalizedString dialogText;

    private bool canInteract = false;
    private InputAction interactAction;

    private void Start()
    {
        this.interactAction = InputSystem.actions.FindAction("Interact");
        this.dialogBox.SetActive(false);
    }

    private void Update()
    {
        if (this.canInteract && this.interactAction.WasPressedThisFrame())
        {
            ToggleDialogBox();
        }
    }

    private void ToggleDialogBox()
    {
        bool isActive = this.dialogBox.activeInHierarchy;
        this.dialogBox.SetActive(!isActive);

        if (!isActive)
        {
            UIDocument uiDocument = this.dialogBox.GetComponent<UIDocument>();
            Label label = uiDocument.rootVisualElement.Q<Label>();

            label.text = this.dialogText.GetLocalizedString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        this.canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        this.canInteract = false;
        this.dialogBox.SetActive(false);
    }
}