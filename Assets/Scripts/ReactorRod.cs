using UnityEngine;

namespace IronTrauma {
	public class ReactorRod : MonoBehaviour {
		public Rigidbody Rigidbody;
		public float     Power;
		public float     LeftPower;

		protected void Start() {
			LeftPower = Power;
		}
	}
}