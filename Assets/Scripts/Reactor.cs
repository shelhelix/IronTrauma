using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace IronTrauma {
	public class Reactor : MonoBehaviour {
		public Light LampIndicator;

		public TMP_Text Text;

		public List<ReactorSocket> Sockets;

		protected void Update() {
			LampIndicator.color = Sockets.Any(x => x.HasRod) ? Color.green : Color.red;
			Text.text           = string.Empty;
			Sockets.ForEach(x => {
				x.TryUseRod(Time.deltaTime);
				if ( x.HasRod ) {
					Text.text += $"Rod socket status: {x.RodPower}\n";
				} else {
					Text.text += "Rod socket status: No rod\n";
				}
			});
		}
	}
}