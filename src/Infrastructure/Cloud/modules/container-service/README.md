# Container Service (No database) module

```csharp
// NOTE: (mibui 2023-05-19) This is an abstraction that enables us to
//                          deploy many of our services without much boilerplate.
//                          This is possible since most of our services are stateless
//                          REST APIs that are exposed publicly
```

Module for container services that do not need a database, e.g. MovieInformation.
It will simply setup a serverless (`Cloud Run`) service instance that can scale down to 0. The service will be configured as a public API with no auth.

## Deploying a new services

Add this to main.tf at the root with the relevant values for the variables for your specific service.

```terraform
 module "container_service_no_db" {
   source       = "./modules/container_service_no_db"
   service_name = "MovieInformation"
   image        = "MOVIEINFORMATION_IMAGE_NAME_IN_CONTAINER_REGISTRY"
   max_instances = 1
   port = 8080
 }
```
