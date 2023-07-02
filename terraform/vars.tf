locals {
  location       = "West Europe"
  short_location = "euw"
}

locals {
  identity                             = "identityservices"
  resource_group_name                  = "${local.identity}rg${local.short_location}"
  createUser_service_plan_name         = "${local.identity}cusp${local.short_location}"
  createUser_fapp_storage_account_name = "${local.identity}cusa${local.short_location}"
  createUser_fapp_name                 = "${local.identity}cufapp${local.short_location}"
}