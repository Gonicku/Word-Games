// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.IO;

class WordFunctions
{


    // Methods

    /** chooseFile
     * Determines which file to read from based on the first letter of the typed word;
     Used or word list files
     * @param input: The word the user typed in (String)
     * @return: The name of the chosen file (String)
     * */
    public static string chooseFile(String input)
    {
        string firstLetter = input.Substring(0,1).ToUpper();
        string fileName = Path.Combine(Directory.GetCurrentDirectory(),
        "Dictionary Files\\Word lists in csv\\" + firstLetter + "word.csv");

        return fileName;
    } // chooseFile

    /** chooseDictionaryFile
     * Determines which file to read from based on the first letter of the typed word;
     used for dictionary files
     * @param input: The word the user typed in (String)
     * @return: The name of the chosen file (String)
     * */
    public static string chooseDictionaryFile(String input)
    {
        string firstLetter = input.Substring(0,1).ToUpper();
        string fileName = Path.Combine(Directory.GetCurrentDirectory(),
        "Dictionary Files\\Dictionary in csv\\" + firstLetter + ".csv");

        return fileName;
    } // chooseFile

    /** doesWordExist
     * Determines if the chosen word exists in our dictionary
     * @param input: The word the user typed in (String)
     * @return: Whether or not the word was found in the dictionary (Boolean)
     * */
    public static bool doesWordExist(String input)
    {
        // Select the right file based on the first char of the inputted word
        string filePath = chooseFile(input);
        bool wordExists = false;
        input += " ";
        StreamReader? reader = null;
        Console.WriteLine("Inputted Word: " + input);

        // Read the selected data file
        if(File.Exists(filePath))
        {
            reader = new StreamReader(File.OpenRead(filePath));
            List<string> listA = new List<string>();
            while(!reader.EndOfStream && !wordExists)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                // Adds each word to a list
                foreach(var item in values)
                {
                    listA.Add(item);
                }
                foreach(var column1 in listA)
                {
                    // If the current word matches the input, break the loop
                    if(String.Equals(column1, input, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Word Found!");
                        wordExists = true;
                        break;
                    }
                    //Console.WriteLine(column1); // Writes the current word to the console
                }
            }
        } else
        {
            Console.WriteLine("File '" + filePath + "' doesn't exist.");
        }
        return wordExists;
    } // doesWordExist

    /** doesWordExistTyping
     * Determines if the string of letters the user is typing exists in any of our dictionary's words
     * @param input: The word the user is typing (String)
     * @return: Whether or not the string of letters was found in the dictionary (Boolean)
     * */
    public static bool doesWordExistTyping(String input)
    {
        return false;
    } // doesWordExistTyping

    /** doesWordMatchMeaning
     * Determines if the inputted word matches our dictionary's definition of said word
     * @param input: The word the user typed in (String)
     * @param defInput: The inputted definition (String)
     * @return: Whether or not the inputted definition matches the dictionary's definition (Boolean)
     * */
    public static bool doesWordMatchMeaning(String input, String defInput)
    {
        string word = "";
        string definition = "";
        string quotation = "";
        quotation += (char) 34;
        // Select the right file based on the first char of the inputted word
        string filePath = chooseDictionaryFile(input);
        bool defMatches = false;
        input += " ";
        IEnumerable<string> lines = File.ReadLines(filePath);
        Console.WriteLine("Inputted Word: " + input);

        /* NOTE: This is unfinished, but the idea is to loop through the file until the 
        inputted word is found. Then it reads character by character until it reaches a ')',
        at which point it reads upcoming characters as the definition we seek*/
        foreach(string line in lines)
        {
            //Console.WriteLine("\n Word " + iteration + ": " + line);
            if(!string.IsNullOrEmpty(line)){
                // If the first character in a line is a quotation, ignore the first character
                if(line.Substring(0, 1).Equals(quotation))
                {
                    word = line.Substring(1, line.IndexOf(' '));
                }
                else
                {
                    word = line.Substring(0, line.IndexOf(' '));
                }
                //Console.WriteLine("\nWord: " + word);
                // If the word matches our inputted word, split the line between the word and its definition
                if(word.Equals(input))
                {
                    definition = line.Substring(line.IndexOf(')') + 2);
                    // If the definition found matches our inputted definition, break the loop
                    //Console.WriteLine("Current Word: " + word + ", Current Definition: " + definition);
                    if(definition.Equals(defInput))
                    {
                        defMatches = true;
                        return defMatches;
                    }
                }
            }
        }
        Console.WriteLine("\nDefinition Found: " + definition + "\n");
        return defMatches;
    } // doesWordMatchMeaning

