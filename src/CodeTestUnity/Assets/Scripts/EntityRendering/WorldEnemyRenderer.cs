using Industry.Simulation.Math;
using CodeTest.Game.Simulation.Models;
using System.Diagnostics;
using UnityEngine;

namespace CodeTestUnity.EntityRendering
{
	public class WorldEnemyRenderer : EntityRenderer<WorldEnemy>
	{
		[SerializeField] private SpriteRenderer graphic;
		[SerializeField] private AudioSource explosionSound;

		protected override void OnStopRendering(WorldEnemy target)
		{
			if (target != null)
			{
				target.OnDestroyed -= OnDestroyed;
			}
		}

		protected override void OnStartRendering(WorldEnemy target)
		{
			if (target != null)
			{
				target.OnDestroyed += OnDestroyed;
				graphic.enabled = true;
				SetPosition();
			}
			else
			{
				graphic.enabled = false;
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

			var size = new Vector3(RenderTarget.Template.Width.AsFloat, RenderTarget.Template.Height.AsFloat, 0.0f);

			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(localPosition, size);
			Gizmos.color = Color.white;
		}

		private void OnDestroyed()
		{
			explosionSound.Play();
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
