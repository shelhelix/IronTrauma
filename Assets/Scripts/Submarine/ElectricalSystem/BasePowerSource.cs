using System.Collections.Generic;
using UnityEngine;

namespace IronTrauma.Submarine.ElectricalSystem {
	public abstract class BasePowerSource : MonoBehaviour {
		public List<BasePowerConsumer> Consumers;
		
		public void DistributePower(float timeSpanSec) {
			var maxPotential  = GetMaxGenerationPotential(timeSpanSec);
			var consumedPower = ConsumePower(maxPotential, timeSpanSec);
			GeneratePower(consumedPower);
		}
		
		protected abstract float GetMaxGenerationPotential(float timeSpanSec);

		protected abstract void GeneratePower(float neededPower);
		
		float ConsumePower(float availablePower, float timeSpanSec) {
			var totalConsumedPower = 0f;
			foreach ( var consumer in Consumers ) {
				var consumedPower = consumer.ConsumePower(availablePower, timeSpanSec);
				availablePower     -= consumedPower;
				totalConsumedPower += consumedPower;
			}
			return totalConsumedPower;
		}
	}
}