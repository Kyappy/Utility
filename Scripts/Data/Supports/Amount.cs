using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data.Events;

namespace Utility.Data.Supports {
	/// <summary>
	/// Amount class.
	/// </summary>
	[Serializable] public class Amount {
		#region public events
		/// <summary>
		/// Full amount value event.
		/// </summary>
		public event AmountModifiedEventHandler FullEvent;
		/// <summary>
		/// Empty amount value event.
		/// </summary>
		public event AmountModifiedEventHandler EmptyEvent;
		/// <summary>
		/// Modify amount value event.
		/// </summary>
		public event AmountModifiedEventHandler ModifyValueEvent;
		/// <summary>
		/// Increase amount value event.
		/// </summary>
		public event AmountModifiedEventHandler IncreaseEvent;
		/// <summary>
		/// Decrease amount value event.
		/// </summary>
		public event AmountModifiedEventHandler DecreaseEvent;
		#endregion

		#region public accessors
		/// <summary>
		/// Gets minimum amount value.
		/// </summary>
		public float Minimum { get { return minimum; } set { SetMinimum(value); } }
		/// <summary>
		/// Gets maximum amount value.
		/// </summary>
		public float Maximum { get { return maximum; } set { SetMaximum(value); } }
		/// <summary>
		/// Gets actual amount value.
		/// </summary>
		public float Value { get { return value; } set { this.value = ClampValue(value); } }
		/// <summary>
		/// Gets the remaining value.
		/// </summary>
		public float RemainingValue { get { return value - Minimum; } }
		/// <summary>
		/// Gets the missing value.
		/// </summary>
		public float MissingValue { get { return Maximum - value; } }
		/// <summary>
		/// Gets the amount range value.
		/// </summary>
		public float RangeValue { get { return Maximum - Minimum; } }
		#endregion

		#region private fields
		/// <summary>
		/// The minimum amount value.
		/// </summary>
		[SerializeField] private float minimum;
		/// <summary>
		/// The maximum amount value.
		/// </summary>
		[SerializeField] private float maximum;
		/// <summary>
		/// The actual amount value.
		/// </summary>
		[SerializeField] private float value;
		/// <summary>
		/// Less than events dictionary.
		/// </summary>
		private readonly Dictionary<int, AmountModifiedEventHandler> lessThanEvents = new Dictionary<int, AmountModifiedEventHandler>();
		/// <summary>
		/// More than events dictionary.
		/// </summary>
		private readonly Dictionary<int, AmountModifiedEventHandler> moreThanEvents = new Dictionary<int, AmountModifiedEventHandler>();
		#endregion

		#region public methods		
		/// <summary>
		/// Registers a callback to call when the value is less than the given threshold.
		/// </summary>
		/// <param name="threshold">The threshold.</param>
		/// <param name="callback">The callback.</param>
		public void OnLessThan(int threshold, AmountModifiedEventHandler callback) {
			lessThanEvents.Add(threshold, null);
			lessThanEvents[threshold] += callback;
		}

		/// <summary>
		/// Removes a callback from less than event dictionary at the given threshold.
		/// </summary>
		/// <param name="threshold">The threshold.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="remove">Removes the event at the threshold key if true.</param>
		public void OffLessThan(int threshold, AmountModifiedEventHandler callback, bool remove = true) {
			if (!lessThanEvents.ContainsKey(threshold)) return;
			// ReSharper disable once DelegateSubtraction
			lessThanEvents[threshold] -= callback;
			if (remove) lessThanEvents.Remove(threshold);
		}

		/// <summary>
		/// Registers a callback to call when the value is more than the given threshold.
		/// </summary>
		/// <param name="threshold">The threshold.</param>
		/// <param name="callback">The callback.</param>
		public void OnMoreThan(int threshold, AmountModifiedEventHandler callback) {
			moreThanEvents.Add(threshold, null);
			moreThanEvents[threshold] += callback;
		}

		/// <summary>
		/// Removes a callback from more than event dictionary at the given threshold.
		/// </summary>
		/// <param name="threshold">The threshold.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="remove">Removes the event at the threshold key if true.</param>
		public void OffMoreThan(int threshold, AmountModifiedEventHandler callback, bool remove = true) {
			if (!moreThanEvents.ContainsKey(threshold)) return;
			// ReSharper disable once DelegateSubtraction
			moreThanEvents[threshold] -= callback;
			if (remove) moreThanEvents.Remove(threshold);
		}

		/// <summary>
		/// Increase amount value;
		/// </summary>
		/// <param name="increaseValue">Value to increase.</param>
		/// <param name="operation">amount operation type.</param>
		public void Increase(float increaseValue, AmountOperation operation) {
			var previousValue = value;
			value = ClampValue(GetComputedValue(value + increaseValue, operation));
			CallEvents(previousValue);
		}

		/// <summary>
		/// Decrease amount value.
		/// </summary>
		/// <param name="decreaseValue">Value to decrease.</param>
		/// <param name="operation">amount operation type.</param>
		public void Decrease(float decreaseValue, AmountOperation operation) {
			var previousValue = value;
			value = ClampValue(GetComputedValue(value - decreaseValue, operation));
			CallEvents(previousValue);
		}

		/// <summary>
		/// Increase maximum value;
		/// </summary>
		/// <param name="increaseMaximumValue">Value to increase.</param>
		/// <param name="operation">amount operation type.</param>
		public void IncreaseMaximum(float increaseMaximumValue, AmountOperation operation) {
			SetMaximum(GetComputedValue(maximum + increaseMaximumValue, operation));
		}

		/// <summary>
		/// Decrease maximum value.
		/// </summary>
		/// <param name="decreaseMaximumValue">Value to decrease.</param>
		/// <param name="operation">amount operation type.</param>
		public void DecreaseMaximum(float decreaseMaximumValue, AmountOperation operation) {
			SetMaximum(GetComputedValue(maximum - decreaseMaximumValue, operation));
		}

		/// <summary>
		/// Increase minimum value;
		/// </summary>
		/// <param name="increaseMinimumValue">Value to increase.</param>
		/// <param name="operation">amount operation type.</param>
		public void IncreaseMinimum(float increaseMinimumValue, AmountOperation operation) {
			SetMinimum(GetComputedValue(maximum + increaseMinimumValue, operation));
		}

		/// <summary>
		/// Decrease minimum value.
		/// </summary>
		/// <param name="decreaseMinimumValue">Value to decrease.</param>
		/// <param name="operation">amount operation type.</param>
		public void DecreaseMinimum(float decreaseMinimumValue, AmountOperation operation) {
			SetMinimum(GetComputedValue(maximum - decreaseMinimumValue, operation));
		}

		/// <summary>
		/// Sets the amount value to min value.
		/// </summary>
		public void ResetToMinimum() {
			var previousValue = value;
			value = Minimum;
			CallEvents(previousValue);
		}

		/// <summary>
		/// Sets the amount value to min value.
		/// </summary>
		public void ResetToMaximum() {
			var previousValue = value;
			value = Maximum;
			CallEvents(previousValue);
		}
		#endregion

		#region private methods
		/// <summary>
		/// Clamps the given value.
		/// </summary>
		/// <param name="valueToClamp">Value to clamp.</param>
		/// <returns>The clamped value.</returns>
		private float ClampValue(float valueToClamp) {
			return Mathf.Clamp(valueToClamp, Minimum, Maximum);
		}

		/// <summary>
		/// Controls and sets the maximum value.
		/// </summary>
		/// <param name="maximumValue">The maximum value to set.</param>
		private void SetMaximum(float maximumValue) {
			if (maximumValue <= minimum) return;
			maximum = maximumValue;
			value = maximum;
		}

		/// <summary>
		///  Controls and sets the minimum value.
		/// </summary>
		/// <param name="minimumValue">The minimum value to set.</param>
		private void SetMinimum(float minimumValue) {
			if (minimumValue >= maximum) return;
			minimum = minimumValue;
			value = maximum;
		}

		/// <summary>
		/// Gets the computed value using operation type.
		/// </summary>
		/// <param name="valueToCompute">The value to compute.</param>
		/// <param name="operation">The operation to apply.</param>
		/// <returns>The computed value.</returns>
		private float GetComputedValue(float valueToCompute, AmountOperation operation) {
			switch (operation) {
				case AmountOperation.Default: return valueToCompute;
				case AmountOperation.MissingPercentage: return MissingValue * valueToCompute / 100f;
				case AmountOperation.RangePercentage: return RangeValue * valueToCompute / 100f;
				case AmountOperation.RemainingPercentage: return RemainingValue * valueToCompute / 100f;
				default: return 0;
			}
		}

		/// <summary>
		/// Checks for events to call.
		/// </summary>
		/// <param name="previousValue">The previous amount value.</param>
		private void CallEvents(float previousValue) {
			foreach (var key in lessThanEvents.Keys) {
				if (value < key) CallEvent(lessThanEvents[key]);
			}
			foreach (var key in moreThanEvents.Keys) {
				if (value > key) CallEvent(moreThanEvents[key]);
			}
			CallEvent(ModifyValueEvent);
			if (value >= maximum) CallEvent(FullEvent);
			else if (value <= minimum) CallEvent(EmptyEvent);
			if (value > previousValue) CallEvent(IncreaseEvent);
			else if (value < previousValue) CallEvent(DecreaseEvent);
		}

		/// <summary>
		/// Safely calls event.
		/// </summary>
		/// <param name="amountModifiedEventHandler">Event to call.</param>
		private static void CallEvent(AmountModifiedEventHandler amountModifiedEventHandler) {
			var handler = amountModifiedEventHandler;
			if (handler != null) handler();
		}
		#endregion
	}
}