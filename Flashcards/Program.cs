using Flashcards;

var oxford = new OxfordDictionaryService(
    "05c2296e",
    "e7b7ae146f787fe665fee7ae88b7db16"
    );

var card = await oxford.GetWordDataAsync("ace");

if (card != null)
{
    Console.WriteLine($"Word: {card.Word}");
    Console.WriteLine($"Definition:  {card.Definition}");
    Console.WriteLine($"Audip: {card.AudioUrl}");
    Console.WriteLine($"Example: ");
    foreach (var ex in card.Examples)
    {
        Console.WriteLine($" - {ex}");
    }
}
    
    
    
    