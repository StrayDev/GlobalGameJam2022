using UnityEngine;
using UnityEngine.InputSystem;

public enum Player
{
    PlayerOne, PlayerTwo
}

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player player = Player.PlayerOne;
    [SerializeField] private bool moved = false;
    [SerializeField] private InfiniteLanes _laneScript;

    private InputActions _input = default;
    private InputDevice _device = default;
    
    private void OnEnable()
    {
        // set the input
        _input = new InputActions();
        _input.Player.Enable();
        
        // set device
        _device = player == Player.PlayerOne ? GameData.Player1Device : GameData.Player2Device;
        _input.devices = new[] { _device };
        
        // set colour 
        var material = GetComponent<Renderer>().material;
        material.color = player == Player.PlayerOne ? Color.blue : Color.green;

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
        Debug.Log("X Axis Value: "+value.ToString());
        _laneScript.SetLane(value);
    }
    
    public bool Moved()
    {
        return moved;
    }
    public void setMoved(bool _moved)
    {
        moved = _moved;
    }
}
