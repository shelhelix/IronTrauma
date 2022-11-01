using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IronTrauma {
	public class Reactor : MonoBehaviour {
		public Light LampIndicator;
		
		public List<ReactorSocket> Sockets;

		protected void Update() {
			LampIndicator.color = Sockets.Any(x => x.HasRod) ? Color.green : Color.red;
		}
	}
}