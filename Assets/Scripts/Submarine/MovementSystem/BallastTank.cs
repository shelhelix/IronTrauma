using UnityEngine;

namespace IronTrauma.Submarine.EngineSystem {
	public class BallastTank : MonoBehaviour {
		public float NormalizedFilled  = 0.5f;
		public float AbsolutePushPower = 10;

		// half filled ballast tank do nothing
		public float PushPower => (0.5f - NormalizedFilled) * AbsolutePushPower;
	}
}