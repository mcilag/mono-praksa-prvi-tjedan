namespace StudentApplication.Common
{
    public interface IStudentFilter
    {
        string Filter { get; set; }

        string FilterLike(string Filter);
    }
}