using AutoMapper;
using ClubWebsite.Models.Common;
using ClubWebsite.Models.Payment;
using System.IO;
using System.Text;

namespace ClubWebsite.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MemberViewModel, MemberModel>()
                .ForMember(e => e.Picture , m => m.MapFrom(s => new FileInfo(s.Picture.FileName).Extension));
            CreateMap<MemberViewModel, MemberModelTemp>()
                .ForMember(e => e.Picture, m => m.MapFrom(s => new FileInfo(s.Picture.FileName).Extension));
            CreateMap<BkashQueryPaymentReturnModel, BkashExecutePaymentReturnModel>();
        }
    }
}
