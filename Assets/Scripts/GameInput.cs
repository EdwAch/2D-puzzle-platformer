using UnityEngine;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }
    
    private InputActions inputActions;
    
    
    private void Awake() {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();
    }

    public bool IsRightActionPressed() {
        return inputActions.Player.Right.IsPressed();
    }
    
    public bool IsLeftActionPressed() {
        return inputActions.Player.Left.IsPressed();
    }
    
    public bool IsJumpActionPressed() {
        return inputActions.Player.Jump.WasPressedThisFrame();
    }

    public void Dispose() {
        inputActions.Dispose();
    }
}