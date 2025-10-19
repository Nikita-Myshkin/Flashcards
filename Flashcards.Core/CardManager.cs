namespace Flashcards;

public class CardManager
{
    private readonly DataManager _dataManager;
    private List<WordList> _allWordList;

    public CardManager(DataManager dataManager)
    {
        _dataManager = dataManager;
        _allWordList =  new List<WordList>();
    }

    public Flashcard CreateCard(string word, string definition, List<string>? examples = null, string? imagePath = null)
    {
        var card = new Flashcard()
        {
            Word = word,
            Definition = definition,
            ImagePath = imagePath,
            Examples = examples ?? new List<string>()
        };
        return card;
    }

    public void AddCardToList(WordList list, Flashcard card)
    {
        list.Cards.Add(card);
    }

    public WordList CreateNewWordList(string name)
    {
        var newList = new WordList() { Name = name };
        _allWordList.Add(newList);
        return newList;
    }

    public List<WordList> GetAllWordLists() => _allWordList;

    public void SaveAllLists()
    {
        _dataManager.SaveAll(_allWordList);
    }

    public void LoadLists()
    {
        _allWordList = _dataManager.LoadAll();
    }
}

public class LearningSession
{
    private readonly WordList _wordList;
    private Queue<Flashcard> _currentCards;
    private List<Flashcard> _failedCards;
    
    public int TotalCards {get; private set;}
    public int RemainingCards => _currentCards.Count;
    public Flashcard? CurrentCard { get; private set; }

    public LearningSession(WordList wordList)
    {
        _wordList = wordList;
        _currentCards = new Queue<Flashcard>();

        foreach (var card in wordList.Cards)
        {
            if (card.Status != LearningStatus.Learned)
            {
                _currentCards.Enqueue(card);
            }
        }
        
        _failedCards = new List<Flashcard>();
        TotalCards = _currentCards.Count;

        NextCard();
    }

    public void NextCard()
    {
        if (_currentCards.Count > 0)
        {
            CurrentCard = _currentCards.Dequeue();
        }
        else
        {
            CurrentCard = null;
        }
    }

    public void MarkCorrect()
    {
        if (CurrentCard == null) return;
        
        _failedCards.Add(CurrentCard);
        CurrentCard.Status = LearningStatus.Learned;
        NextCard();
    }

    public void MarkIncorrect()
    {
        if (CurrentCard == null) return;
        _failedCards.Add(CurrentCard);
        CurrentCard.Status = LearningStatus.Learning;
        NextCard();
    }
    
    public List<Flashcard> GetFailedCards() => _failedCards;
}