using IronTrauma.Submarine.ElectricalSystem;
using UnityEngine;

namespace IronTrauma {
	public class Lamp : BasePowerConsumer {
		public Light Light;
		
		public override void PowerAdd() {
			Light.enabled = true;
		}

		public override void PowerSuspend() {
			Light.enabled = false;
		}
	}
}