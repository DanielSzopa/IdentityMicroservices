variable "resource_group_name" {
  type = string
}

variable "resource_group_location" {
  type = string
}

variable "identity_name" {
  type = string
}

variable "short_location" {
  type = string
}

locals {
  servicebus_namespace_name = "${var.identity_name}sbn${var.short_location}"
}

locals {
  topic_name                          = "notifications"
  veryficationEmail_subscription_name = "veryficationEmail"
  newsletter_subscription_name        = "newsletter"
  newsletter_newsletter_filter_name   = "newsletterFilter"
}