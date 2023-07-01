resource "azurerm_resource_group" "rg" {
  name     = local.resource_group_name
  location = local.location
}

resource "azurerm_servicebus_namespace" "servicebus_namespace" {
  name                = local.servicebus_namespace_name
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  sku                 = "Standard"
}

resource "azurerm_servicebus_topic" "topic" {
  name                  = "notyfications"
  namespace_id          = azurerm_servicebus_namespace.servicebus_namespace.id
  max_size_in_megabytes = 1024
}

resource "azurerm_servicebus_subscription" "veryficationEmail_subscription" {
  name                                 = local.veryficationEmail_subscription_name
  topic_id                             = azurerm_servicebus_topic.topic.id
  max_delivery_count                   = 1
  dead_lettering_on_message_expiration = true
}

resource "azurerm_servicebus_subscription" "newsletter_subscription" {
  name                                 = local.newsletter_subscription_name
  topic_id                             = azurerm_servicebus_topic.topic.id
  max_delivery_count                   = 1
  dead_lettering_on_message_expiration = true
}

resource "azurerm_servicebus_subscription_rule" "newsletter_subscription_rule" {
  name            = local.newsletter_newsletter_filter_name
  subscription_id = azurerm_servicebus_subscription.newsletter_subscription.id
  filter_type     = "CorrelationFilter"

  correlation_filter {
    properties = {
      isNewsletterSubscriber = "True"
    }
  }
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
    "ServiceBus:ConnectionString" = azurerm_servicebus_namespace.servicebus_namespace.default_primary_connection_string
  }
}