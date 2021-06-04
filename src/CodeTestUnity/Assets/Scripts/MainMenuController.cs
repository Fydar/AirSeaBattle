using UnityEngine;

namespace CodeTestUnity
{
	public class MainMenuController : MonoBehaviour
	{
		[SerializeField] private GameObject mainMenuContainer;
		[SerializeField] private HudController hudContainer;
		[SerializeField] private GameRunner gameRunner;

		private void Start()
		{
			mainMenuContainer.gameObject.SetActive(true);
			hudContainer.Hide();
		}

		public void UiStartButton()
		{
			mainMenuContainer.SetActive(false);
			hudContainer.Show();

			gameRunner.RunGame();
		}
	}
}
