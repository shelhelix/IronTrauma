using UnityEngine;

namespace IronTrauma.StartScene {
	public class StartSceneStarter : MonoBehaviour {
		public Character Character;
		
		public void Start() {
			Character.Init();
		}
	}
}