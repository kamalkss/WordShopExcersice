using WordShopExcersice.Data.Models;
using WordShopExcersice.WebApi.DTO;

namespace WordShopExcersice.WebApi.Profile;

public class PostCodeProfile : AutoMapper.Profile
{
    public PostCodeProfile()
    {
        CreateMap<CreatePostCodeDTO, PostCodeModel>().ReverseMap();
        CreateMap<PostCodeModel, ReadPostCodeDTO>().ReverseMap();
    }
}