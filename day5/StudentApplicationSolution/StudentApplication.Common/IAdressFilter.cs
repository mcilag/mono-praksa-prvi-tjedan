namespace StudentApplication.Common
{
    public interface IAdressFilter
    {
        string Filter { get; set; }

        string FilterLike(string Filter);
    }
}