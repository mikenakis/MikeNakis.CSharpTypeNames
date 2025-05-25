#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable CA1852 // Type '{}' can be sealed because...

class C0<T1>
{
	public T1 F1 = default!;

	public class C1A
	{
		public T1 F1 = default!;
	}

	public class C1B<T2, T3>
	{
		public T1 F1 = default!;
		public T2 F2 = default!;
		public T3 F3 = default!;

		public class C2A
		{
			public T1 F1 = default!;
			public T2 F2 = default!;
			public T3 F3 = default!;
		}

		public class C2B<T4>
		{
			public T1 F1 = default!;
			public T2 F2 = default!;
			public T3 F3 = default!;
			public T4 F4 = default!;
		}
	}
}
