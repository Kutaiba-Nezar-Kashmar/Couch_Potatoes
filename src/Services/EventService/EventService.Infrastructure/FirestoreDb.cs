using Google.Api.Gax;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace EventService.Infrastructure;

public class FirestoreDb
{
    public static Google.Cloud.Firestore.FirestoreDb GetFirestoreDb()
    {
        // NOTE: (mibui 2023-05-15) allow for overwriting the key path via environment variable for production setting
        var pathToServiceAccountKey =
            Environment.GetEnvironmentVariable("GCP_SERVICE_ACCOUNT_KEY") ?? "serviceAccountKey.json";

        var credentials = GoogleCredential.FromFile(pathToServiceAccountKey);

        return new FirestoreDbBuilder
        {
            ProjectId = "couch-potatoes-sep6",
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction,
            Credential = credentials
        }.Build();
    }
}