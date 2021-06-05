﻿using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;
using System.Diagnostics;
using UnityEngine;

namespace CodeTestUnity.EntityRendering
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
				(simPosition.X - (RenderTarget.World.Width * Constants.Half)).AsFloat,
				(simPosition.Y - (RenderTarget.World.Height * Constants.Half)).AsFloat,
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
				(position.X - (RenderTarget.World.Width * Constants.Half)).AsFloat,
				(position.Y - (RenderTarget.World.Height * Constants.Half)).AsFloat,
				0.0f);
		}
	}
}
