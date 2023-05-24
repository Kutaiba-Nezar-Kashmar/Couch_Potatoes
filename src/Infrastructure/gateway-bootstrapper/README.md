# API Gateway Bootstrapper
Dynamically changes `kong.yaml` URLs during container startup based on environment variables.

```javascript
// NOTE: (mibui 2023-05-24) We need to rebuild the image everytime
//                          we add a new service since it needs the
//                          services.json inside the image. Furthermore
//                          We need to ensure that no "URL" is empty for the kong.yaml
//                          that gets created, i.e. every service we declare in services.json,
//                          must have a corresponding GATEWAY_SERVICE_NAME environment variable set
```

## Configuration
Services should be declared in the `services.json` file.

To set the URL for a given service, when it starts up in a container, set an environment variable with the following naming convention: `GATEWAY_SERVICE_NAME`

e.g. `GATEWAY_USER_SERVICE=http://some_url`