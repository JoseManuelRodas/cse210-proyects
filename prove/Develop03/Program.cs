using System;
using System.Collections.Generic;

namespace ScriptureAppForW3
{
    class Program
    {
        static void Main(string[] args)
        {
        Console.Clear();

            var scriptures = new List<Scripture>
            {
                new Scripture("joshua 1:9", "Have not I commanded thee? Be strong and of a good courage; be not afraid, neither be thou dismayed: for the aLord thy God is with thee whithersoever thou goest."),
                new Scripture("2 Nefi 32:5", "For behold, again I say unto you that if ye will enter in by the way, and receive the Holy Ghost, it will ashow unto you all things what ye should do."),
                new Scripture("3 Nefi 28:11", "And the aHoly Ghost beareth record of the Father and me; and the Father giveth the Holy Ghost unto the children of men, because of me."),
                new Scripture("Mormón 5:23", "Know ye not that ye are in the ahands of God? Know ye not that he hath all power, and at his great command the bearth shall be crolled together as a scroll?.")
            };

            Console.WriteLine(" Welcome to the Scripture Memorizer! \n Please select an scripture with the index number:");
            Console.WriteLine("\n 1.- joshua 1:9 \n 2.- 2 Nefi 32:5 \n 3.- Nefi 28:11 \n 4.- Mormon 5:23");


            int selectedIndex = int.Parse(Console.ReadLine()) - 1;
            var selectedScripture = scriptures[selectedIndex];

            var game = new WordHidingGame(selectedScripture);

            while (!game.AllWordsHidden)
            {
                Console.Clear();
                game.DisplayScripture();
                Console.WriteLine("\n \n Press Enter to hide more words or type 'quit' to exit.");
                if (Console.ReadLine() == "quit")
                    break;
                game.HideRandomWord();
            }

            Console.WriteLine("\nThank you for playing! Press any key to exit.");
            Console.ReadKey();
        }
    }

    class Scripture
    {
        public string Reference { get; }
        public string Text { get; }

        public Scripture(string reference, string text)
        {
            Reference = reference;
            Text = text;
        }
    }

    class WordHidingGame
    {
        private readonly Scripture _scripture;
        private readonly List<string> _hiddenWords;

        public bool AllWordsHidden => _hiddenWords.Count == _scripture.Text.Split().Length;

        public WordHidingGame(Scripture scripture)
        {
            _scripture = scripture;
            _hiddenWords = new List<string>();
        }

        public void DisplayScripture()
        {
            Console.WriteLine($"\n{_scripture.Reference}:\n");
            var words = _scripture.Text.Split();
            foreach (var word in words)
            {
                if (_hiddenWords.Contains(word))
                    Console.Write("____ ");
                else
                    Console.Write($"{word} ");
            }
        }

        public void HideRandomWord()
        {
            var words = _scripture.Text.Split().Where(w => !_hiddenWords.Contains(w)).ToList();
            if (words.Count > 0)
            {
                var random = new Random();
                var randomIndex = random.Next(words.Count);
                _hiddenWords.Add(words[randomIndex]);
            }
        }
    }
}
