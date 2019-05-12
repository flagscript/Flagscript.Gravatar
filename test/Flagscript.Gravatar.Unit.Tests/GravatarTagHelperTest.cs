using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

using Flurl.Http.Testing;
using Xunit;
using Xunit.Abstractions;

using Flagscript.Gravatar.TagHelpers;

namespace Flagscript.Gravatar.Unit.Tests
{

	public class GravatarTagHelperTest
	{

		/// <summary>
		/// Output to debug tests.
		/// </summary>
		private ITestOutputHelper Output { get; set; }

		/// <summary>
		/// Constructor taking debug output.
		/// </summary>
		/// <param name="output">Test Output Helper.</param>
		public GravatarTagHelperTest(ITestOutputHelper output)
		{
			Output = output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestTagHelper()
		{
			var config = new GravatarTagHelperConfiguration();
			var tagHelper = new GravatarTagHelper(config);

			var ctx = new TagHelperContext(
				new TagHelperAttributeList{
					{"size", "80"},
					{"alt", new HtmlString("Testing") }
				}, 
				new Dictionary<object, object>(), 
				Guid.NewGuid().ToString("N")
			);

			var output = new TagHelperOutput(
				"gravatar",
				new TagHelperAttributeList(), (useCachedResult, htmlEncoder) =>
				{
					var tagHelperContent = new DefaultTagHelperContent();
					tagHelperContent.SetContent("fakeemail@nodomain.tech");
					return Task.FromResult<TagHelperContent>(tagHelperContent);
				}
			);

			tagHelper.Alt = "hi";
			tagHelper.Size = 80;

			using (var httpTest = new HttpTest())
			{
				var fullJson = File.ReadAllText("proper-gravatar.json");
				httpTest.RespondWith(fullJson, status: 200);
				await tagHelper.ProcessAsync(ctx, output);

			}
							

			Output.WriteLine($"Tag is {output.TagName}");
			Assert.Equal("img", output.TagName);
			Assert.Contains("s=80", output.Attributes["src"].Value.ToString());
			Assert.Equal("hi", output.Attributes["alt"].Value.ToString());

		}

	}

}
