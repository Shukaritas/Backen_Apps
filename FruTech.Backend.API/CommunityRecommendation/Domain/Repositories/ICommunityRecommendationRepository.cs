namespace FruTech.Backend.API.CommunityRecommendation.Domain.Repositories;

using FruTech.Backend.API.Shared.Domain.Repositories;
using Model.Aggregates;

/// <summary>
/// Repository interface for managing community recommendations.
/// </summary>
public interface ICommunityRecommendationRepository : IBaseRepository<CommunityRecommendation>
{
    // New: update author name in all recommendations with oldUserName
    Task UpdateAuthorNameAsync(string oldUserName, string newUserName);
}