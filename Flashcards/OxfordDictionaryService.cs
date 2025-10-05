using System.Text.Json;

namespace Flashcards;

public class OxfordDictionaryService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://od-api-sandbox.oxforddictionaries.com/api/v2";
    
    public OxfordDictionaryService(string appId, string appKey)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("app_id", appId);
        _httpClient.DefaultRequestHeaders.Add("app_key", appKey);
    }

    public async Task<Flashcard?> GetWordDataAsync(string word)
    {
        try
        {
            string url = $"{BaseUrl}/entries/en-us/{word.ToLower()}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string json = await response.Content.ReadAsStringAsync();
            
            var oxfordData = JsonSerializer.Deserialize<OxfordResponse>(json);

            if (oxfordData?.results == null || oxfordData.results.Count == 0)
                return null;
            
            var firstResult = oxfordData.results[0];
            var firstLexical = firstResult.lexicalEntries?[0];
            var firstEntry = firstLexical?.entries?[0];
            var firstSense = firstEntry?.senses?[0];

            string? definition = firstSense?.definitions?[0];
            string? audioUrl = firstEntry.pronunciations?
                .FirstOrDefault(p => p.audioFile != null)?.audioFile;

            var card = new Flashcard()
            {
                Word = word,
                Definition = definition,
                AudioUrl = audioUrl
            };

            if (firstSense?.examples != null)
            {
                foreach (var ex in firstSense.examples)
                {
                    if (ex.text != null)
                        card.Examples.Add(ex.text);
                }
                {
                    
                }
            }
            
            return card;
        }
        catch (Exception ex)
        {
            return null;
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}