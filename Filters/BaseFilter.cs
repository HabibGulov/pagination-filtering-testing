public record BaseFilter
{
    public int PageSize{get; set;}
    public int PageNumber{get; set;}

    public BaseFilter()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public BaseFilter(int pageSize, int pageNumber)
    {
        if(pageSize<=0) this.PageSize=10;
        this.PageSize=pageSize;
        if(pageNumber<=0) this.PageNumber=1;
        this.PageNumber=pageNumber;
    }
}