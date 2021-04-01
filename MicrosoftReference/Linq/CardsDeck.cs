using System.Collections.Generic;
using System.Linq;


namespace MicrosoftReference.Linq
{
    public class CardsDeck
    {

        public IEnumerable<object> Deck { get; private set; }

        public static int DeckSize => Suits().Count() * Ranks().Count();

        public CardsDeck()
        {
            Deck = from s in Suits() from r in Ranks() select new { Suit = s, Rank = r };
        }

        private static IEnumerable<string> Suits()
        {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }

        private static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }

        public (IEnumerable<object>, IEnumerable<object>) Split()
        {
            var takeSize = DeckSize / 2;
            return (Deck.Take(takeSize), Deck.Skip(DeckSize - takeSize));
        }

        public void Shuffle()
        {
            var (take, skip) = Split();
            Deck = take.InterleaveSequenceWith(skip);
        }

    }
}