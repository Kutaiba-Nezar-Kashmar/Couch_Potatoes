using Google.Api.Gax;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace EventService.Infrastructure;

public class FirestoreDbReference
{
    public static FirestoreDb GetFirestoreDb()
    {
        var pathToServiceAccountKey = "serviceAccountKey.json";

        var credentials = GoogleCredential.FromFile(pathToServiceAccountKey);

        return new FirestoreDbBuilder
        {
            ProjectId = "couch-potatoes-sep6",
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction,
            Credential = credentials
        }.Build();
    }
}