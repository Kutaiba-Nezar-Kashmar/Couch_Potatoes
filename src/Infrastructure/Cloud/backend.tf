terraform {
  backend "gcs" {
    bucket = "couch-potatoes-sep6-bucket-tfstate"
    prefix = "terraform/state"
  }
}

# REMOTE STATE BACKEND  -------------------------------
resource "google_storage_bucket" "default" {
  name          = "couch-potatoes-sep6-bucket-tfstate"
  force_destroy = false
  location      = "EU"
  storage_class = "STANDARD"

  lifecycle {
    prevent_destroy = true
  }
  versioning {
    enabled = true
  }
}
# END REMOTE STATE BACKEND ------------------------------------
