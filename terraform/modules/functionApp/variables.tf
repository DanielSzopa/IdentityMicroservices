variable "resource_group_name" {
  type = string
}

variable "resource_group_location" {
  type = string
}

variable "short_location" {
  type = string
}

variable "service_plan_name" {
  type = string
}

variable "fapp_storage_account_name" {
  type = string
}

variable "fapp_name" {
  type = string
}

variable "app_settings" {
  type = map(string)
}
