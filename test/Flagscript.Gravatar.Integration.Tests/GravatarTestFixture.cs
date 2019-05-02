namespace Flagscript.Gravatar.Integration.Tests
{

	/// <summary>
	/// Test Fixture for the various gravatar unit tests.
	/// </summary>
	public class GravatarTestFixture
	{

		/// <summary>
		/// <see cref="GravatarLibrary"/> to use during testing.
		/// </summary>
		/// <value><see cref="GravatarLibrary"/> to use during testing</value>
		public GravatarLibrary Library { get; private set; }

		/// <summary>
		/// Contructor initializing test fixture.
		/// </summary>
		public GravatarTestFixture()
		{

			Library = new GravatarLibrary();

		}

	}

}
