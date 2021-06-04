using CodeTest.Game.Simulation;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldRenderer : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer background;
		[SerializeField] private WorldGunRenderer gunRendererPrefab;
		[SerializeField] private WorldEnemyRenderer enemyRendererPrefab;

		public World World { get; private set; }

		public void Render(World world)
		{
			World = world;

			World.Guns.Handlers[this].AddAndInvoke(new InstantiateAndDestoryHandler<WorldGun>(gunRendererPrefab));
			World.Enemies.Handlers[this].AddAndInvoke(new InstantiateAndDestoryHandler<WorldEnemy>(enemyRendererPrefab));
		}
	}
}
