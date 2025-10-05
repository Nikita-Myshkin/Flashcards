namespace Flashcards;

public class OxfordResponse
{
    public List<OxfordResult>? results { get; set; }
}

public class OxfordResult
{
    public List<LexicalEntry>? lexicalEntries { get; set; }
}

public class LexicalEntry
{
    public List<Entry>? entries { get; set; }
}

public class Entry
{
    public List<Pronunciation>? pronunciations { get; set; }
    public List<Sense>? senses { get; set; }
}

public class Pronunciation
{
    public string? audioFile { get; set; }
}

public class Sense
{
    public List<string>? definitions { get; set; }
    public List<Example>? examples { get; set; }
}

public class Example
{
    public string? text { get; set; }
}