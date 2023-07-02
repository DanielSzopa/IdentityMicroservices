resource "azurerm_resource_group" "rg" {
  name     = local.resource_group_name
  location = local.location
}

module "serviceBus" {
  source                  = "./modules/serviceBus"
  resource_group_name     = azurerm_resource_group.rg.name
  resource_group_location = azurerm_resource_group.rg.location
  identity_name           = local.identity
  short_location          = local.short_location
}

module "identity_fapp" {
  source                    = "./modules/functionApp"
  resource_group_name       = azurerm_resource_group.rg.name
  resource_group_location   = azurerm_resource_group.rg.location
  short_location            = local.short_location
  fapp_storage_account_name = local.identity_fapp_storage_account_name
  service_plan_name         = local.identity_service_plan_name
  fapp_name                 = local.identity_fapp_name
  app_settings = {
    "ServiceBus:ConnectionString" = module.serviceBus.servicebus_default_primary_connection_string
  }
}
