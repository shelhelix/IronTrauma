using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace IronTrauma.Common {
	public class Player : MonoBehaviour {
		[Header("Settings")]
		public MovementType SelectedMovementType;
		public ControllerTurnType SelectedTurnType;
		
		
		[Header("Movement")]
		public TeleportationProvider             TeleportationProvider;
		public ActionBasedContinuousMoveProvider ContinuousMoveProvider;

		[Header("Turn")]
		public ActionBasedSnapTurnProvider SnapTurnProvider;
		public ActionBasedContinuousTurnProvider ContinuousTurnProvider;

		public void Start() {
			TeleportationProvider.enabled  = SelectedMovementType == MovementType.Teleportation;
			ContinuousMoveProvider.enabled = SelectedMovementType == MovementType.Locomotion;
			
			SnapTurnProvider.enabled       = SelectedTurnType == ControllerTurnType.Snap;
			ContinuousTurnProvider.enabled = SelectedTurnType == ControllerTurnType.Continuous;
		}
	}
}