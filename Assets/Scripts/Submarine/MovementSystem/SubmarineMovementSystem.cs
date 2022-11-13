using UnityEngine;

namespace IronTrauma.Submarine.EngineSystem {
	public class SubmarineMovementSystem : MonoBehaviour {
		public BallastTank BallastTank;
		public Rigidbody  SubmarineRigidbody;

		public void FixedUpdate() {
			SubmarineRigidbody.AddForce(SubmarineRigidbody.transform.up * BallastTank.PushPower, ForceMode.Impulse);
		}
	}
}