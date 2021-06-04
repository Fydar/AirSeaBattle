using CodeTest.Game.Simulation;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldRenderer : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer background;
		[SerializeField] private WorldGunRenderer gunRendererPrefab;
		[SerializeField] private WorldEnemyRenderer enemyRendererPrefab;
		[SerializeField] private WorldProjectileRenderer projectileRendererPrefab; 

		public World World { get; private set; }

		public void Render(World world)
		{
			World = world;

			World.Guns.Handlers[this].AddAndInvoke(new InstantiateAndDestoryHandler<WorldGun>(gunRendererPrefab));
			World.Enemies.Handlers[this].AddAndInvoke(new InstantiateAndDestoryHandler<WorldEnemy>(enemyRendererPrefab));
			World.Projectiles.Handlers[this].AddAndInvoke(new InstantiateAndDestoryHandler<WorldProjectile>(projectileRendererPrefab));
		}

		private void Update()
		{
			if (World == null)
			{
				return;
			}

			background.transform.localScale = new Vector3(World.Width.AsFloat, World.Height.AsFloat, 0.0f);
		}

		private void OnDrawGizmos()
		{
			var localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			var gunSize = new Vector3(World.Width.AsFloat, World.Height.AsFloat, 0.0f);

			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(localPosition, gunSize);
			Gizmos.color = Color.white;
		}
	}
}
