namespace Test.Data.Interfaces
{
    public interface IDaoFactory
    {
        IInstructorDao InstructorDao { get; }
    }
}