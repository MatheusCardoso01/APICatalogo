namespace APICatalogo.Pagination;

public class Parameters
{
    private int _maxPageSize;

    public int PageNumber { get; set; } = 1;

    private int _pageSize;

    public void SetMaxPageSize(int maxPageSize)
    { 
        _maxPageSize = maxPageSize;
    }

    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > _maxPageSize || value == 0) ? _maxPageSize : value; }
    }
}