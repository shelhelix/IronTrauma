using UnityEngine;

namespace IronTrauma {
	public class ReactorRod : MonoBehaviour {
		public Rigidbody Rigidbody;
		public float     Power;
		public float     LeftPower;

		public MeshRenderer MeshRenderer;
		public Material     UnstableRodMaterial;

		protected void Start() {
			LeftPower = Power;
		}

		public void MakeUnstable() {
			MeshRenderer.material = UnstableRodMaterial;
		}
	}
}