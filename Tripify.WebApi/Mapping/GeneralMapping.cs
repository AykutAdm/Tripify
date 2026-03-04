using AutoMapper;
using Tripify.DTOs.AboutDtos;
using Tripify.DTOs.BookingDtos;
using Tripify.DTOs.CategoryDtos;
using Tripify.DTOs.CommentDtos;
using Tripify.DTOs.GuideDtos;
using Tripify.DTOs.TestimonialDtos;
using Tripify.DTOs.TourDtos;
using Tripify.WebApi.Entities;
using Tripify.WebAPI.Entities;

namespace Tripify.WebAPI.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            CreateMap<Tour, ResultTourDto>().ReverseMap();
            CreateMap<Tour, CreateTourDto>().ReverseMap();
            CreateMap<Tour, UpdateTourDto>().ReverseMap();
            CreateMap<Tour, GetTourByIdDto>().ReverseMap();
            CreateMap<Tour, ResultLast4TourDto>().ReverseMap();

            CreateMap<Comment, ResultCommentDto>().ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();
            CreateMap<Comment, UpdateCommentDto>().ReverseMap();
            CreateMap<Comment, GetCommentByIdDto>().ReverseMap();
            CreateMap<Comment, ResultCommentListByTourIdDto>().ReverseMap();

            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, GetAboutByIdDto>().ReverseMap();

            CreateMap<Guide, ResultGuideDto>().ReverseMap();
            CreateMap<Guide, CreateGuideDto>().ReverseMap();
            CreateMap<Guide, UpdateGuideDto>().ReverseMap();
            CreateMap<Guide, GetGuideByIdDto>().ReverseMap();

            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetTestimonialByIdDto>().ReverseMap();

            CreateMap<Booking, ResultBookingDto>().ReverseMap();
            CreateMap<Booking, CreateBookingDto>().ReverseMap();
            CreateMap<Booking, UpdateBookingDto>().ReverseMap();
            CreateMap<Booking, GetBookingByIdDto>().ReverseMap();
        }
    }
}
