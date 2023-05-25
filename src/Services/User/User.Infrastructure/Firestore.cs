using System.Text;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Api.Gax;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace User.Infrastructure;

public static class Firestore
{
    public static FirestoreDb Get()
    {
        RestoreServiceAccountKeyFromEnvironment();
        // NOTE: (mibui 2023-05-16) allow for overwriting the key path via environment variable for production setting
        var pathToServiceAccountKey =
            Environment.GetEnvironmentVariable("GCP_SERVICE_ACCOUNT_KEY") ?? "./service-account-key.json";

        var credentials = GoogleCredential.FromFile(pathToServiceAccountKey);

        return new FirestoreDbBuilder
        {
            ProjectId = "couch-potatoes-sep6",
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction,
            Credential = credentials
        }.Build();
    }

    public static void CreateFirestoreApp()
    {
        var serviceAccount = Environment.GetEnvironmentVariable("GCP_SERVICE_ACCOUNT_KEY") ??
                             "./service-account-key.json";
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(serviceAccount),
        });
    }

    private static void RestoreServiceAccountKeyFromEnvironment()
    {
        // NOTE: (mibui 2023-05-25) We don't want to ship our images with the GCP service account key, since it is sensitive.
        //                          Instead we restore them from environment variable, if possible.
        var serviceAccountKeyJsonContent = Environment.GetEnvironmentVariable("GCP_SERVICE_ACCOUNT_KEY_JSON");
        if (serviceAccountKeyJsonContent is null || string.IsNullOrEmpty(serviceAccountKeyJsonContent))
        {
            return;
        }
        
        using FileStream file = File.Create("./service-account-key.json");
        try
        {
            byte[] jsonKeyBytes = new UTF8Encoding(true).GetBytes(serviceAccountKeyJsonContent);
            file.Write(jsonKeyBytes, 0, jsonKeyBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to restore GCP Service Account Key: {e}");
        }
    }
}