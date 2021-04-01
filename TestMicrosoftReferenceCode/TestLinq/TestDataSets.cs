using System.Collections.Generic;
using System.Linq;
using MicrosoftReference.Linq;
using NUnit.Framework;


namespace TestMicrosoftReferenceCode.TestLinq
{
    public class TestDataSets
    {
        private CardsDeck _deck;

        [SetUp]
        public void Setup()
        {
            _deck = new CardsDeck();
        }

        [Test]
        public void TestDeckGeneration()
        {
            var allCards = new List<object>();

            foreach (var card in _deck.Deck)
            {
                Assert.That(!allCards.Contains(card));
                allCards.Add(card);
            }

            Assert.AreEqual(CardsDeck.DeckSize, _deck.Deck.Count());
        }

        [Test]
        public void TestSplit()
        {
            var (take, skip) = _deck.Split();
            var takeList = take.ToList();
            var skipList = skip.ToList();

            var intersect = takeList.Intersect(skipList);

            Assert.That(!intersect.Any());
            Assert.That(takeList.Count + skipList.Count == CardsDeck.DeckSize);
        }

        [Test]
        public void TestMiddleSplitReorganizeDeckInEightInterLeaves()
        {
            var iterations = 0;
            var startingDeck = _deck.Deck.ToList();

            do
            {
                _deck.Shuffle();
                iterations++;
            } while (!_deck.Deck.SequenceEquals(startingDeck));

            Assert.AreEqual(8, iterations);
        }
    }
}