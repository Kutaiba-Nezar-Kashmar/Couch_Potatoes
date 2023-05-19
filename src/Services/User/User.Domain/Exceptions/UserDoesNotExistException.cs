namespace User.Domain.Exceptions;

public class UserDoesNotExistException: Exception
{
    public UserDoesNotExistException(string id, Exception? inner = null): base ($"User with id {id} does not exist", inner)
    {
        
    }
}