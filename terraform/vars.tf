locals {
  location       = "West Europe"
  short_location = "euw"
}

locals {
  identity                  = "identityservices"
  resource_group_name       = "${local.identity}rg${local.short_location}"
  servicebus_namespace_name = "${local.identity}sbn${local.short_location}"
}

locals {
  topic_name                          = "notyfications"
  veryficationEmail_subscription_name = "veryficationEmail"
  newsletter_subscription_name        = "newsletter"
  newsletter_newsletter_filter_name   = "newsletterFilter"
}