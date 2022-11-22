using UnityEngine;

namespace IronTrauma.Submarine.EngineSystem {
	public class BallastTankTumbler : MonoBehaviour {
		public HingeJoint Joint;
		
		public float NormalizedFilled  = 0.5f;
		public float AbsolutePushPower = 1;

		// half filled ballast tank do nothing
		public float PushPower => (0.5f - NormalizedJointPosition) * AbsolutePushPower;

		float NormalizedJointPosition => (Joint.angle - Joint.limits.min) / (Joint.limits.max - Joint.limits.min); 
		
	}
}