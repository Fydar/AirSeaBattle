using System;
using System.IO;

namespace Industry.Simulation.Math.Internal
{
	internal static class MathEngine
	{
		internal static readonly long[] sin;
		internal static readonly long[] tan;
		internal static readonly long[] asin;
		internal static readonly long[] acos;
		internal static readonly long[] atan;
		internal static readonly long[] sqrt;

		static MathEngine()
		{
			sin = Load("Industry.Simulation.Math.LUT.Sin.bin");
			tan = Load("Industry.Simulation.Math.LUT.Tan.bin");
			asin = Load("Industry.Simulation.Math.LUT.Asin.bin");
			acos = Load("Industry.Simulation.Math.LUT.Acos.bin");
			atan = Load("Industry.Simulation.Math.LUT.Atan.bin");
			sqrt = Load("Industry.Simulation.Math.LUT.Sqrt.bin");
		}

		private static long[] Load(string resourceName)
		{
			var assembly = typeof(MathEngine).Assembly;
			var stream = assembly.GetManifestResourceStream(resourceName);

			byte[] data = new byte[stream.Length];
			stream.CopyTo(new MemoryStream(data));

			long[] copiedData = new long[stream.Length / 8];
			Buffer.BlockCopy(data, 0, copiedData, 0, data.Length);

			return copiedData;
		}
	}
}
