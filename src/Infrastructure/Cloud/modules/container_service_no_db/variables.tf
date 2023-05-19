
variable "image" {
  description = "Container image to run"
  type        = string
}

variable "service_name" {
  description = "Name of the service. Should be in the format movie_information"
  type        = string
}

variable "gcp_region" {
  description = "Region to deploy to"
  type        = string
  default     = "europe-west1"
}

variable "port" {
  description = "Port that the service listens on"
  type        = number
}

variable "max_instances" {
  type        = number
  description = "Max number of instances of the server that can be spun up. Defaults to 1"
  default     = 1
}


variable "tmdb_api_key" {
  type        = string
  sensitive   = true
  description = "TMDB API Key. Only provide if the service needs it"
}
