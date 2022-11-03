using System;
using UnityEngine;

namespace IronTrauma {
	public class ReactorSocket {
		public const float MaxOutputRatePerSecond = 1f;

		public event Action        OnRodReleased;
		public event Action<float> OnRodPowerChanged;

		ReactorRod _insertedRod;

		public bool HasRod => _insertedRod != null;

		public float NormalizedRodPower => HasRod ? _insertedRod.LeftPower / _insertedRod.Power : 0f;

		public float MaxOutput => HasRod ? Mathf.Min(MaxOutputRatePerSecond, _insertedRod.LeftPower) : 0f;
		
		public void InsertRod(ReactorRod rod) {
			_insertedRod = rod;
		}

		public void ReleaseRod() {
			_insertedRod = null;
			OnRodReleased?.Invoke();
		}

		public void UseRod(float consumedPower) {
			if ( !HasRod ) {
				Debug.LogWarning("Can't consume power. No rod in the socket");
				return;
			}
			if ( consumedPower > MaxOutputRatePerSecond ) {
				Debug.LogWarning("Can't consume more than max output. Clamped");
				consumedPower = MaxOutputRatePerSecond;
			}
			_insertedRod.LeftPower -= consumedPower;
			OnRodPowerChanged?.Invoke(NormalizedRodPower);
			if ( _insertedRod.LeftPower <= 0 ) {
				_insertedRod.LeftPower = 0;
				ReleaseRod();
			}
		}
	}
}