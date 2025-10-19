using System.Text.Json;

namespace Flashcards;

public class DataManager
{
    private readonly string _filePath;
    
    public DataManager(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveAll(List<WordList> wordList)
    {
        var json = JsonSerializer.Serialize(wordList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<WordList> LoadAll()
    {
        if (!File.Exists(_filePath))
            return new List<WordList>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<WordList>>(json);

        try
        {
            var lists = JsonSerializer.Deserialize<List<WordList>>(json);
            return lists ?? new List<WordList>();
        }
        catch
        {
            return new List<WordList>();
        }
    }
}