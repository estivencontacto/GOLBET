using AutoMapper;
using GolBet.Entities;
using GolBet.Services.DTOs;

namespace GolBet.Services.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Match, MatchDto>()
            .ForMember(dto => dto.HomeTeamName, options => options.MapFrom(match => match.HomeTeam.Name))
            .ForMember(dto => dto.HomeTeamLogoUrl, options => options.MapFrom(match => match.HomeTeam.LogoUrl))
            .ForMember(dto => dto.AwayTeamName, options => options.MapFrom(match => match.AwayTeam.Name))
            .ForMember(dto => dto.AwayTeamLogoUrl, options => options.MapFrom(match => match.AwayTeam.LogoUrl))
            .ForMember(dto => dto.MatchDate, options => options.MapFrom(match => match.MatchDateUtc))
            .ForMember(dto => dto.BetCount, options => options.MapFrom(match => match.Bets.Count))
            .ForMember(dto => dto.StatusName, options => options.Ignore());
    }
}
