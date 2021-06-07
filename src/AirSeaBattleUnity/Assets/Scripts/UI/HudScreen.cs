using UnityEngine;

namespace AirSeaBattleUnity
{
	public class HudScreen : MonoBehaviour
	{
		[SerializeField] private GameObject container;
		[SerializeField] private WorldPlayerRenderer playerRenderer;

		public WorldPlayerRenderer PlayerRenderer => playerRenderer;

		public void Show()
		{
			container.SetActive(true);
		}

		public void Hide()
		{
			container.SetActive(false);
		}

		public void HideTime()
		{
			PlayerRenderer.TimeRemainingContainer.SetActive(false);
		}

		public void ShowTime()
		{
			PlayerRenderer.TimeRemainingContainer.SetActive(true);
		}
	}
}
