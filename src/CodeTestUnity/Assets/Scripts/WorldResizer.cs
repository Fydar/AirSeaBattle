using CodeTest.Game.Math;
using CodeTest.Game.Simulation;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldResizer : MonoBehaviour
	{
		[SerializeField] private Entrypoint entrypoint;

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

					entrypoint.CurrentWorld.Resize(entrypoint.CurrentWorld.Height * ratio, entrypoint.CurrentWorld.Height);
				}

				currentWidth = Screen.width;
				currentHeight = Screen.height;
				lastWorld = entrypoint.CurrentWorld;
			}
		}
	}
}
