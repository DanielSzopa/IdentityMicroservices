variable "sendGrid_api_key" {
  type      = string
  sensitive = true
}

locals {
  location       = "West Europe"
  short_location = "euw"
}

locals {
  identity                               = "identityservices"
  resource_group_name                    = "${local.identity}rg${local.short_location}"
  identity_service_plan_name             = "${local.identity}idsp${local.short_location}"
  identity_fapp_storage_account_name     = "${local.identity}idsa${local.short_location}"
  identity_fapp_name                     = "${local.identity}idfapp${local.short_location}"
  notyfication_service_plan_name         = "${local.identity}notsp${local.short_location}"
  notyfication_fapp_storage_account_name = "${local.identity}notsa${local.short_location}"
  notyfication_fapp_name                 = "${local.identity}notfapp${local.short_location}"
}