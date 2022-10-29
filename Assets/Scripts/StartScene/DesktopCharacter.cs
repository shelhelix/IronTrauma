using UnityEngine;
using UnityEngine.InputSystem;

namespace IronTrauma.StartScene {
	public class DesktopCharacter : MonoBehaviour, DesktopInput.IActionMapActions {
		DesktopInput _input;
		
		public void Init() {
			_input = new DesktopInput();
			_input.ActionMap.SetCallbacks(this);
			_input.Enable();
		}

		protected void OnDestroy() {
			_input?.Disable();
		}

		public void OnMovement(InputAction.CallbackContext context) {
			var movementVector = context.ReadValue<Vector2>();
			transform.position += new Vector3(movementVector.x, 0, movementVector.y);
		}
	}
}