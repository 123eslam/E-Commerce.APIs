namespace Shared.Results
{
    public record PaginatedResult<TData>(int pageSize, int pageIndex, int totalCount, IEnumerable<TData> Data)
    {
    }
}
