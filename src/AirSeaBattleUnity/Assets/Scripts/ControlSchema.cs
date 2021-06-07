using System;
using UnityEngine;

namespace AirSeaBattleUnity
{
	[CreateAssetMenu]
	public class ControlSchema : ScriptableObject
	{
		[SerializeField] private KeyCode[] up;
		[SerializeField] private KeyCode[] down;
		[SerializeField] private KeyCode[] fire;

		public KeyCode[] Up => up ?? Array.Empty<KeyCode>();
		public KeyCode[] Down => down ?? Array.Empty<KeyCode>();
		public KeyCode[] Fire => fire ?? Array.Empty<KeyCode>();
	}
}
