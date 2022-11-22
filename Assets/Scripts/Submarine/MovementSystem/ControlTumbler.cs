using UnityEngine;

namespace IronTrauma.Submarine.EngineSystem {
	public class ControlTumbler : MonoBehaviour {
		public HingeJoint Joint;
		public float      Min;
		public float      Max;

		public float CurValue => NormalizedJointPosition * (Max - Min) + Min;
		
		protected float NormalizedJointPosition => (Joint.angle - Joint.limits.min) / (Joint.limits.max - Joint.limits.min); 
	}
}