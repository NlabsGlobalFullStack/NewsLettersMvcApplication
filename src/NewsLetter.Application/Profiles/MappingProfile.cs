using AutoMapper;
using NewsLetter.Application.Features.Blogs.Commands.Create;
using NewsLetter.Domain.Entities;

namespace NewsLetter.Application.Profiles;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBlogCommand, Blog>().ReverseMap();
    }
}
