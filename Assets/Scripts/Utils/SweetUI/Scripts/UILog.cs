using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Sweet.UI
{
	[RequireComponent(typeof(ScrollRect))]
	public class UILog : MonoBehaviour
	{
		private ScrollRect scrollRect;

		public enum LogType { Neutral, Positive, Negative, Important }
		[SerializeField]
		private UITextBase neutralTextAsset;
		[SerializeField]
		private UITextBase positiveTextAsset;
		[SerializeField]
		private UITextBase negativeTextAsset;
		[SerializeField]
		private UITextBase importantTextAsset;
		[SerializeField]
		private bool animatedLog = true;

		private void Awake()
		{
			scrollRect = GetComponent<ScrollRect>();
		}

		public void AddLog(string text, LogType logType)
		{
			InstantiateText(text, GetTextAsset(logType));
			StartCoroutine(UpdateScrollBarNextFrame());
		}

		public void AddLog(string text)
		{
			AddLog(text, LogType.Neutral);
		}

		private IEnumerator UpdateScrollBarNextFrame()
		{
			yield return null;
			yield return null;
			scrollRect.verticalScrollbar.value = 0;
		}

		private UITextBase GetTextAsset(LogType logType)
		{
			switch (logType)
			{
				case LogType.Neutral:
					return neutralTextAsset;
				case LogType.Positive:
					return positiveTextAsset;
				case LogType.Negative:
					return negativeTextAsset;
				case LogType.Important:
					return importantTextAsset;
				default:
					return neutralTextAsset;
			}
		}

		private void InstantiateText(string text, UITextBase textAsset)
		{
			if (textAsset == null) return;
			UITextBase temp = Instantiate(textAsset, scrollRect.content.transform);
			temp.SetText(text, animatedLog);
		}
	}
}
