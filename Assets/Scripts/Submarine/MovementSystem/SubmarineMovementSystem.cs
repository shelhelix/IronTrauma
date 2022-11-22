using UnityEngine;

namespace IronTrauma.Submarine.EngineSystem {
	public class SubmarineMovementSystem : MonoBehaviour {
		public Rigidbody  SubmarineRigidbody;

		public BallastTankTumbler     BallastTankTumbler;
		public RotationTumbler RotationTumbler;
		public EngineTumbler   EngineTumbler;
		
		public void FixedUpdate() {
			SubmarineRigidbody.AddForce(SubmarineRigidbody.transform.up * BallastTankTumbler.PushPower, ForceMode.Impulse);
			SubmarineRigidbody.AddForce(SubmarineRigidbody.transform.forward * EngineTumbler.CurThrust, ForceMode.Impulse);
			RotateSubmarine();
		}

		void RotateSubmarine() {
			var angleSpeedInRad = RotationTumbler.CurAngularSpeed * Mathf.Deg2Rad;
			SubmarineRigidbody.angularVelocity = new Vector3(0, angleSpeedInRad, 0);
		}
	}
}