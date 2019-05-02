using System;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Flagscript.Gravatar.Integration.Tests
{

	/// <summary>
	/// Integration Tests for <see cref="GravatarLibrary"/>.
	/// </summary>
    public class GravatarLibraryTest : IClassFixture<GravatarTestFixture>
	{

		/// <summary>
		/// The test fixture.
		/// </summary>
		/// <value>The test fixture</value>
		public GravatarTestFixture TestFixture { get; private set; }

		/// <summary>
		/// Output to debug tests.
		/// </summary>
		private ITestOutputHelper Output { get; set; }

		/// <summary>
		/// Constructor taking test fixture.
		/// </summary>
		/// <param name="testFixture">Test Fixture.</param>
		/// <param name="output">Test Output Helper.</param>
		public GravatarLibraryTest(GravatarTestFixture testFixture, ITestOutputHelper output)
		{
			TestFixture = testFixture;
			Output = output;
		}

		/// <summary>
		/// Integration Test for <see cref="GravatarLibrary.GetGravatarProfile(string)"/>.
		/// </summary>
		[Fact]
        public async Task TestGravatarLibraryGetProfile()
        {

			var testEmail = Environment.GetEnvironmentVariable("TEST_EMAIL");
			var emailProfile = await TestFixture.Library.GetGravatarProfile(testEmail);
			Assert.Equal("Kaestle", emailProfile.LastName);

        }

    }

}
