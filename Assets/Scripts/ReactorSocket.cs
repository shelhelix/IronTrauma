
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace IronTrauma {
	public class ReactorSocket : MonoBehaviour {
		public const float MaxOutputRatePerSecond = 1f;
		
		public Transform MinRodPosition;
		public Transform MaxRodPosition;


		public bool HasRod => _activeRod && _isInserted;

		public float RodPower => _activeRod.LeftPower / _activeRod.Power;

		ReactorRod _activeRod;
		bool       _isInserted;

		XRGrabInteractable XRComponent => _activeRod.GetComponent<XRGrabInteractable>();

		public void TryUseRod(float consumedPower) {
			if ( !HasRod ) {
				return;
			}
			if ( consumedPower > MaxOutputRatePerSecond ) {
				Debug.LogWarning("Can't consume more than max output. Clamped");
				consumedPower = MaxOutputRatePerSecond;
			}
			_activeRod.LeftPower          -= consumedPower;
			_activeRod.transform.position =  Vector3.Lerp(MinRodPosition.position, MaxRodPosition.position, 1 - RodPower);
			if ( _activeRod.LeftPower <= 0 ) {
				var rod = _activeRod;
				RemoveRod();
				var throwDirection = (MinRodPosition.position - MaxRodPosition.position).normalized;
				rod.Rigidbody.AddForce(throwDirection * 10, ForceMode.Impulse);
				rod.MakeUnstable();
			}
		}
		
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
			RemoveRod();
		}

		void RemoveRod() {
			_activeRod.Rigidbody.constraints = RigidbodyConstraints.None;
			XRComponent.lastSelectExited.RemoveListener(OnLastSelectedExit);
			XRComponent.firstSelectEntered.RemoveListener(OnSelected);
			_activeRod  = null;
			_isInserted = false;
		}

		void OnLastSelectedExit(SelectExitEventArgs arg0) {
			_isInserted                      = true;
			_activeRod.transform.position    = MinRodPosition.position;
			_activeRod.transform.rotation    = Quaternion.identity;
			_activeRod.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		void OnSelected(SelectEnterEventArgs args) {
			_activeRod.Rigidbody.constraints = RigidbodyConstraints.None;
		}
	}
}