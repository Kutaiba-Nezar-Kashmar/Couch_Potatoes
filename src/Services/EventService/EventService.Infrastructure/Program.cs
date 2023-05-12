using Google.Api.Gax;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

var pathToServiceAccountKey = "serviceAccountKey.json";
//Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", pathToServiceAccountKey);

var credentials = GoogleCredential.FromFile(pathToServiceAccountKey);

var db = new FirestoreDbBuilder
{
    ProjectId = "couch-potatoes-sep6",      
    EmulatorDetection = EmulatorDetection.EmulatorOrProduction,
    Credential = credentials
}.Build();