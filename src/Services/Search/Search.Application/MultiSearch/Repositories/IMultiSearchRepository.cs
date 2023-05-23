using Search.Domain.Models;

namespace Search.Application.MultiSearch.Repositories;

public interface IMultiSearchRepository
{
    Task<MultiSearchResponse> MultiSearch(string query);
}