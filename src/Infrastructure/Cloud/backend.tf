terraform {
  backend "gcs" {
    bucket = "couch-potatoes-sep6-bucket-tfstate"
    prefix = "terraform/state"
  }
}
