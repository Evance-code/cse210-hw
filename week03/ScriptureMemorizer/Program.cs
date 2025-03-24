/*
EXCEEDING REQUIREMENTS:
1. Added support for loading scriptures from a file (scriptures.txt)
2. Implemented smart word hiding - only hides words that aren't already hidden
3. Added a scripture library with 5 default scriptures
*/
class Program
{
    static void Main(string[] args)
    {
        // Initialize scripture
        Scripture scripture = InitializeScripture();

        // Main loop
        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit:");

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3); // Hide 3 words at a time
        }

        // Final display when all words are hidden
        if (scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nAll words are now hidden. Program ending.");
        }
    }

    static Scripture InitializeScripture()
    {
        // Try to load from file
        try
        {
            if (File.Exists("scriptures.txt"))
            {
                string[] lines = File.ReadAllLines("scriptures.txt");
                if (lines.Length > 0)
                {
                    Random random = new Random();
                    string[] parts = lines[random.Next(lines.Length)].Split('|');
                    
                    // Parse reference (supports both single verse and verse ranges)
                    string[] refParts = parts[0].Split(' ');
                    string book = refParts[0];
                    string[] chapterVerse = refParts[1].Split(':');
                    int chapter = int.Parse(chapterVerse[0]);
                    string[] verses = chapterVerse[1].Split('-');

                    if (verses.Length == 1)
                    {
                        return new Scripture(
                            new Reference(book, chapter, int.Parse(verses[0])),
                            parts[1]);
                    }
                    else
                    {
                        return new Scripture(
                            new Reference(book, chapter, int.Parse(verses[0]), int.Parse(verses[1])),
                            parts[1]);
                    }
                }
            }
        }
        catch { /* Fall through to default scripture */ }

        // Default scripture if file loading fails
        return new Scripture(
            new Reference("John", 3, 16),
            "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
    }
}