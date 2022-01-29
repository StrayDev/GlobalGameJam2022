using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum Player
{
    PlayerOne, PlayerTwo
}

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player player = Player.PlayerOne;
    [SerializeField] private bool moved = false;
    [SerializeField] private LaneScript _laneScript;

    private InputActions _input = default;
    private InputDevice _device = default;

    [SerializeField] private bool canMove = false;
    [SerializeField] private UnityEvent<Player> onControlSwapEvent = default;

    private void OnEnable()
    {
        // set the input
        _input = new InputActions();
        _input.Player.Enable();
        
        // set device
        _device = player == Player.PlayerOne ? GameData.Player1Device : GameData.Player2Device;
        _input.devices = new[] { _device };
        
        // set colour 
        foreach(var renderer in GetComponentsInChildren<Renderer>())
        {
            var material = renderer.material;
            material.color = player == Player.PlayerOne ? Color.blue : Color.green;
        }

        // set callbacks

        if (player == Player.PlayerOne)
        {
            // active control
            canMove = true;
            _input.Player.Horizontal.performed += OnMove;
        }
        else
        {
            // inactive
            _input.Player.SwapControl.performed += OnSwap;
        }
    }

    private void OnDisable()
    {
        _input.Player.Horizontal.performed += OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        //Debug.Log("X Axis Value: "+value.ToString());
        _laneScript.SetLane(value);
    }

    public void OnSwap(InputAction.CallbackContext context)
    {
        SwapControl(player);
        onControlSwapEvent.Invoke(player);
    }

    public void SwapControl(Player playerID)
    {
        Debug.Log(player);
        if (!canMove)
        {
            canMove = true;
            _input.Player.SwapControl.performed -= OnSwap;
            _input.Player.Horizontal.performed += OnMove;
        }
        else
        {
            canMove = false;
            _input.Player.SwapControl.performed += OnSwap;
            _input.Player.Horizontal.performed -= OnMove;
        }
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
