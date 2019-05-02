using System;
using System.IO;
using System.Threading.Tasks;

using Flurl.Http.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Flagscript.Gravatar.Unit.Tests
{

	/// <summary>
	/// Unit Tests for <see cref="GravatarLibrary"/>.
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
		/// Test <see cref="GravatarLibrary.GenerateEmailHash(string)" /> with bad parameters.
		/// </summary>
		[Fact]
        public void TestGenerateEmailHashNullEmpty()
        {

			try 
			{
				TestFixture.Library.GenerateEmailHash(null);
				Assert.False(true, "GenerateEmailHash with null email did not exception");
			}
			catch (ArgumentNullException ae)
			{
				Assert.Equal("email", ae.ParamName);
			}

			try
			{
				TestFixture.Library.GenerateEmailHash("  ");
				Assert.False(true, "GenerateEmailHash with empty email did not exception");
			}
			catch (ArgumentNullException ae)
			{
				Assert.Equal("email", ae.ParamName);
			}

		}

		/// <summary>
		/// Test <see cref="GravatarLibrary.GenerateEmailHash(string)" /> produces lower case 
		/// email hash.
		/// </summary>
		[Fact]
		public void TestGenerateEmailHash()
		{

			var testEmail = "FakeEmail@nodomain.tech";

			var emailHash = TestFixture.Library.GenerateEmailHash(testEmail);
			var lowerEmailHash = TestFixture.Library.GenerateEmailHash(testEmail.ToLower());

			Assert.Equal(lowerEmailHash, emailHash);

		}

		/// <summary>
		/// Test <see cref="GravatarLibrary.GetGravatarProfile(string)"/> when user is not found
		/// in Gravatar.
		/// </summary>
		[Fact]
		public async Task TestGetGravatarProfileNotFound()
		{
			using (var httpTest = new HttpTest())
			{
				var fakeEmail = "fakeemail@nodomain.tech";
				httpTest.RespondWith("\"User not found\"", status: 404);
				try 
				{
					var profile = await TestFixture.Library.GetGravatarProfile(fakeEmail);
					Assert.False(true, "GetGravatarProfile did not exception for non-existant gravatar");
				}
				catch (GravatarNotFoundException gnfe)
				{
					Assert.Contains(fakeEmail, gnfe.Message);
				}				
			}
		}

		/// <summary>
		/// Test <see cref="GravatarLibrary.GetGravatarProfile(string)"/> when gravatar does not contain
		/// name information. Required in Flagscript.
		/// </summary>
		[Fact]
		public async Task TestGetGravatarProfileBaseJson()
		{

			using (var httpTest = new HttpTest())
			{
				try 
				{

					var fakeEmail = "fakeemail@nodomain.tech";
					var slimJson = File.ReadAllText("empty-gravatar.json");
					httpTest.RespondWith(slimJson, status: 200);
					var profile = await TestFixture.Library.GetGravatarProfile(fakeEmail);
					Assert.False(true, "Empty Gravitar did not exception");

				}
				catch (FlagscriptException fe)
				{
					Assert.Contains("does not contain any name information", fe.Message);
				}
			}

		}

		/// <summary>
		/// Test <see cref="GravatarLibrary.GetGravatarProfile(string)"/>.
		/// </summary>
		[Fact]
		public async Task TestGetGravatarProfileFullJson()
		{

			using (var httpTest = new HttpTest())
			{
				var fakeEmailNonNormalize = "    FakeEmaiL@NoDomain.tecH    ";
				var fakeEmailHash = TestFixture.Library.GenerateEmailHash(fakeEmailNonNormalize);
				Output.WriteLine("=>     " + fakeEmailHash);

				var fakeEmail = "fakeemail@nodomain.tech";
				var fullJson = File.ReadAllText("proper-gravatar.json");
				httpTest.RespondWith(fullJson, status: 200);
				var profile = await TestFixture.Library.GetGravatarProfile(fakeEmailNonNormalize);
				Assert.Equal(fakeEmail, profile.Email);
				Assert.Equal("first", profile.FirstName);
				Assert.Equal("last", profile.LastName);
				Assert.Equal("full", profile.FullName);
				Assert.Equal("unit test", profile.About);
				Assert.Contains("gravatar.com", profile.ProfileUrl);

			}

		}


	}

}
