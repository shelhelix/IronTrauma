using UnityEngine;

namespace IronTrauma.Submarine.ElectricalSystem {
	public abstract class BasePowerConsumer : MonoBehaviour {
		public float NeededPowerPerSecond;

		public abstract void PowerAdd();

		public abstract void PowerSuspend();

		// returns left power
		public float ConsumePower(float power, float passedTime) {
			var consumptionRate = NeededPowerPerSecond * passedTime;
			if ( power >= consumptionRate ) {
				PowerAdd();
				return consumptionRate;
			}
			PowerSuspend();
			return 0;
		}
	}
}