    /** doesWordContainXLetter
     * Determines if the inputted word contains a certain letter
     * @param input: The word the user typed in (String)
     * @param x: The letter to search for (Character)
     * @return: Whether or not the inputted word contains the chosen letter (Boolean)
     * */
    public static bool doesWordContainXLetter(String input, char x)
    {
        bool letterFound = false;
        for(int i = 0; i < input.Length; i++)
        {
            if(char.Equals(input[i], x))
            {
                Console.WriteLine("Letter \'" + x +  "\' found in word.\n");
                letterFound = true;
                return letterFound;
            }
        }

        Console.WriteLine("Letter \'" + x +  "\' not found in given word.\n");
        return letterFound;
    } // doesWordContainXLetter

    /** doesWordContainSuffix
     * Determines if the inputted word contains a certain suffix
     * @param input: The word the user typed in (String)
     * @param suffix: The suffix to search for (String)
     * @return: Whether or not the inputted word contains the chosen suffix (Boolean)
     * */
    public static bool doesWordContainSuffix(String input, String suffix)
    {
        bool suffixExists = false;
        int suffixLength = suffix.Length;
        Console.WriteLine("Inputted Word: " + input + ", Suffix Length: " + suffixLength);

        // If the word contains the inputted suffix, set our boolean to true
        if(String.Equals(input.Substring(input.Length - suffixLength, suffixLength), suffix))
            {
                Console.WriteLine("Suffix \"-" + suffix +  "\" exists in " + input + "\n");
                suffixExists = true;
            }
        // Otherwise, leave it false
        else
            {
                Console.WriteLine(input + " doesn't contain suffix \"-" + suffix + "\"\n");
            }
        return suffixExists;
    } // doesWordContainSuffix

    /** doesWordContainPrefix
     * Determines if the inputted word contains a certain prefix
     * @param input: The word the user typed in (String)
     * @param prefix: The prefix to search for (String)
     * @return: Whether or not the inputted word contains the chosen prefix (Boolean)
     * */
    public static bool doesWordContainPrefix(String input, String prefix)
    {
        bool prefixExists = false;
        int prefixLength = prefix.Length;
        Console.WriteLine("Inputted Word: " + input + ", Prefix Length: " + prefixLength);

        // If the word contains the inputted prefix, set our boolean to true
        if(String.Equals(input.Substring(0, prefixLength), prefix))
            {
                Console.WriteLine("Prefix \"" + prefix + "-\" exists in " + input + "\n");
                prefixExists = true;
            }
        // Otherwise, leave it false
        else
            {
                Console.WriteLine(input + " doesn't contain prefix \"" + prefix + "-\"\n");
            }
        return prefixExists;
    } // doesWordContainPrefix

    /** doesWordContainPhonicSound
     * Determines if the inputted word contains a certain phonic sound
     * @param input: The word the user typed in (String)
     * @param phonicChars: The phonic characters to search for (String)
     * @return: Whether or not the inputted word contains the chosen phonic sound (Boolean)
     * */
    public static bool doesWordContainPhonicSound(String input, String phonicChars)
    {
        return false;
    } // doesWordContainPhonicSound

    /** getIndexofXLetter
     * Determines the index of the chosen letter in the inputted word
     * @param input: The selected word (String)
     * @param x: The letter to search for (Character)
     * @return: The index of the letter in the word; returns -1 if it doesn't exist (Integer)
     * */
    public static int getIndexofXLetter(string input, char x)
    {
        for(int i = 0; i < input.Length; i++)
        {
            if(char.Equals(input[i], x))
            {
                Console.WriteLine("Letter \'" + x + "\' found at index " + i + "\n");
                return i;
            }
        }

        Console.WriteLine("Letter \'" + x + "\' not found in given word.\n");
        return -1;
    } // getIndexofXLetter

    /** generateRandomChar
     * Randomly generates a character
     * @param gridLocation: (Integer)
     * @return: The randomly generated character (Character)
     * */
    public static char generateRandomChar(int gridLocation)
    {
        return '0';
    } // generateRandomChar

    public static void Main(string[] args)
    {
        string quotation = "";
        quotation += (char) 34;
        // Test for "doesWordExist" method
        bool dadada = doesWordExist("mad");
        Console.WriteLine("Word Exists?: " + dadada + "\n");
        // Test for "doesWordContainPrefix" method
        doesWordContainPrefix("Power", "Pow");
        doesWordContainPrefix("Superfluous", "Pow");
        // Test for "doesWordContainSuffix" method
        doesWordContainSuffix("Speed", "eed");
        doesWordContainSuffix("Flight", "eed");
        // Test for "getIndexofXLetter"
        getIndexofXLetter("Sonic", 'i');
        getIndexofXLetter("Sonic", 'k');
        // Test for "doesWordContainXLetter"
        doesWordContainXLetter("Sonic", 'c');
        doesWordContainXLetter("Sonic", 'j');
        // Test for doesWordMatchMeaning
        Console.WriteLine("Definition Matches?: " + doesWordMatchMeaning("Quack", "The cry of the duck, or a sound in imitation of it; a hoarse, quacking noise." + quotation));
    } // Main
}