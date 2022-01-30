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
    [SerializeField] private bool canSwap = false;
    
    [SerializeField] private UnityEvent<Player> onControlSwapEvent = default;

    private PlayerTrain train;

    public Player getPlayerID() => player;

    private void OnEnable()
    {
        // set the input
        _input = new InputActions();
        _input.Player.Enable();
        
        // set device
        _device = player == Player.PlayerOne ? GameData.Player1Device : GameData.Player2Device;
        
        if (GameData.Player1Device != null) {
            _input.devices = new[] { _device };
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
            canSwap = true;
            _input.Player.SwapControl.performed += OnSwap;
        }

        _laneScript = this.GetComponent<LaneScript>();
        train = GetComponent<PlayerTrain>();
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        if (!GameData.isMidSwap()) {
            var value = context.ReadValue<float>();
            //Debug.Log("X Axis Value: "+value.ToString());
            _laneScript.SetLane(value);
        }
    }

    public void OnSwap(InputAction.CallbackContext context)
    {
        if (!canSwap) return;
        SwapControl(player);
        onControlSwapEvent.Invoke(player);
    }

    public void SwapControl(Player playerID)
    {
        canSwap = false;
        GameData.setMidSwap(true);
        train.SetPhysical(false);

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

    public void OnSwapComplete()
    {
        GameData.setMidSwap(false);
        if (!canMove)
        {
            canSwap = true;
            train.SetPhysical(true);
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
