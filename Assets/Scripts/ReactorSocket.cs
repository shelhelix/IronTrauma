
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace IronTrauma {
	public class ReactorSocket : MonoBehaviour {
		public Transform InsertedRodRoot;

		public bool HasRod => _activeRod && _isInserted;

		ReactorRod _activeRod;
		bool       _isInserted;

		XRGrabInteractable XRComponent => _activeRod.GetComponent<XRGrabInteractable>();
		
		void OnTriggerEnter(Collider other) {
			var rodComponent = other.GetComponent<ReactorRod>();
			if ( !rodComponent ) {
				return;
			}
			_activeRod = rodComponent;
			XRComponent.lastSelectExited.AddListener(OnLastSelectedExit);
			XRComponent.firstSelectEntered.AddListener(OnSelected);
		}

		void OnTriggerExit(Collider other) {
			var rodComponent = other.GetComponent<ReactorRod>();
			if ( (rodComponent != _activeRod) || !_activeRod) {
				return;
			}
			_activeRod.Rigidbody.constraints = RigidbodyConstraints.None;
			XRComponent.lastSelectExited.RemoveListener(OnLastSelectedExit);
			XRComponent.firstSelectEntered.AddListener(OnSelected);
			_activeRod  = null;
			_isInserted = false;
		}

		void OnLastSelectedExit(SelectExitEventArgs arg0) {
			_isInserted                      = true;
			_activeRod.transform.position    = InsertedRodRoot.position;
			_activeRod.transform.rotation    = Quaternion.identity;
			_activeRod.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		void OnSelected(SelectEnterEventArgs args) {
			_activeRod.Rigidbody.constraints = RigidbodyConstraints.None;
			
		}
	}
}