using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace IronTrauma.Submarine.ElectricalSystem.Nuclear {
	public class ReactorSocketView : MonoBehaviour {
		public Transform MinRodPosition;
		public Transform MaxRodPosition;

		public readonly ReactorSocket Socket = new();

		ReactorRodView _insertedRodView;
		
		XRGrabInteractable XRComponent => _insertedRodView.GetComponent<XRGrabInteractable>();

		void Start() {
			Socket.OnRodReleased     += OnRodReleased;
			Socket.OnRodPowerChanged += OnRodPowerChanged;
		}

		void OnRodPowerChanged(float normalizedPower) {
			_insertedRodView.transform.position = Vector3.Lerp(MinRodPosition.position, MaxRodPosition.position, 1 - normalizedPower);
		}

		void OnRodReleased() {
			_insertedRodView.Rigidbody.constraints = RigidbodyConstraints.None;
			_insertedRodView.SwitchToUnstableView();
			XRComponent.lastSelectExited.RemoveListener(OnLastSelectedExit);
			XRComponent.firstSelectEntered.RemoveListener(OnSelected);
			_insertedRodView = null;
		}
		
		void OnTriggerEnter(Collider other) {
			var rodComponent = other.GetComponent<ReactorRodView>();
			if ( !rodComponent ) {
				return;
			}
			_insertedRodView = rodComponent;
			XRComponent.lastSelectExited.AddListener(OnLastSelectedExit);
			XRComponent.firstSelectEntered.AddListener(OnSelected);
		}

		void OnTriggerExit(Collider other) {
			var rodComponent = other.GetComponent<ReactorRodView>();
			if ( (rodComponent != _insertedRodView) || !_insertedRodView) {
				return;
			}
			OnRodReleased();
		}

		void OnLastSelectedExit(SelectExitEventArgs arg0) {
			Socket.InsertRod(_insertedRodView.Rod);
			_insertedRodView.transform.position    = MinRodPosition.position;
			_insertedRodView.transform.rotation    = Quaternion.identity;
			_insertedRodView.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		void OnSelected(SelectEnterEventArgs args) {
			_insertedRodView.Rigidbody.constraints = RigidbodyConstraints.None;
		}
	}
}