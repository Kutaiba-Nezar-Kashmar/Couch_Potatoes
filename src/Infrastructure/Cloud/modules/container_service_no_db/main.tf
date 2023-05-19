terraform {
  required_providers {
    google = {
      source = "hashicorp/google"
    }
  }
}


resource "google_cloud_run_v2_service" "default" {
  name     = var.service_name
  location = var.gcp_region
  ingress  = "INGRESS_TRAFFIC_ALL"

  template {
    scaling {
      max_instance_count = var.max_instances
    }
    containers {
      image = var.image
      ports {
        container_port = var.port
      }
    }
  }
}
