namespace Industry.Simulation.Math
{
	public struct Constants
	{
		public static readonly Fixed MinValue;
		public static readonly Fixed MaxValue;

		public static readonly Fixed SafeMinValue;
		public static readonly Fixed SafeMaxValue;

		public static readonly Fixed Zero;
		public static readonly Fixed One;
		public static readonly Fixed MinusOne;
		public static readonly Fixed Half;

		public static readonly Fixed Two;
		public static readonly Fixed Three;
		public static readonly Fixed Four;
		public static readonly Fixed Five;

		public static readonly Fixed Pi;
		public static readonly Fixed InversePi;
		public static readonly Fixed PiTimes2;
		public static readonly Fixed PiOver2;
		public static readonly Fixed InversePiOver2;
		public static readonly Fixed PiOver4;
		public static readonly Fixed Pi3Over4;
		public static readonly Fixed Deg2Rad;
		public static readonly Fixed Rad2Deg;

		public static readonly Fixed Rad180;
		public static readonly Fixed Rad90;
		public static readonly Fixed Rad45;

		public static readonly Fixed Epsilon;

		public static readonly Fixed Log2Max;
		public static readonly Fixed Log2Min;
		public static readonly Fixed Ln2;

		static Constants()
		{
			MinValue = new Fixed(long.MinValue);
			MaxValue = new Fixed(long.MaxValue);

			SafeMinValue = -new Fixed(2147483648L);
			SafeMaxValue = -SafeMinValue;

			Zero = 0;
			One = 1;
			Half = One / 2;
			MinusOne = -One;

			Two = 2;
			Three = 3;
			Four = 4;
			Five = 5;

			Pi = new Fixed(205887L);
			InversePi = One / Pi;
			PiTimes2 = new Fixed(411774L);
			PiOver2 = new Fixed(102943L);
			InversePiOver2 = One / PiOver2;
			PiOver4 = new Fixed(51471L);
			Pi3Over4 = new Fixed(154415L);
			Deg2Rad = new Fixed(1143L);
			Rad2Deg = new Fixed(3754936L);

			Rad180 = Pi;
			Rad90 = Rad180 * Half;
			Rad45 = Rad90 * Half;

			Epsilon = One / 1000;

			Log2Max = new Fixed(0x1F00000000);
			Log2Min = new Fixed(-0x2000000000);
			Ln2 = new Fixed(0xB17217F7);
		}
	}
}
