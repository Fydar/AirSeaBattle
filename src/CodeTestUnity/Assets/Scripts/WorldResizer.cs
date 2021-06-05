using CodeTest.Game.Simulation;
using Industry.Simulation.Math;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldResizer : MonoBehaviour
	{
		[SerializeField] private GameRunner entrypoint;

		private int currentWidth;
		private int currentHeight;
		private World lastWorld;

		private void Update()
		{
			if (currentWidth != Screen.width
				|| currentHeight != Screen.height
				|| lastWorld != entrypoint.CurrentWorld)
			{
				if (entrypoint.CurrentWorld != null)
				{
					var ratio = ((Fixed)Screen.width) / ((Fixed)Screen.height);

					entrypoint.CurrentWorld.Resize(entrypoint.CurrentWorld.WorldHeight * ratio, entrypoint.CurrentWorld.WorldHeight);
				}

				currentWidth = Screen.width;
				currentHeight = Screen.height;
				lastWorld = entrypoint.CurrentWorld;
			}
		}
	}
}
