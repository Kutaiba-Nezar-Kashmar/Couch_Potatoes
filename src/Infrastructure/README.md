```csharp
# NOTE: (mibui 2023-05-20) This is meant for local development. For production we will probably use some offering from GCP since have credits for that.
#                          Otherwise we could also setup a service on GCP that runs the Kong API Gateway image
```

Kong API Gateway listens on `port 8000`. It also exposes a admin service at `port 8001` where we can access different information about the routes we have configured, e.g. `GET http://localhost:8001/user-service/routes`

For this to work properly we need to ensure a `.env` file exists in every service folder and that if you need a GCP Service Account Key, then you must setup a `volume` in the `docker-compose.yaml` of the service. 
