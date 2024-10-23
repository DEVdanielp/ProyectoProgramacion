using Hospital.Web.Core.Pagination;

namespace Hospital.Web.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationRequest request) 
        {
            return query.Skip((request.Page - 1) * request.RecordsPerPage)
                        .Take(request.RecordsPerPage);
        }

    }
}
