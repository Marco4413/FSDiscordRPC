using DiscordRPC;

namespace FSDiscordRPC
{
    internal class RichPresenceButtonConfig
    {
        public string? Label { get; set; }
        public string? Url { get; set; }

    }

    internal class RichPresenceConfig
    {
        public string? ApplicationId { get; set; }
        public string? Details { get; set; }
        public ulong? StartTimestamp { get; set; }
        public ulong? EndTimestamp { get; set; }
        public string? LargeImageKey { get; set; }
        public string? LargeImageText { get; set; }
        public string? SmallImageKey { get; set; }
        public string? SmallImageText { get; set; }
        public RichPresenceButtonConfig[]? Buttons { get; set; }
        public string? State { get; set; }
        public int? PartySize { get; set; }
        public int? PartyMax { get; set; }

        public RichPresence ToRichPresence()
        {
            return new()
            {
                Details = Details,
                Timestamps = new Timestamps
                {
                    StartUnixMilliseconds = StartTimestamp,
                    EndUnixMilliseconds = EndTimestamp
                },
                Assets = new Assets
                {
                    LargeImageKey = LargeImageKey,
                    LargeImageText = LargeImageText,
                    SmallImageKey = SmallImageKey,
                    SmallImageText = SmallImageText,
                },
                Buttons = Buttons?.Select(b => new Button { Label = b.Label, Url = b.Url }).ToArray(),
                State = State,
                Party = new Party
                {
                    ID = PartySize == null && PartyMax == null ? null : "party-id",
                    Size = PartySize ?? 0,
                    Max = PartyMax ?? 0
                }
            };
        }
    }
}
