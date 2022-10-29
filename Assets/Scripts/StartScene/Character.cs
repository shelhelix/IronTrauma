using IronTrauma.Utils;
using UnityEngine;

namespace IronTrauma.StartScene {
	public class Character :  MonoBehaviour {
		public GameObject XrRoot;
		public DesktopCharacter DesktopRoot;

		public void Init() {
			XrRoot.SetActive(XrDetector.IsXrActive);
			DesktopRoot.gameObject.SetActive(!XrDetector.IsXrActive);
			if ( !XrDetector.IsXrActive ) {
 				DesktopRoot.Init();
			}
		}
	}
}