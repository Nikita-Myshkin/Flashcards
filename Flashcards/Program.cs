using Flashcards;

var begginnersList = new WordList
{
    Name = "For begginners"
};

var card1 = new Flashcard()
{
    Word = "blame",
    Definition = "to say or think that a person or thing is responsible for something bad",
    Status = LearningStatus.Learning
};
card1.Examples.Add("Don't blame me for your mistakes.");

var card2 = new Flashcard()
{
    Word = "camera",
    Definition = "a device for taking photographs",
    Status = LearningStatus.Learned
};
card2.Examples.Add("I bought a new camera yesterday.");

begginnersList.Cards.Add(card1);
begginnersList.Cards.Add(card2);

Console.WriteLine($"List:  {begginnersList.Name}");
Console.WriteLine($"Created: {begginnersList.CreatedDate}");
Console.WriteLine($"Total words: {begginnersList.WordCount}");
Console.WriteLine("\nWords:");

foreach (var card in begginnersList.Cards)
{
    Console.WriteLine($" - {card.Word} {(card.Status)}");
}