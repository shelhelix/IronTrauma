using UnityEngine;

namespace IronTrauma.Submarine.SonarSystem {
	public static class SonarUtils {
		public static Vector3 CalculateSonarObjectPosition(Vector3 objectPos, Vector3 sonarSourceCenter, float sonarScale, Vector3 sonarCenterView) => (objectPos - sonarSourceCenter) * sonarScale + sonarCenterView;
	}
}