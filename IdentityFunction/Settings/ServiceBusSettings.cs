namespace IdentityFunction.Settings
{
    public class ServiceBusSettings
    {
        public static string SectionName = "ServiceBus";

        public string PartnerTopic { get; set; }
        public string NewsletterFilterKey { get; set; }
        public string ConnectionString { get; set; }
    }
}
