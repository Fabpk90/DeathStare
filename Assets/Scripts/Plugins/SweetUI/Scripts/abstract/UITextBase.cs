using UnityEngine;
using System.Collections;
using BrunoMikoski.TextJuicer;

namespace Sweet.UI
{
	[RequireComponent(typeof(TMP_TextJuicer))]
	public abstract class UITextBase : MonoBehaviour
	{
		protected TMP_TextJuicer _textJuicer;

		protected virtual void Awake()
		{
			_textJuicer = GetComponent<TMP_TextJuicer>();
		}


		//display text 
		public virtual void SetText(string text, bool isAnimated = true)
		{
			if (!isAnimated)
			{
				_textJuicer.SetTextandProgress(text, 1);
			}
			else
			{
				_textJuicer.SetTextAndPlay(text);
			}
		}
	}
}
