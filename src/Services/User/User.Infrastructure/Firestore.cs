using Google.Api.Gax;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace User.Infrastructure;

public static class Firestore
{
    public static FirestoreDb Get()
    {
        // NOTE: (mibui 2023-05-16) allow for overwriting the key path via environment variable for production setting
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