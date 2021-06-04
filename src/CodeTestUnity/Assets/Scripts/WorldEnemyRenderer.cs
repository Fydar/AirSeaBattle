using CodeTest.Game.Math;
using CodeTest.Game.Simulation;
using System.Diagnostics;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldEnemyRenderer : MonoBehaviour, IRenderer<WorldEnemy>
	{
		[SerializeField] private SpriteRenderer graphic;

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
				renderTarget = value;

				if (renderTarget == null)
				{
					graphic.enabled = false;
				}
				else
				{
					graphic.enabled = true;
					SetPosition();
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
