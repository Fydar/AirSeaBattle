using CodeTest.Game.Simulation.Models;
using Industry.Simulation.Math;
using RPGCore.Events;
using UnityEngine;

namespace CodeTestUnity.EntityRendering
{
	public class WorldGunRenderer : EntityRenderer<WorldGun>
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

		protected override void OnStopRendering(WorldGun target)
		{
		}

		protected override void OnStartRendering(WorldGun target)
		{
			if (target == null)
			{
				graphic.enabled = false;
			}
			else
			{
				graphic.enabled = true;
				UpdateGraphics();
			}
		}

		private void Update()
		{
			if (RenderTarget == null)
			{
				return;
			}

			UpdateGraphics();
		}

		private void OnDrawGizmos()
		{
			if (RenderTarget == null)
			{
				return;
			}

			var position = new Vector3(
				(RenderTarget.Bounds.Center.X - (RenderTarget.World.WorldWidth * Constants.Half)).AsFloat,
				(RenderTarget.Bounds.Center.Y - (RenderTarget.World.WorldHeight * Constants.Half)).AsFloat, 0.0f);

			var size = new Vector3(
				RenderTarget.World.Configuration.GunSize.X.AsFloat,
				RenderTarget.World.Configuration.GunSize.Y.AsFloat, 0.0f);

			Gizmos.DrawWireCube(position, size);

			Gizmos.color = Color.green;
			Gizmos.color = Color.white;
		}

		private void UpdateGraphics()
		{
			var position = RenderTarget.Position;
			transform.localPosition = new Vector3(
				(position.X - (RenderTarget.World.WorldWidth * Constants.Half)).AsFloat,
				(position.Y - (RenderTarget.World.WorldHeight * Constants.Half)).AsFloat,
				0.0f);

			if (RenderTarget.Angle.Value.Graphic == "gun_90")
			{
				graphic.sprite = gun90;
			}
			else if (RenderTarget.Angle.Value.Graphic == "gun_60")
			{
				graphic.sprite = gun60;
			}
			else if (RenderTarget.Angle.Value.Graphic == "gun_30")
			{
				graphic.sprite = gun30;
			}
		}
	}
}
