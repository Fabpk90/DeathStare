using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Sweet.UI
{
	[RequireComponent(typeof(Button))]
	public class UIPageButton : MonoBehaviour
	{
		private enum Direction { Next, Previous }

		[SerializeField]
		private UIPageText pageText;

		[SerializeField]
		private Direction direction = Direction.Next;
		[SerializeField]
		private bool WaitAnimationToEnd = true;

		private Button button;

		private void Awake()
		{
			button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			button.onClick.AddListener(OnClick);
		}

		private void OnDisable()
		{
			button.onClick.RemoveListener(OnClick);
		}

		private void OnClick()
		{
			switch (direction)
			{
				case Direction.Next:
					pageText.GoToNextPage();
					break;
				case Direction.Previous:
					pageText.GoToPreviousPage();
					break;
				default:
					break;
			}
		}

		private void Update()
		{
			switch (direction)
			{
				case Direction.Next:
					SetInteractable(pageText.IsNextPageExist());
					break;
				case Direction.Previous:
					SetInteractable(pageText.IsPreviousPageExist());
					break;
				default:
					break;
			}

		}

		private void SetInteractable(bool b)
		{
			if (WaitAnimationToEnd)
			{
				button.interactable = b && pageText.IsTextAnimationEnd();
			}
			else
			{
				button.interactable = b;
			}
		}

	}
}
