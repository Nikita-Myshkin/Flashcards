using System.Text.Json;

namespace Flashcards;

public class DataManager
{
    private readonly string _filePath;
    
    public DataManager(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveWordList(WordList wordList)
    {
        string json = JsonSerializer.Serialize(wordList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public WordList LoadWordList()
    {
        if (!File.Exists(_filePath))
            return null;
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<WordList>(json);
    }
}