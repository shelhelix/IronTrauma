using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace IronTrauma.Submarine.ElectricalSystem.Nuclear {
	public class Reactor : BasePowerSource {
		public TMP_Text Text;

		public List<ReactorSocketView> SocketsView;
		
		List<ReactorSocket> _sockets;
		
		protected override float GetMaxGenerationPotential(float timeSpanSec) => _sockets.Sum(x => x.MaxOutput) * timeSpanSec;

		protected override void GeneratePower(float neededPower) {
			var socketsWithRods      = _sockets.FindAll(x => x.HasRod);
			var socketsWithRodsCount = socketsWithRods.Count;
			if ( socketsWithRodsCount == 0 ) {
				if ( neededPower > 0 ) {
					Debug.LogError("Can't consume power - no rods are available");
				}
				return;
			}
			var neededPowerFromSocket = neededPower / socketsWithRodsCount;
			socketsWithRods.ForEach(x => x.UseRod(neededPowerFromSocket));
		}

		void Start() {
			_sockets = SocketsView.Select(x => x.Socket).ToList();
		}

		protected void Update() {
			DistributePower(Time.deltaTime);
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