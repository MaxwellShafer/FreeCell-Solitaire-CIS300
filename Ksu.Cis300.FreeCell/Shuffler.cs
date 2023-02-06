/* Shuffler.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.FreeCell
{
    /// <summary>
    /// Contains methods for obtaining a shuffled deck of cards.
    /// </summary>
    public static class Shuffler
    {
        /// <summary>
        /// Gets a new unshuffled deck of cards.
        /// </summary>
        /// <returns>The deck of cards.</returns>
        private static Card[] GetDeck()
        {
            int ranks = Card.MaximumRank - Card.MinumumRank + 1;
            Card[] deck = new Card[ranks * Card.NumberOfSuits];
            int i = 0;
            for (Suit suit = 0; (int)suit < Card.NumberOfSuits; suit++)
            {
                for (int rank = Card.MinumumRank; rank <= Card.MaximumRank; rank++)
                {
                    deck[i] = new Card(rank, suit);
                    i++;
                }
            }
            return deck;
        }

        /// <summary>
        /// Returns a stack containing a deck of cards in a random order determined
        /// by the given seed. 
        /// </summary>
        /// <param name="seed">The seed for random numbers.</param>
        /// <returns>A stack containing the shuffled deck.</returns>
        public static Stack<Card> GetShuffledDeck(int seed)
        {
            Stack<Card> result = new Stack<Card>();
            Random r = new Random(seed);
            Card[] deck = GetDeck();
            for (int i = deck.Length - 1; i >= 0; i--)
            {
                int j = r.Next(i + 1);
                result.Push(deck[j]);
                deck[j] = deck[i];
            }
            return result;
        }

    }
}
