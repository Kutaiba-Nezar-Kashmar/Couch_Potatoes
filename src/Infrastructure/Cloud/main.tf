# TODO: (mibui 2023-05-15) Setup GCS bucket for remote state and run terraform init to use the backend.
# NOTE: (mibui 2023-05-18) Run 'gcloud auth application-default login' before trying to interact with our cloud

# REMOTE STATE BACKEND  -------------------------------
terraform {
  backend "s3" {
    bucket         = "terraform-state-michaelbui99"
    key            = "global/s3/terraform.tfstate"
    region         = "eu-west-1"
    dynamodb_table = "terraform-state-locking-db"
    encrypt        = true
  }
}

# END REMOTE STATE BACKEND ------------------------------------
variable "TMDB_API_KEY" {
  type      = string
  sensitive = true
}
module "container-service" {
  source        = "./modules/container-service"
  service_name  = "movie-information"
  image         = "docker.io/michaelbui293886/couch-potatoes-movieinformation"
  tmdb_api_key  = var.TMDB_API_KEY
  max_instances = 1
  port          = 80
}
