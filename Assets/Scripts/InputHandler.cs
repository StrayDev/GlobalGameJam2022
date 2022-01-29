using UnityEngine;
using UnityEngine.InputSystem;

public enum Player
{
    PlayerOne, PlayerTwo
}

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player player = Player.PlayerOne;
    
    private InputActions _input = default;
    private InputDevice _device = default;
    
    private void OnEnable()
    {
        // set the input
        _input = new InputActions();
        _input.Player.Enable();
        
        // set device
        _device = player == Player.PlayerOne ? GameData.Player1Device : GameData.Player2Device;
        if (_device != null)
        {
            _input.devices = new[] { _device };
        }
        // set colour 
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            var material = renderer.material;
            material.color = player == Player.PlayerOne ? Color.blue : Color.green;
        }

        // set callbacks
        _input.Player.Horizontal.performed += OnMove;
    }

    private void OnDisable()
    {
        _input.Player.Horizontal.performed += OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        SetLane(value);
    }

    private void SetLane(float value)
    {
        //5 Lanes
        //change lane
        transform.position += new Vector3(value, 0, 0);
    }
    
}
