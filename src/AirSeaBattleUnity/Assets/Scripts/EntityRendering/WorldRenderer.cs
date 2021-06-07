using AirSeaBattle.Game.Simulation;
using AirSeaBattle.Game.Simulation.Models;
using UnityEngine;

namespace AirSeaBattleUnity.EntityRendering
{
	public class WorldRenderer : EntityRenderer<World>
	{
		[SerializeField] private SpriteRenderer background;
		[SerializeField] private WorldGunRenderer gunRendererPrefab;
		[SerializeField] private WorldEnemyRenderer enemyRendererPrefab;
		[SerializeField] private WorldProjectileRenderer projectileRendererPrefab;

		protected override void OnStopRendering(World target)
		{
			target.Guns.Handlers[this].Clear();
			target.Enemies.Handlers[this].Clear();
			target.Projectiles.Handlers[this].Clear();
		}

		protected override void OnStartRendering(World target)
		{
			if (target != null)
			{
				target.Guns.Handlers[this].AddAndInvoke(new RendererPoolHandler<WorldGun>(gunRendererPrefab, 1));
				target.Enemies.Handlers[this].AddAndInvoke(new RendererPoolHandler<WorldEnemy>(enemyRendererPrefab, 5));
				target.Projectiles.Handlers[this].AddAndInvoke(new RendererPoolHandler<WorldProjectile>(projectileRendererPrefab, 5));
			}
		}

		private void Update()
		{
			if (RenderTarget == null)
			{
				return;
			}

			background.transform.localScale = new Vector3(RenderTarget.WorldWidth.AsFloat, RenderTarget.WorldHeight.AsFloat, 0.0f);
		}

		private void OnDrawGizmos()
		{
			if (RenderTarget == null)
			{
				return;
			}

			var localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			var gunSize = new Vector3(RenderTarget.WorldWidth.AsFloat, RenderTarget.WorldHeight.AsFloat, 0.0f);

			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(localPosition, gunSize);
			Gizmos.color = Color.white;
		}
	}
}
