using System.Collections;
using UnityEngine;

namespace AirSeaBattleUnity
{
	public class MainMenuScreen : MonoBehaviour
	{
		[SerializeField] private GameObject mainMenuContainer;

		private bool buttonPressed = false;

		public void UiStartButton()
		{
			buttonPressed = true;
		}

		public IEnumerator WaitForButtonPressed()
		{
			while (!buttonPressed)
			{
				yield return null;
			}
			buttonPressed = false;
		}

		public void Show()
		{
			mainMenuContainer.SetActive(true);
		}

		public void Hide()
		{
			mainMenuContainer.SetActive(false);
		}
	}
}
