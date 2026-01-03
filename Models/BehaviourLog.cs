public sealed record BehaviourLog
{
    public string CatName { get; init; } = string.Empty;

    public int BehaviourScore { get; init; }

    public string BehaviourDescription { get; init; } = string.Empty;

    public string DateLogged { get; init; } = DateTime.UtcNow.ToString("dd-MM-yyyy");
}