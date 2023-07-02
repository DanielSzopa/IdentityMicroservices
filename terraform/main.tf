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

module "createUser_fapp" {
  source                               = "./modules/functionApp"
  resource_group_name                  = azurerm_resource_group.rg.name
  resource_group_location              = azurerm_resource_group.rg.location
  short_location                       = local.short_location
  createUser_fapp_storage_account_name = local.createUser_fapp_storage_account_name
  createUser_service_plan_name         = local.createUser_service_plan_name
  createUser_fapp_name                 = local.createUser_fapp_name
  app_settings = {
    "ServiceBus:ConnectionString" = module.serviceBus.servicebus_default_primary_connection_string
  }
}
