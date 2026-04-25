using UnityEngine;
using UnityEngine.UI;

public class ControlSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button switchButton;

    void Start()
    {
        joystick.gameObject.SetActive(false);
        switchButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        playerMovement.ToggleControl();
        joystick.gameObject.SetActive(playerMovement.IsUsingJoystick);
    }
}
