using AutoMapper;

using Flagscript.Gravatar.Integration;

namespace Flagscript.Gravatar.Mapping
{

	/// <summary>
	/// Automapper profile for Gravatar Response to Library Return mappigs.
	/// </summary>
	public class GravatarMappingProfile : Profile
	{

		/// <summary>
		/// Automapper profile name.
		/// </summary>
		/// <value>Automapper profile name.</value>
		public override string ProfileName => "GravatarMappingProfile";

		/// <summary>
		/// Profile constructor with mapping information.
		/// </summary>
		public GravatarMappingProfile()
		{

			CreateMap<EntryResponse, GravatarProfile>()
				.ForMember(d => d.About, o => o.MapFrom(s => s.AboutMe)) 
				.ForMember(d => d.ProfileUrl, o => o.MapFrom(s => s.ProfileUrl))
				.ForMember(d => d.ThumbnailUrl, o => o.MapFrom(s => s.ThumbnailUrl))
				.ForMember(d => d.FirstName, o => o.ResolveUsing(s => s.Name?.GivenName))
				.ForMember(d => d.FullName, o => o.ResolveUsing(s => s.Name?.Formatted))
				.ForMember(d => d.LastName, o => o.ResolveUsing(s => s.Name?.FamilyName));

		}

	}

}
