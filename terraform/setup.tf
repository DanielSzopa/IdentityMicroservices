terraform {
  required_providers {
    azurerm = {
        source = "hashicorp/azurerm"
        version = "3.62.0"
    }
  }

  backend "azurerm" {
      resource_group_name = "identityservicesstaterg"
      storage_account_name = "identityservicesstatesa"
      container_name = "state"
      key = "identity.tfstate"
  }

    required_version = "~> 1.4.6"
}

provider "azurerm" {
  features {
    
  }
}