# TODO: (mibui 2023-05-15) Setup GCS bucket for remote state and run terraform init to use the backend.
# NOTE: (mibui 2023-05-18) Run 'gcloud auth application-default login' before trying to interact with our cloud

# API GATEWAY ---------------------------------------------------
## NOTE: (mibui 2023-05-24) For more info, read /src/Infrastructure/gateway-bootstrapper/README.md

# output "gateway_url" {
#   value      = google_cloud_run_v2_service.gateway.uri
#   depends_on = [google_cloud_run_v2_service.gateway]
# }
# resource "google_cloud_run_v2_service" "gateway" {
#   name     = "couch-potatoes-api-gateway"
#   location = "europe-west1"
#   ingress  = "INGRESS_TRAFFIC_ALL"

#   template {
#     scaling {
#       max_instance_count = 1
#     }
#     containers {
#       image = "docker.io/michaelbui293886/couch-potatoes-gateway-bootstrapper"
#       env {
#         name  = "GATEWAY_MOVIEINFORMATION_SERVICE"
#         value = module.movieinformation_service.service_url
#       }
#       env {
#         name  = "GATEWAY_USER_SERVICE"
#         value = module.user_service.service_url
#       }
#       env {
#         name  = "GATEWAY_PERSON_SERVICE"
#         value = module.movieinformation_service.service_url
#       }
#       env {
#         name  = "GATEWAY_METRICS_SERVICE"
#         value = module.movieinformation_service.service_url
#       }
#       env {
#         name  = "GATEWAY_SEARCH_SERVICE"
#         value = module.movieinformation_service.service_url
#       }
#       ports {
#         container_port = 8000
#       }
#     }
#   }

# }

# // NOTE: (mibui 2023-05-19) We are using a no auth policy since the API will exposed as public APIs
# data "google_iam_policy" "no_auth_policy" {
#   binding {
#     role = "roles/run.invoker"
#     members = [
#       "allUsers"
#     ]
#   }
# }

# resource "google_cloud_run_v2_service_iam_policy" "service_iam_policy" {
#   name     = google_cloud_run_v2_service.gateway.name
#   project  = google_cloud_run_v2_service.gateway.project
#   location = google_cloud_run_v2_service.gateway.location

#   policy_data = data.google_iam_policy.no_auth_policy.policy_data
# }


# # END API GATEWAY ---------------------------------------------------------------

# # SERVICES ---------------------------------------------------------------------------
# variable "TMDB_API_KEY" {
#   type      = string
#   sensitive = true
# }

# variable "GCP_SERVICE_ACCOUNT_KEY_JSON" {
#   type      = string
#   sensitive = true
# }


# ## Movieinformation ----------------------------
# module "movieinformation_service" {
#   source        = "./modules/container-service"
#   service_name  = "couch-potatoes-movie-information"
#   image         = "docker.io/michaelbui293886/couch-potatoes-movieinformation"
#   tmdb_api_key  = var.TMDB_API_KEY # NOTE: (mibui 2023-05-25) Should be passed by environment variable for security reasons
#   max_instances = 1
#   port          = 80
# }

# ## User ----------------------------
# module "user_service" {
#   source                       = "./modules/container-service"
#   service_name                 = "couch-potatoes-user-service"
#   image                        = "docker.io/michaelbui293886/couch-potatoes-user"
#   tmdb_api_key                 = var.TMDB_API_KEY
#   gcp_service_account_key_json = var.GCP_SERVICE_ACCOUNT_KEY_JSON # NOTE: (mibui 2023-05-25) Should be passed by environment variable for security reasons
#   max_instances                = 1
#   port                         = 80
# }
