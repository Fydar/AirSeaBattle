using CodeTest.Game.Simulation;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldEnemyRenderer : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer graphic;

		public WorldEnemy Enemy { get; private set; }

		public void Render(WorldEnemy enemy)
		{
			Enemy = enemy;

			SetPosition();
		}

		private void SetPosition()
		{
			transform.localPosition = new Vector3(Enemy.PositionX.AsFloat, 0.0f, 0.0f);
		}
	}
}
