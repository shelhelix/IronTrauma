using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace IronTrauma {
	public class Reactor : MonoBehaviour {
		
		
		public TMP_Text Text;

		public List<ReactorSocket> Sockets;

		public List<PowerConsumer> Consumers;


		float MaxGenerationPotential {
			get {
				var maxPowerPerSecond = Sockets.Sum(x => x.HasRod ? Mathf.Min(x.RodPower, ReactorSocket.MaxOutputRatePerSecond) : 0f);
				return maxPowerPerSecond * Time.deltaTime;
			}
		}

		void DistributePower() {
			if ( Consumers.Count == 0 ) {
				return;
			}
			var consumedPower = ConsumePower(MaxGenerationPotential);
			ConsumeRods(consumedPower);
		}

		void ConsumeRods(float consumedPower) {
			var socketsWithRods      = Sockets.FindAll(x => x.HasRod);
			var socketsWithRodsCount = socketsWithRods.Count;
			if ( socketsWithRodsCount == 0 ) {
				if ( consumedPower > 0 ) {
					Debug.LogError("Can't consume power - no rods are available");
				}
				return;
			}
			var neededPowerFromSocket = consumedPower / socketsWithRodsCount;
			socketsWithRods.ForEach(x => x.TryUseRod(neededPowerFromSocket));
		}

		float ConsumePower(float availablePower) {
			var totalConsumedPower = 0f;
			foreach ( var consumer in Consumers ) {
				var consumedPower = consumer.ConsumePower(availablePower, Time.deltaTime);
				availablePower     -= consumedPower;
				totalConsumedPower += consumedPower;
			}
			return totalConsumedPower;
		}

		protected void Update() {
			DistributePower();
			Text.text = string.Empty;
			Sockets.ForEach(x => {
				if ( x.HasRod ) {
					Text.text += $"Rod socket status: {Mathf.FloorToInt(x.RodPower * 100)}%\n";
				} else {
					Text.text += "Rod socket status: No rod\n";
				}
			});
		}
	}
}