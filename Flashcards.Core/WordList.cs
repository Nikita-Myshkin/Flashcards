namespace Flashcards;

public class WordList
{
    public string? Name { get; set; }
    public List<Flashcard> Cards { get; set; } = new List<Flashcard>();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    public int WordCount => Cards.Count;
}