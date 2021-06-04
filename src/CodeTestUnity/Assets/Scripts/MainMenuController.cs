using System.Collections;
using UnityEngine;

namespace CodeTestUnity
{
	public class MainMenuController : MonoBehaviour
	{
		[SerializeField] private GameObject mainMenuContainer;
		[SerializeField] private GameRunner gameRunner;

		public void UiStartButton()
		{
			mainMenuContainer.SetActive(false);

			gameRunner.RunGame();
		}
	}
}
