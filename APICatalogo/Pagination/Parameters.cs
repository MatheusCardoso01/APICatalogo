namespace APICatalogo.Pagination;

public class Parameters
{
    const int maxPageSize = 50;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = maxPageSize; // só vale se não for definido no request

    public int PageSize
    { 
        get { return _pageSize; }
        set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
    }
}