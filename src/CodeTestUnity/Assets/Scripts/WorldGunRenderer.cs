using CodeTest.Game.Simulation;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldGunRenderer : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer graphic;

		public WorldGun Gun { get; private set; }

		public void Render(WorldGun gun)
		{
			Gun = gun;

			SetPosition();
		}

		private void Update()
		{
			
		}

		private void SetPosition()
		{
			transform.localPosition = new Vector3(Gun.PositionX.AsFloat, 0.0f, 0.0f);
		}
	}
}
