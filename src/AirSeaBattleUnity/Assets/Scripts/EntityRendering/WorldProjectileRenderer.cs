using AirSeaBattle.Game.Simulation.Models;
using Industry.Simulation.Math;
using UnityEngine;

namespace AirSeaBattleUnity.EntityRendering
{
	public class WorldProjectileRenderer : EntityRenderer<WorldProjectile>
	{
		[SerializeField] private SpriteRenderer graphic;

		protected override void OnStopRendering(WorldProjectile target)
		{
		}

		protected override void OnStartRendering(WorldProjectile target)
		{
			if (target == null)
			{
				graphic.enabled = false;
			}
			else
			{
				graphic.enabled = true;
				SetPosition();
			}
		}

		private void Update()
		{
			if (RenderTarget == null)
			{
				return;
			}

			SetPosition();
		}

		private void OnDrawGizmos()
		{
			if (RenderTarget == null)
			{
				return;
			}

			var simPosition = RenderTarget.Position.Value;

			var localPosition = new Vector3(
				(simPosition.X - (RenderTarget.World.WorldWidth * Constants.Half)).AsFloat,
				(simPosition.Y - (RenderTarget.World.WorldHeight * Constants.Half)).AsFloat,
				0.0f);

			var size = new Vector3(0.125f, 0.125f, 0.0f);

			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(localPosition, size);
			Gizmos.color = Color.white;
		}

		private void SetPosition()
		{
			var position = RenderTarget.Position.Value;
			transform.localPosition = new Vector3(
				(position.X - (RenderTarget.World.WorldWidth * Constants.Half)).AsFloat,
				(position.Y - (RenderTarget.World.WorldHeight * Constants.Half)).AsFloat,
				0.0f);
		}
	}
}
