using UnityEngine;

namespace IronTrauma.Submarine.EngineSystem {
	public class RotationTumbler : MonoBehaviour {
		// TODO: change to -1 <-> 1 element
		public float NormalizedAngularThrust = 0f;
		
		public float MaxAngularSpeed = 10;

		public float CurAngularSpeed => NormalizedAngularThrust * MaxAngularSpeed;
	}
}