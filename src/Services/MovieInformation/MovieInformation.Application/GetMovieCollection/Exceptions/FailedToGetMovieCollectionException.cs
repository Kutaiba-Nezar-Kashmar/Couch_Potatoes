namespace MovieInformation.Application.GetMovieCollection.Exceptions;

public class FailedToGetMovieCollectionException: Exception
{
    public FailedToGetMovieCollectionException():base()
    {
        
    }
    
    public FailedToGetMovieCollectionException(string msg): base(msg)
    {
    }
}