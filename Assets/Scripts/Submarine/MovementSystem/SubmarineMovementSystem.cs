using UnityEngine;

namespace IronTrauma.Submarine.EngineSystem {
	public class SubmarineMovementSystem : MonoBehaviour {
		public Rigidbody  SubmarineRigidbody;

		public ControlTumbler BallastTankControlTumbler;
		public ControlTumbler RotationTumbler;
		public ControlTumbler EngineTumbler;
		
		public void FixedUpdate() {
			print($"UP: {BallastTankControlTumbler.CurValue}, Forward: {EngineTumbler.CurValue}, rotation: {RotationTumbler.CurValue}");
			
			SubmarineRigidbody.AddForce(SubmarineRigidbody.transform.up * BallastTankControlTumbler.CurValue, ForceMode.Impulse);
			SubmarineRigidbody.AddForce(SubmarineRigidbody.transform.forward * EngineTumbler.CurValue, ForceMode.Impulse);
			RotateSubmarine();
		}

		void RotateSubmarine() {
			var angleSpeedInRad = RotationTumbler.CurValue * Mathf.Deg2Rad;
			SubmarineRigidbody.angularVelocity = new Vector3(0, angleSpeedInRad, 0);
		}
	}
}