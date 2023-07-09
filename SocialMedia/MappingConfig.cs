using AutoMapper;
using SocialMedia.Models;
using SocialMedia.Models.DTOs;
using SocialMedia.Models.DTOs.Comment;
using SocialMedia.Models.DTOs.Reaction;
using SocialMedia.Models.DTOs.UserRequest;

namespace SocialMedia;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<Post, PostCreatedDTO>().ReverseMap();
        CreateMap<Post, PostUpdatedDTO>().ReverseMap();

        CreateMap<Comment, GetCommentsDTO>().ReverseMap();
        CreateMap<Comment, CreateCommentDTO>().ReverseMap();
        CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
            
        CreateMap<Reaction, GetReactionDTO>().ReverseMap();
        CreateMap<Reaction, CreateReactionDTO>().ReverseMap();

        CreateMap<UserRequest, GetUserRequestDto>().ReverseMap();
            
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<User, BasicUserDto>().ReverseMap();
    }
}