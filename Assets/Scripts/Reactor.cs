using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace IronTrauma {
	public class Reactor : MonoBehaviour {
		public TMP_Text Text;

		public List<ReactorSocketView> SocketsView;

		public List<PowerConsumer> Consumers;

		List<ReactorSocket> _sockets;

		float MaxGenerationPotential => _sockets.Sum(x => x.MaxOutput) * Time.deltaTime;

		void Start() {
			_sockets = SocketsView.Select(x => x.Socket).ToList();
		}

		void DistributePower() {
			var consumedPower = ConsumePower(MaxGenerationPotential);
			ConsumeRods(consumedPower);
		}

		void ConsumeRods(float consumedPower) {
			var socketsWithRods      = _sockets.FindAll(x => x.HasRod);
			var socketsWithRodsCount = socketsWithRods.Count;
			if ( socketsWithRodsCount == 0 ) {
				if ( consumedPower > 0 ) {
					Debug.LogError("Can't consume power - no rods are available");
				}
				return;
			}
			var neededPowerFromSocket = consumedPower / socketsWithRodsCount;
			socketsWithRods.ForEach(x => x.UseRod(neededPowerFromSocket));
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
			SocketsView.ForEach(x => {
				if ( x.Socket.HasRod ) {
					Text.text += $"Rod socket status: {Mathf.FloorToInt(x.Socket.NormalizedRodPower * 100)}%\n";
				} else {
					Text.text += "Rod socket status: No rod\n";
				}
			});
		}
	}
}