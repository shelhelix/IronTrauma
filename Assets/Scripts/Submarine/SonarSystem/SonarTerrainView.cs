using UnityEngine;

namespace IronTrauma.Submarine.SonarSystem {
	public class SonarTerrainView : MonoBehaviour {
		public Terrain WorldTerrain;
		
		[Header("mini terrain object")]
		public Terrain SonarMiniTerrain;
		public TerrainCollider SonarMiniTerrainCollider;
		
		public void Init() {
			var terrainDataClone = TerrainDataCloner.Clone(WorldTerrain.terrainData);
			SonarMiniTerrain.terrainData         = terrainDataClone;
			SonarMiniTerrainCollider.terrainData = terrainDataClone;
		}
		
		public void OnSonarUpdate(float sonarScale, Vector3 sonarCenter, Vector3 sonarViewCenter) {
			// Update position
			SonarMiniTerrain.transform.position = SonarUtils.CalculateSonarObjectPosition(WorldTerrain.transform.position, sonarCenter, sonarScale, sonarViewCenter);
			// Update scale 
			SonarMiniTerrain.terrainData.size = WorldTerrain.terrainData.size * sonarScale;
		}

		public bool IsTerrainObject(Collider collider) => collider is TerrainCollider;
	}
}