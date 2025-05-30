#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable CA1852 // Seal internal types

class C0<T1, T2>
{
	public T1 F1 = default!;
	public T2 F2 = default!;
	public ICollection<T2> F2c = default!;

	public class C1A
	{
		public T1 F1 = default!;
		public T2 F2 = default!;
		public ICollection<T2> F2c = default!;
	}

	public class C1B<T3>
	{
		public T1 F1 = default!;
		public T2 F2 = default!;
		public T3 F3 = default!;
		public ICollection<T3> F3c = default!;

		public class C2A
		{
			public T1 F1 = default!;
			public T2 F2 = default!;
			public T3 F3 = default!;
			public ICollection<T3> F3c = default!;

			public class C3A<T4>
			{
				public T1 F1 = default!;
				public T2 F2 = default!;
				public T3 F3 = default!;
				public T4 F4 = default!;
				public ICollection<T4> F4c = default!;
			}
		}

		public class C2B<T4>
		{
			public T1 F1 = default!;
			public T2 F2 = default!;
			public T3 F3 = default!;
			public T4 F4 = default!;
			public ICollection<T4> F4c = default!;
		}
	}
}
