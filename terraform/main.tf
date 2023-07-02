terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.62.0"
    }
  }

  backend "azurerm" {
    resource_group_name  = "identityservicesstaterg"
    storage_account_name = "identityservicesstatesa"
    container_name       = "state"
    key                  = "identity.tfstate"
  }

  required_version = "~> 1.4.6"
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}

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

resource "azurerm_storage_account" "createUser_fapp_storage_account" {
  name                     = local.createUser_fapp_storage_account_name
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_service_plan" "createUser_fapp_service_plan" {
  name                = local.createUser_service_plan_name
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  os_type             = "Windows"
  sku_name            = "Y1"
}

resource "azurerm_windows_function_app" "createUser_fapp" {
  name                       = local.createUser_fapp_name
  resource_group_name        = azurerm_resource_group.rg.name
  location                   = azurerm_resource_group.rg.location
  storage_account_name       = azurerm_storage_account.createUser_fapp_storage_account.name
  storage_account_access_key = azurerm_storage_account.createUser_fapp_storage_account.primary_access_key
  service_plan_id            = azurerm_service_plan.createUser_fapp_service_plan.id

  site_config {
    application_stack {
      dotnet_version              = "v6.0"
      use_dotnet_isolated_runtime = true
    }
  }

  app_settings = {
    "ServiceBus:ConnectionString" = module.serviceBus.servicebus_default_primary_connection_string
  }
}