using UnityEngine;
using System.Collections;
using TMPro;
using BrunoMikoski.TextJuicer;
namespace Sweet.UI
{
	public class UIPageText : UITextBase
	{
		private string[] pages;
		private bool[] isRead;
		private int pageIndex;

		//Inspector
		[TextArea(2, 10), SerializeField]
		private string fullText;
		[SerializeField]
		private char spliter = '#';
		[SerializeField]
		private bool pageLoop = false;
		[SerializeField]
		private bool autoStart = false;
		[SerializeField]
		private bool animateVisitedPage = false;


		#region Unity Methods

		private void Start()
		{
			if (autoStart)
				LoadText(fullText, true);
		}

		private void OnValidate()
		{
			UpdatePages();
		}
		#endregion

		#region internal

		private void UpdatePages()
		{
			pages = fullText.Split(spliter);
			pageIndex = 0;
			isRead = new bool[pages.Length];
			for (int i = 0; i < isRead.Length; i++) isRead[i] = false;
		}

		private void SetText()
		{
			if (!isRead[pageIndex] || animateVisitedPage)
			{
				_textJuicer.SetTextAndPlay(pages[pageIndex]);
				isRead[pageIndex] = true;
			}
			else
			{
				_textJuicer.SetTextandProgress(pages[pageIndex], 1);
				isRead[pageIndex] = true;
			}
		}
		#endregion

		#region public method

		public bool IsPageExist(int index)
		{
			return (index >= 0 && index < pages.Length);
		}

		public bool IsNextPageExist()
		{
			return pageLoop || IsPageExist(pageIndex + 1);
		}
		public bool IsPreviousPageExist()
		{
			return pageLoop || IsPageExist(pageIndex - 1);
		}

		public void GoToPage(int index)
		{
			if (IsPageExist(index))
			{
				pageIndex = index;
				_textJuicer.Play(true);
			}
			SetText();
		}

		public void GoToNextPage()
		{
			if (pageLoop && pageIndex >= pages.Length - 1)
			{
				GoToPage(0);
			}
			else
			{
				GoToPage(pageIndex + 1);
			}
		}

		public void GoToPreviousPage()
		{
			if (pageLoop && pageIndex <= 0)
			{
				GoToPage(pages.Length - 1);
			}
			else
			{
				GoToPage(pageIndex - 1);
			}
		}

		public void LoadText(string txt, bool displayFirstPage = false)
		{
			fullText = txt;
			UpdatePages();
			if (displayFirstPage)
			{
				SetText();
			}
		}

		public bool IsTextAnimationEnd()
		{
			return _textJuicer.Progress == 1;
		}
		#endregion
	}
}
