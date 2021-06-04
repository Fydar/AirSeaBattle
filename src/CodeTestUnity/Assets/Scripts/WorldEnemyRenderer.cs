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
