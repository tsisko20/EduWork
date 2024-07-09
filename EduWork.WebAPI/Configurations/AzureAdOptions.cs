namespace EduWork.WebAPI.Configurations
{
    public record AzureAdOptions
    {
        public const string Section = "AzureAd";
        public string ClientId { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public string ValidateAuthority { get; set; } = string.Empty;
    }
}