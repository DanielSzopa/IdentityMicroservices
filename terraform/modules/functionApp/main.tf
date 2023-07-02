resource "azurerm_storage_account" "storage_acocunt" {
  name                     = var.fapp_storage_account_name
  resource_group_name      = var.resource_group_name
  location                 = var.resource_group_location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_service_plan" "service_plan" {
  name                = var.service_plan_name
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  os_type             = "Windows"
  sku_name            = "Y1"
}

resource "azurerm_windows_function_app" "function_app" {
  name                       = var.fapp_name
  resource_group_name        = var.resource_group_name
  location                   = var.resource_group_location
  storage_account_name       = azurerm_storage_account.storage_acocunt.name
  storage_account_access_key = azurerm_storage_account.storage_acocunt.primary_access_key
  service_plan_id            = azurerm_service_plan.service_plan.id

  site_config {
    application_stack {
      dotnet_version              = "v6.0"
      use_dotnet_isolated_runtime = true
    }
  }

  app_settings = var.app_settings
}