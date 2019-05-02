using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

using Flagscript.Gravatar.TagHelpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

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
			var tagHelper = new GravatarTagHelper();

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
					tagHelperContent.SetContent("fakeemail@flagscript.tech");
					return Task.FromResult<TagHelperContent>(tagHelperContent);
				}
			);

			tagHelper.Alt = "hi";
			tagHelper.Size = 80;
			await tagHelper.ProcessAsync(ctx, output);

			Assert.Equal("img", output.TagName);
			Assert.Contains("s=80", output.Attributes["src"].Value.ToString());
			Assert.Equal("hi", output.Attributes["alt"].Value.ToString());

		}

	}

}
