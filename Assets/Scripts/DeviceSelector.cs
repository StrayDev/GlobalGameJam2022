using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using FMODUnity;

public class DeviceSelector : MonoBehaviour
{
    // events to trigger UI
    [SerializeField] private StudioEventEmitter playerJoinNoise;

    [SerializeField] private UnityEvent<InputDevice> playerOneDeviceSet = default;
    [SerializeField] private UnityEvent<InputDevice> playerTwoDeviceSet = default;
    
    [SerializeField] private UnityEvent playerOneDeviceRemoved = default;
    [SerializeField] private UnityEvent playerTwoDeviceRemoved = default;
    
    [SerializeField] private UnityEvent bothDevicesSelected = default;

    private InputActions _inputActions = default;
    
    void OnEnable()
    {
        // create the input & enable it
        _inputActions = new InputActions();
        _inputActions.Menu.Enable();
        
        _inputActions.Menu.SetDevice.performed += OnSetDevice;
        _inputActions.Menu.RemoveDevice.performed += OnRemoveDevice;
    }    

    private void OnDisable()
    {
        _inputActions.Menu.SetDevice.performed -= OnSetDevice;
        _inputActions.Menu.RemoveDevice.performed -= OnRemoveDevice;
    }

    private void OnSetDevice(InputAction.CallbackContext context)
    {
        // get the device used
        var device = context.control.device;

        // try set player 1
        if (GameData.Player1Device == null && GameData.Player2Device != device)
        {
            GameData.Player1Device = device;
            playerJoinNoise.Play();
            playerOneDeviceSet.Invoke(device);
            return;
        }
        
        // try set player 2
        if (GameData.Player2Device == null && GameData.Player1Device != device)
        {
            GameData.Player2Device = device;
            playerJoinNoise.Play();
            playerTwoDeviceSet.Invoke(device);
        }
        
        // send ready signal
        if (GameData.Player1Device != null && GameData.Player2Device != null)
        {
            bothDevicesSelected.Invoke();
        }
    }

    private void OnRemoveDevice(InputAction.CallbackContext context)
    {
        // get the device used
        var device = context.control.device;

        // remove device
        if (GameData.Player1Device == device)
        {
            playerOneDeviceRemoved.Invoke();
            GameData.Player1Device = null;
            return;
        }

        if (GameData.Player2Device == device)
        {
            playerTwoDeviceRemoved.Invoke();
            GameData.Player2Device = null;
        }
    }
}
