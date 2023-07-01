locals {
  location       = "West Europe"
  short_location = "euw"
}

locals {
  identity            = "identityservices"
  resource_group_name = "${local.identity}rg${local.short_location}"
}