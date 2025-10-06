using Flashcards;

var dataManager = new DataManager("wordlist.json");
var cardManager = new CardManager(dataManager);

cardManager.LoadLists();

WordList myList;

if (cardManager.GetAllWordLists().Count == 0)
{
    Console.WriteLine("Creating new word list");
    myList = cardManager.CreateNewWordList("B1 Level Words");

    var card1 = cardManager.CreateCard(
        "blame",
        "to say or think that a person or thing is responsible for something bad that has happened",
        new List<string>
        {
            "Don't blame me - it's not my fault",
            "She blamed herself for the accident"
        }
    );
    
    var card2 = cardManager.CreateCard(
        "convince",
        "to make somebody/yourself believe that something is true",
        new List<string>
        {
            "I'd convinced myself that I was right",
            "She convinced him to stay"
        }
    );
    
    var card3 = cardManager.CreateCard(
        "ancient",
        "belonging to a period of history that is thousands of years in the past",
        new List<string>
        {
            "ancient history",
            "ancient Rome"
        }
    );
    
    cardManager.AddCardToList(myList, card1);
    cardManager.AddCardToList(myList, card2);
    cardManager.AddCardToList(myList, card3);
    
    cardManager.SaveAllLists();
    Console.WriteLine($"Created new list: {myList.Cards.Count} cards\n");
}
else
{
    myList = cardManager.GetAllWordLists()[0];
    Console.WriteLine($"Loaded list: {myList.Name} {myList.Cards.Count} cards");
}

Console.WriteLine("--- Starting Learning Session ---\n");
var session = new LearningSession(myList);

if (session.TotalCards == 0)
{
    Console.WriteLine("No cards to learn! All cards are already learned.");
    return;
}

while (session.CurrentCard != null)
{
    var card = session.CurrentCard;
    
    Console.WriteLine($"Card {session.TotalCards - session.RemainingCards + 1}/{session.TotalCards}");
    Console.WriteLine($"\nDefinition: {card.Definition}");

    if (card.Examples.Count > 0)
    {
        Console.WriteLine("\nExamples");
        foreach (var example in card.Examples)
        {
            Console.WriteLine($"  • {example}");
        }
    }
    
    Console.WriteLine("\nType the word");
    string? answer = Console.ReadLine();

    if (answer?.Trim().ToLower() == card.Word?.ToLower())
    {
        Console.WriteLine("✓ Correct!\n");
        session.MarkCorrect();
    }
    else
    {
        Console.WriteLine($"✗ Wrong! The word was: {card.Word}\n");
        session.MarkIncorrect();
    }
}

Console.WriteLine("--- Session Complete ---");
var failed = session.GetFailedCards();

if (failed.Count > 0)
{
    Console.WriteLine($"\nWords to review ({failed.Count}):");
    foreach (var card in failed)
    {
        Console.WriteLine($"  • {card.Word}");
    }
}
else
{
    Console.WriteLine("\n🎉 Perfect! You got all cards correct!");
}
cardManager.SaveAllLists();
Console.WriteLine("\nProgress saved.");