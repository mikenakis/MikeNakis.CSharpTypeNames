#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable CA1852 // Seal internal types

using System.Collections.Generic;

class C0<T1, T2>
{
	public T1 F1 = default!;
	public const string F1Name = nameof( F1 );
	public T2 F2 = default!;
	public const string F2Name = nameof( F2 );
	public ICollection<T2> F2c = default!;
	public const string F2cName = nameof( F2c );

	public class C1A
	{
		public T1 F1 = default!;
		public const string F1Name = nameof( F1 );
		public T2 F2 = default!;
		public const string F2Name = nameof( F2 );
		public ICollection<T2> F2c = default!;
		public const string F2cName = nameof( F2c );
	}

	public class C1B<T3>
	{
		public T1 F1 = default!;
		public const string F1Name = nameof( F1 );
		public T2 F2 = default!;
		public const string F2Name = nameof( F2 );
		public T3 F3 = default!;
		public const string F3Name = nameof( F3 );

		public class C2A
		{
			public T1 F1 = default!;
			public const string F1Name = nameof( F1 );
			public T2 F2 = default!;
			public const string F2Name = nameof( F2 );
			public T3 F3 = default!;
			public const string F3Name = nameof( F3 );

			public class C3A<T4>
			{
				public T1 F1 = default!;
				public const string F1Name = nameof( F1 );
				public T2 F2 = default!;
				public const string F2Name = nameof( F2 );
				public T3 F3 = default!;
				public const string F3Name = nameof( F3 );
				public T4 F4 = default!;
				public const string F4Name = nameof( F4 );
			}
		}

		public class C2B<T4>
		{
			public T1 F1 = default!;
			public const string F1Name = nameof( F1 );
			public T2 F2 = default!;
			public const string F2Name = nameof( F2 );
			public T3 F3 = default!;
			public const string F3Name = nameof( F3 );
			public T4 F4 = default!;
			public const string F4Name = nameof( F4 );
		}
	}
}
