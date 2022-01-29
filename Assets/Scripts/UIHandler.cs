
using UnityEngine;
using UnityEngine.InputSystem;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _TextPrompt = default;
    [SerializeField] private GameObject _KeyboardIcon = default;
    [SerializeField] private GameObject _ControllerIcon = default;

    public void OnUIChange(InputDevice device)
    {
        var inputActions = new InputActions();

        if (inputActions.KeyboardMouseScheme.SupportsDevice(device))
        {
            SetUIToKeyboardAndMouse();
            return;
        }
        
        if (inputActions.GamePadXboxScheme.SupportsDevice(device))
        {
            SetUIToXBox();
        }
    }

    public void OnUIReset()
    {
        _TextPrompt.SetActive(true);
        _KeyboardIcon.SetActive(false);
        _ControllerIcon.SetActive(false);
    }

    private void SetUIToKeyboardAndMouse()
    {
        _KeyboardIcon.SetActive(true);
        _TextPrompt.SetActive(false);
        _ControllerIcon.SetActive(false);
    }
    
    private void SetUIToXBox()
    {
        _ControllerIcon.SetActive(true);
        _KeyboardIcon.SetActive(false);
        _TextPrompt.SetActive(false);
    }
}
