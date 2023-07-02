namespace NotyficationFunction.Settings;

public class SendGridSettings
{
    public static string Section = "SendGrid";

    public string ApiKey { get; set; }
    public string From { get; set; }
}
