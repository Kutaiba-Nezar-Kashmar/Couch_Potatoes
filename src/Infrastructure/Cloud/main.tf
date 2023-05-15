# TODO: (mibui 2023-05-15) Setup GCS bucket for remote state and run terraform init to use the backend.
terraform {
  backend "gcs" {
    bucket = "tf-state-prod"
    prefix = "terraform/state"
  }
}

provider "provider" {
  project = "couch-potatoes-sep6"
  region  = "europe-west1"
}

module "container_service_no_db" {
  source       = "./modules/container_service_no_db"
  service_name = "MovieInformation"
  image        = "MOVIEINFORMATION_IMAGE_NAME" # Replace this when we have uploaded the image
}
