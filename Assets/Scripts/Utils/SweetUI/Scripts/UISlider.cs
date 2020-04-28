using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sweet.UI
{
	public class UISlider : UITransition
	{
		//inspector
		[SerializeField]
		private RectTransform backgroundRect;
		[SerializeField]
		private RectTransform mainImageRect;
		[SerializeField]
		private RectTransform positiveAnticipationRect;
		[SerializeField]
		private RectTransform negativeAnticipationRect;
		[SerializeField]
		private RectTransform comparatorRect;
		[SerializeField]
		private float updateDelay = 1;
		[SerializeField]
		private float updateSpeed = 1;
		[Range(0, 1), SerializeField, Space]
		private float _value;
		[Range(0, 1), SerializeField]
		private float _delayedValue;
		[Range(0, 1), SerializeField]
		private float minComparatorValue;
		[Range(0, 1), SerializeField]
		private float maxComparatorValue;

		//private 
		private bool updating = false;
		private float lastChange = 0;
		private float lastValue = 0;
		private bool isComparing;
		private Coroutine comparatorRoutine;
		private Coroutine waitDelayUpdateRoutine;
		private Coroutine delayUpdateRoutine;

		//accessors
		public float value
		{
			get { return _value; }
			set { SetValue(value, false); }
		}
		public float delayedValue => _delayedValue;


		#region UnityMethod

		private void OnRectTransformDimensionsChange()
		{
			if (backgroundRect && mainImageRect && positiveAnticipationRect && negativeAnticipationRect && comparatorRect)
			{
				UpdatePosition();
			}
		}

		private void OnValidate()
		{
			if (backgroundRect && mainImageRect && positiveAnticipationRect && negativeAnticipationRect && comparatorRect)
			{
				if (Application.isPlaying)
				{
					UpdateDelayValue();
					UpdatePosition();
				}
				else
				{
					UpdatePosition();
				}
			}
		}

		#endregion

		#region public method
		//Set the value
		public void SetValue(float newValue, bool instantChange = false)
		{
			newValue = Mathf.Clamp01(newValue);
			if (_value != newValue)
			{
				lastChange = Time.time;
			}
			_value = newValue;
			if (instantChange)
			{
				_delayedValue = _value;
			}
			else
			{
				UpdateDelayValue();
			}
			UpdatePosition();
		}
		//set the comparator
		public void SetComparator(float min, float max, float duration, float speed, float resetValue = 0)
		{
			if (comparatorRoutine != null)
			{
				StopCoroutine(comparatorRoutine);
			}
			comparatorRoutine = StartCoroutine(Comparing(min, max, duration, speed, resetValue));
		}

		#endregion

		#region Internal

		private IEnumerator Comparing(float min, float max, float duration, float speed, float resetValue)
		{
			isComparing = true;

			while (minComparatorValue != min || maxComparatorValue != max)
			{
				minComparatorValue = Mathf.MoveTowards(minComparatorValue, min, Time.deltaTime * speed);

				maxComparatorValue = Mathf.MoveTowards(maxComparatorValue, max, Time.deltaTime * speed);
				UpdatePosition();
				yield return null;
			}
			UpdatePosition();
			yield return new WaitForSeconds(duration);

			while (minComparatorValue != resetValue || maxComparatorValue != resetValue)
			{
				minComparatorValue = Mathf.MoveTowards(minComparatorValue, resetValue, Time.deltaTime * speed);

				maxComparatorValue = Mathf.MoveTowards(maxComparatorValue, resetValue, Time.deltaTime * speed);
				UpdatePosition();
				yield return null;
			}

			isComparing = false;
		}

		private void UpdatePosition()
		{
			SetPosition(mainImageRect, 0, _delayedValue);
			if (_value > _delayedValue)
			{
				SetPosition(positiveAnticipationRect, _delayedValue, _value);
				SetPosition(negativeAnticipationRect, _value, _value);
			}
			else
			{
				SetPosition(negativeAnticipationRect, _delayedValue, _value);
				SetPosition(positiveAnticipationRect, _value, _value);
			}
			SetPosition(comparatorRect, minComparatorValue, maxComparatorValue);
		}

		private void SetPosition(RectTransform t, float minValue, float maxValue)
		{
			/*Image i = t.GetComponent<Image>();
			if (i && i.sprite!=null && i.fillMethod == Image.FillMethod.Horizontal)
			{
				t.offsetMin = new Vector2(0, 0);
				t.offsetMax = new Vector2(0, 0);
				i.fillAmount = maxValue;
			}else
			{*/
			if (minValue < maxValue)
			{
				t.offsetMin = new Vector2(GetWidthRatio(minValue), t.offsetMin.y);
				t.offsetMax = new Vector2(GetWidthRatio(maxValue - 1), t.offsetMax.y);
			}
			else
			{
				t.offsetMin = new Vector2(GetWidthRatio(maxValue), t.offsetMin.y);
				t.offsetMax = new Vector2(GetWidthRatio(minValue - 1), t.offsetMax.y);
			}
			//}
		}

		private void UpdateDelayValue()
		{
			if (!updating)
			{
				if (waitDelayUpdateRoutine != null)
				{
					StopCoroutine(waitDelayUpdateRoutine);
				}
				waitDelayUpdateRoutine = StartCoroutine(WaitBeforeDelay());
				//StartCoroutine(UpdateDelayValue(_value, updateSpeed));
			}
			else
			{
				if (delayUpdateRoutine != null) StopCoroutine(delayUpdateRoutine);
				delayUpdateRoutine = StartCoroutine(UpdateDelayValue(_value, updateSpeed));
				/*if (waitDelayUpdateRoutine != null)
				{
					StopCoroutine(waitDelayUpdateRoutine);
				}
				waitDelayUpdateRoutine = StartCoroutine(WaitBeforeDelay());*/
			}
		}

		private IEnumerator WaitBeforeDelay()
		{
			yield return new WaitForSeconds(updateDelay);
			UpdatePosition();
			if (delayUpdateRoutine != null) StopCoroutine(delayUpdateRoutine);
			delayUpdateRoutine = StartCoroutine(UpdateDelayValue(_value, updateSpeed));
		}

		private IEnumerator UpdateDelayValue(float newValue, float speed)
		{
			updating = true;
			float lastDv = _delayedValue;
			while (_delayedValue != newValue)
			{
				float dir = Mathf.Sign(newValue - _delayedValue);
				if (Mathf.Abs(newValue - _delayedValue) > Time.deltaTime * speed)
				{
					_delayedValue += dir * Time.deltaTime * speed;
				}
				else
				{
					_delayedValue = newValue;
				}

				UpdatePosition();
				yield return null;
			}
			UpdatePosition();
			updating = false;
		}


		private float GetWidthRatio(float v)
		{
			return v * backgroundRect.rect.width;
		}

		#endregion
	}
}
