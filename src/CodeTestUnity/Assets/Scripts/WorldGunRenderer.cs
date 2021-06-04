using CodeTest.Game.Math;
using CodeTest.Game.Simulation;
using RPGCore.Events;
using System.Diagnostics;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldGunRenderer : MonoBehaviour, IRenderer<WorldGun>
	{
		[SerializeField] private Sprite gun90;
		[SerializeField] private Sprite gun60;
		[SerializeField] private Sprite gun30;

		private class WorldGunPropertyChangedHandler : IEventFieldHandler
		{
			private readonly WorldGun worldGun;

			public WorldGunPropertyChangedHandler(WorldGun worldGun)
			{
				this.worldGun = worldGun;
			}

			public void OnAfterChanged()
			{
			}

			public void OnBeforeChanged()
			{
			}
		}

		[SerializeField] private SpriteRenderer graphic;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private WorldGun renderTarget;
		public WorldGun RenderTarget
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
					UpdateGraphics();
				}
			}
		}

		private void Update()
		{
			if (renderTarget == null)
			{
				return;
			}

			UpdateGraphics();
		}

		private void OnDrawGizmos()
		{
			var simPosition = renderTarget.Position;
			var localPosition = new Vector3(
				(simPosition.X - (renderTarget.World.Width * Constants.Half)).AsFloat,
				(simPosition.Y - (renderTarget.World.Height * Constants.Half)).AsFloat,
				0.0f);

			var gunSize = new Vector3(renderTarget.World.GunSize.X.AsFloat, renderTarget.World.GunSize.Y.AsFloat, 0.0f);

			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(localPosition, gunSize);
			Gizmos.color = Color.white;
		}

		private void UpdateGraphics()
		{
			var position = renderTarget.Position;
			transform.localPosition = new Vector3(
				(position.X - (renderTarget.World.Width * Constants.Half)).AsFloat,
				(position.Y - (renderTarget.World.Height * Constants.Half)).AsFloat,
				0.0f);

			graphic.flipX = renderTarget.IsFlipped.Value;

			if (renderTarget.Angle.Value.Graphic == "gun_90")
			{
				graphic.sprite = gun90;
			}
			else if (renderTarget.Angle.Value.Graphic == "gun_60")
			{
				graphic.sprite = gun60;
			}
			else if (renderTarget.Angle.Value.Graphic == "gun_30")
			{
				graphic.sprite = gun30;
			}
		}
	}
}
