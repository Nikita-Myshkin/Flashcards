namespace Flashcards;

public class Flashcard
{
    public string? Word {get; set;}
    public string? Definition {get; set;}
    public string? ImagePath {get; set;}
    public string? AudioUrl {get; set;}
    public List<string> Examples {get; set;} = new List<string>();
    public LearningStatus Status { get; set; } = LearningStatus.NotStarted;
}

public enum LearningStatus
{
    NotStarted,
    Learning,
    Learned
}