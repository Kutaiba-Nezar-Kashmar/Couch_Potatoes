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

resource "aws_s3_bucket" "terraform_state" {
  bucket = "terraform-state-michaelbui99"

  lifecycle {
    prevent_destroy = true
  }
}

resource "aws_s3_bucket_versioning" "terraform_state_versioning" {
  bucket = aws_s3_bucket.terraform_state.bucket
  versioning_configuration {
    status = "Enabled"
  }
}

resource "aws_s3_bucket_server_side_encryption_configuration" "terraform_state_encryption" {
  bucket = aws_s3_bucket.terraform_state.bucket
  rule {
    apply_server_side_encryption_by_default {
      sse_algorithm = "AES256"
    }
  }
}

resource "aws_dynamodb_table" "terraform_locks" {
  name         = "terraform-state-locking-db"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "LockID"

  attribute {
    name = "LockID"
    type = "S"
  }
}

# END REMOTE STATE BACKEND ------------------------------------



# module "container_service_no_db" {
#   source       = "./modules/container_service_no_db"
#   service_name = "MovieInformation"
#   image        = "MOVIEINFORMATION_IMAGE_NAME" # Replace this when we have uploaded the image
# }
