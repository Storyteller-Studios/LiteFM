namespace LiteFM.Abstractions
{
    public class LastFMOptions
    {
        public required string ApiKey { get; init; }
        public required string ApiSecret { get; init; }
        public bool UseProxy { get; init; } = false;
    }
}
