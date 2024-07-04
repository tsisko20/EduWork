namespace EduWork.WebAPI.Configurations
{
    public record SwaggerOptions
    {
        public const string Section = "SwaggerAzureEntra";
        public string AuthorizationUrl { get; set; } = string.Empty;
        public string TokenUrl {  get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
    }
}
