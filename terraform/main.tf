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