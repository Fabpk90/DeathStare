using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
namespace Sweet.UI
{
	[RequireComponent(typeof(Animator))]
	public class UITransition : MonoBehaviour
	{
		private Animator anim;
		private void Awake()
		{
			anim = GetComponent<Animator>();
		}

		public void Show()
		{
			anim.SetBool("Show", true);
		}

		public void Hide()
		{
			anim.SetBool("Show", false);
		}
	}
}
