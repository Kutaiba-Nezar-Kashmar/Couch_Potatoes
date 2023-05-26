namespace User.Application.UpdateProfileInfo.Exceptions;

public class FailedToUpdateUserProfileInfoException: Exception
{
    public FailedToUpdateUserProfileInfoException(string userId, Exception? inner = null) : base($"Failed to update profile for user {userId}", inner)
    {
        
    }
}