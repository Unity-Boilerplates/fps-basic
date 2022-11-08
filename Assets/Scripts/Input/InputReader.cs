using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Utils;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IPlayerActions
{
	// Assign delegate{} to events to initialise them with an empty delegate
	// so we can skip the null check when we use them

	// Gameplay
	public event UnityAction fireEvent = delegate { };
	public event UnityAction<Vector2> moveEvent = delegate { };
	public event UnityAction<Vector2, bool> lookEvent = delegate { };
	public event UnityAction enableMouseControlCameraEvent = delegate { };
	public event UnityAction disableMouseControlCameraEvent = delegate { };

	


	private GameInput gameInput;

	private void OnEnable()
	{
		if (gameInput == null)
		{
			gameInput = new GameInput();
			gameInput.Player.SetCallbacks(this);
		}

		EnablePlayerInput();
	}

	private void OnDisable()
	{
		DisableAllInput();
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			fireEvent.Invoke();
	}



	public void OnMove(InputAction.CallbackContext context)
	{
		moveEvent.Invoke(context.ReadValue<Vector2>());
	}

	

	public void OnLook(InputAction.CallbackContext context)
	{
		lookEvent.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
	}

	
	private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

	
	
	public void EnablePlayerInput()
	{
		gameInput.Player.Enable();
	}

	

	public void DisableAllInput()
	{
		gameInput.Player.Disable();
	}

	public bool LeftMouseDown() => Mouse.current.leftButton.isPressed;
}