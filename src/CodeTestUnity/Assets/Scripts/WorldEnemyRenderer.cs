﻿using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;
using System.Diagnostics;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldEnemyRenderer : MonoBehaviour, IRenderer<WorldEnemy>
	{
		[SerializeField] private SpriteRenderer graphic;
		[SerializeField] private AudioSource explosionSound;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private WorldEnemy renderTarget;
		public WorldEnemy RenderTarget
		{
			get
			{
				return renderTarget;
			}
			set
			{
				if (renderTarget != null)
				{
					renderTarget.OnDestroyed -= OnDestroyed;
				}

				renderTarget = value;

				if (renderTarget != null)
				{
					renderTarget.OnDestroyed += OnDestroyed;
					graphic.enabled = true;
					SetPosition();
				}
				else
				{
					graphic.enabled = false;
				}
			}
		}

		private void Update()
		{
			if (renderTarget == null)
			{
				return;
			}

			SetPosition();
		}

		private void OnDrawGizmos()
		{
			if (renderTarget == null)
			{
				return;
			}

			var simPosition = renderTarget.Position.Value;

			var localPosition = new Vector3(
				(simPosition.X - (renderTarget.World.Width * Constants.Half)).AsFloat,
				(simPosition.Y - (renderTarget.World.Height * Constants.Half)).AsFloat,
				0.0f);

			var size = new Vector3(renderTarget.Template.Width.AsFloat, renderTarget.Template.Height.AsFloat, 0.0f);

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
			var position = renderTarget.Position.Value;
			transform.localPosition = new Vector3(
				(position.X - (renderTarget.World.Width * Constants.Half)).AsFloat,
				(position.Y - (renderTarget.World.Height * Constants.Half)).AsFloat,
				0.0f);
		}
	}
}
