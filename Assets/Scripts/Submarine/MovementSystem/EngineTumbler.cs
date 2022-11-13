using UnityEngine;

namespace IronTrauma.Submarine.EngineSystem {
	public class EngineTumbler : MonoBehaviour {
		// TODO: change to -1 <-> 1 element
		public float NormalizedEnginePower = 0f;
		
		public float MaxEngineThrust = 10;

		public float CurThrust => NormalizedEnginePower * MaxEngineThrust;
	}
}