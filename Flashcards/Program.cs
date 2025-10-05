using Flashcards;

var manager = new DataManager("wordslist.json");

var list = new WordList { Name = "My words!"};
list.Cards.Add(new Flashcard
{
    Word = "test",
    Definition = "check something",
    Status = LearningStatus.Learning
});

manager.SaveWordList(list);
Console.WriteLine("Saved!");

var loaded = manager.LoadWordList();
Console.WriteLine($"Loaded: {loaded?.Name}");
Console.WriteLine($"Words: {loaded?.WordCount}");