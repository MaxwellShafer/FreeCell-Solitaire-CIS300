/* Card.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ksu.Cis300.FreeCell
{
    /// <summary>
    /// An immutable representation of a single card.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The minimum rank of any card.
        /// </summary>
        public static readonly int MinumumRank = 1;

        /// <summary>
        /// The maximum rank of any card.
        /// </summary>
        public static readonly int MaximumRank = 13;

        /// <summary>
        /// The number of suits.
        /// </summary>
        public static readonly int NumberOfSuits = 4;

        /// <summary>
        /// The image file name prefixes of each of the four suits.
        /// </summary>
        private static string[] _filePrefixes =
            { "clubs_", "diamonds_", "hearts_", "spades_" };

        /// <summary>
        /// The name of the directory containing the images.
        /// </summary>
        private const string _imageDirectory = "../../images/";

        /// <summary>
        /// The image file name suffix.
        /// </summary>
        private const string _fileSuffix = ".png";

        /// <summary>
        /// Gets the rank of the card.
        /// </summary>
        public int Rank { get; }

        /// <summary>
        /// Gets the suit of the card.
        /// </summary>
        public Suit Suit { get; }

        /// <summary>
        /// Gets the color of the card.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// The image of this card.
        /// </summary>
        public Image Picture { get; }

        /// <summary>
        /// Constructs a new card representing the given rank and suit.
        /// </summary>
        /// <param name="rank">The rank of the card.</param>
        /// <param name="suit">The suit of the card.</param>
        public Card(int rank, Suit suit)
        {
            if (rank < MinumumRank || rank > MaximumRank || suit < 0 || (int)suit >= NumberOfSuits)
            {
                throw new ArgumentException();
            }
            Rank = rank;
            Suit = suit;
            if (suit == Suit.Diamonds || suit == Suit.Hearts)
            {
                Color = Color.Red;
            }
            else
            {
                Color = Color.Black;
            }
            Picture = Image.FromFile(_imageDirectory + _filePrefixes[(int)suit] + rank + _fileSuffix);
        }
    }
}
