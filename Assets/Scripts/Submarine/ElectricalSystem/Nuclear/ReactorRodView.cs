using UnityEngine;

namespace IronTrauma.Submarine.ElectricalSystem.Nuclear {
	public class ReactorRodView : MonoBehaviour {
		public Rigidbody  Rigidbody;
		public ReactorRod Rod;

		public MeshRenderer MeshRenderer;
		public Material     UnstableRodMaterial;

		protected void Start() {
			Rod.LeftPower = Rod.Power;
		}

		public void SwitchToUnstableView() {
			MeshRenderer.material = UnstableRodMaterial;
			Rod.LeftPower         = 0;
		}
	}
}