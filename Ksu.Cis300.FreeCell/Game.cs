/* Game.cs
 * Author: Rod Howell
 * Edited by: Max Shafer
 * 
 * 
 * 
 * GRADER NOTE! sorry for my weird comenting on each line please ignore it
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace Ksu.Cis300.FreeCell
{
    /// <summary>
    /// Conrols the game logic for a FreeCell game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The number of free cells.
        /// </summary>
        private const int _freeCellCount = 4;

        /// <summary>
        /// The number of home cells.
        /// </summary>
        private const int _homeCellCount = 4;

        /// <summary>
        /// The number of tableau columns.
        /// </summary>
        private const int _tableauColumnCount = 8;

        /// <summary>
        /// number of miliseconds paused for animations
        /// </summary>
        private const int _mili = 35;
        /// <summary>
        /// keeps track of free cells
        /// </summary>
        Stack<CardLocation> _freeCellStack = new Stack<CardLocation>(_freeCellCount);
        /// <summary>
        ///  keeps track of the selected FreeCell
        /// </summary>
        CardLocation _selectedFreeCell = null;
        /// <summary>
        /// keeps track of the selected column
        /// </summary>
        TableauColumn _selectedColumn = null;



        /// <summary>
        /// Gets the free cells.
        /// </summary>
        public CardLocation[] FreeCells { get; } = new CardLocation[_freeCellCount];

        /// <summary>
        /// Gets the home cells.
        /// </summary>
        public CardLocation[] HomeCells { get; } = new CardLocation[_homeCellCount];

        /// <summary>
        /// Gets the tableau columns.
        /// </summary>
        public TableauColumn[] TableauColumns { get; } = new TableauColumn[_tableauColumnCount];

        /// <summary>
        /// Constructs a Game.
        /// </summary>
        public Game() 
        { 
            for (int i = 0; i < FreeCells.Length; i++)
            {
                FreeCells[i] = new CardLocation();
            }
            for (int i = 0; i < HomeCells.Length; i++)
            {
                HomeCells[i] = new CardLocation();
            }
            for (int i = 0; i < TableauColumns.Length; i++)
            {
                TableauColumns[i] = new TableauColumn();
            }
        }

        /// <summary>
        /// Initializes the free cells
        /// </summary>
        private void StartFreeStack()
        {
            _freeCellStack.Clear(); // clears the free cells out
            for (int i = 0; i < _freeCellCount; i++) // for each of the free cells
            {
                _freeCellStack.Push(FreeCells[i]); // add the location stored in the array
            }
         
        }

        /// <summary>
        /// removes a card location from the free cell stack
        /// </summary>
        /// <param name="loc"> is the card you want to remove from the stack</param>
        
  

        private void RemoveCardInFreeStack(CardLocation loc) 
        {
            
                Stack<CardLocation> tempStack = new Stack<CardLocation>();

                for (int i = 0; i < _freeCellCount; i++)
                {
                try
                {
                    if (_freeCellStack.Peek() == loc)
                    {
                        _freeCellStack.Pop();              // take off the found element from the free cell stack
                    }
                    else
                    {
                        tempStack.Push(_freeCellStack.Pop()); // put the free cells in the temp stack
                    }

                }
                catch { };
                
                    
                }
                try                               //to catch the stack empty from pop
                {
                    for (int i = 0; i < _freeCellCount; i++) // loop through and put the free cell stack back into place.
                    {
                        _freeCellStack.Push(tempStack.Pop());
                    }
                }
                catch { }   
            
        }

        /// <summary>
        /// Add card to A free cell or A home cell.
        /// </summary>
        /// <param name="loc">the location</param>
        /// <param name="card">the card</param>
        public void AddCard(CardLocation loc, Card card)
        {
            loc.Card = card;                    /// put card in loc
            Redraw(loc);                        //update drawing
            RemoveCardInFreeStack(loc);         //remove the CardLocation from the stack of empty free cells using
                                                //the appropriate method above.
        }

        /// <summary>
        /// redaws a location
        /// </summary>
        /// <param name="c">the control</param>
        static void Redraw(Control c) // redraw the control/ diffrent card locations?
        {
            c.Refresh();
            Thread.Sleep(_mili);
        }

        /// <summary>
        /// redraws two locations
        /// </summary>
        /// <param name="c">location 1</param>
        /// <param name="c2">location 2</param>
        private void Redraw(Control c, Control c2)
        {
            c.Refresh();
            c2.Refresh();
            Thread.Sleep(_mili);

        }

        /// <summary>
        /// add card to a column
        /// </summary>
        /// <param name="column"> which column to add to</param>
        /// <param name="card">which card to add</param>
        static void AddCardToColumn(TableauColumn column, Card card)
        {
            column.Column.Push(card);       // add card to column
            Redraw(column);
        }

        /// <summary>
        /// remove a card from the array of free cells
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        private Card RemoveCardFromFreeCell(CardLocation loc)
        {
            Card temp = loc.Card;       // store the card
            loc.Card = null;            // clear the card from the location
            
            _freeCellStack.Push(loc);   // push that same location back to the free stack,
            Redraw(loc);                //update the animation
            return temp;                // because now it does not have a card there and is free

        }

        /// <summary>
        /// removes a card from a column, returns the card removed presumably so it can be put somwhere else.
        /// </summary>
        /// <param name="column"></param>
        /// <returns>the card removed</returns>
        private static Card RemoveCardFromColumn(TableauColumn column)
        {
            Card card = column.Column.Pop();
            column.Refresh();
            return card;                // return card that is removed
            
        }


        /// <summary>
        /// clear a card locataion, either a home cell or free i think?
        /// </summary>
        /// <param name="loc"></param>
        private static void ClearCardLoc(CardLocation[] loc)
        {
            foreach (CardLocation card in loc) //look at each location
            {
                card.Card = null;               //set the card at that location to null
                card.Refresh();
            }
        }

        /// <summary>
        /// clear the tableau columns
        /// </summary>
        private void ClearColumns()
        {
            for (int i = 0; i < _tableauColumnCount; i++) // for each collum
            {
                TableauColumns[i].Column.Clear(); //clear it out
            }

        }

        /// <summary>
        /// deals a set of cards to the columns
        /// </summary>
        /// <param name="seed"> an int that randomizes cards dealt</param>
        private void DealCards(int seed)
        {
            Stack<Card> Deck = Shuffler.GetShuffledDeck(seed); // new shuffled deck

            while (Deck.Count > 0) // iterate till the deck is empty
            {

                for (int i = 0; i < _tableauColumnCount; i++) // to each collum add 1 card from the deck
                {
                    try
                    {
                        TableauColumns[i].Column.Push(Deck.Pop());
                        Redraw(TableauColumns[i]);
                    }
                    catch { } // to catch the empty exception error from the last pop


                }
            }
        }

        /// <summary>
        /// Starts a new game using the given seed.
        /// </summary>
        /// <param name="seed">The seed to use to shuffle the cards.</param>
        public void StartNewGame(int seed)
        {
            ClearCardLoc(FreeCells);
            ClearCardLoc(HomeCells);
            ClearColumns();
            StartFreeStack();
            DealCards(seed);
        }

    
        /// <summary>
        /// takes a location and sets that card to the global selected and changes its property to true
        /// </summary>
        /// <param name="loc">the selected card loc</param>
        public void SelectFreeCell(CardLocation loc) 
        {

            try
            {
                loc.IsSelected = true;
                _selectedFreeCell = loc;
                loc.Refresh(); //****
            }
            catch(InvalidOperationException) { }
            
        }

        /// <summary>
        /// Selects a column
        /// </summary>
        /// <param name="column">the column to be selcted</param>
        /// <param name="numCards">the amount of cards being selected</param>
        public void SelectColumn(TableauColumn column, int numCards)
        {
            
            column.NumberSelected = numCards;    // selects the stack of cards
            _selectedColumn = column;            // set the cards selected  feild to the location of the column            
            column.Refresh();                    //update border
        }

        /// <summary>
        ///  deselect a card in a free cell
        /// </summary>
        /// <returns>the card deslected presumably to put it somewhere else</returns>
        public CardLocation DeselectFreeCell()          // ** NOTE ** Might need to add refresh to method?
        {
            CardLocation temp = _selectedFreeCell;
            _selectedFreeCell.IsSelected = false;       //change the property of the card
            _selectedFreeCell = null;                   // clear the global var
            temp.Refresh();
            return temp;
        }
        /// <summary>
        /// Deselects a column 
        /// </summary>
        /// <returns>the column deselected</returns>
        public TableauColumn DeselectColumn()           // ** NOTE ** Might need to add refresh to method?
        {
            TableauColumn temp = _selectedColumn;       //store the current column
            //_selectedColumn.NumberSelected = 0;         **TAKING OUT TEMP DOC DOES NOT MENTION ** reset the num selected property                     
            _selectedColumn = null;                     // clear global var
            temp.Refresh();
            return temp;                                //return stored column

            //** NOTE ** DOCUMENTATION SAYS TO RETURN THE CARDS THAT WERE SELECTED NOT WHOLE STACK check back here
        }

        /// <summary>
        /// returns if you can stack a card on a collum
        /// </summary>
        /// <param name="selectedCard"> the selected card</param>
        /// <param name="cardOnColumn">the card you are trying to place on</param>
        /// <returns></returns>
        static bool StackableColumn(Card selectedCard, Card cardOnColumn)
        {
            if(selectedCard.Rank == cardOnColumn.Rank - 1 && selectedCard.Color != cardOnColumn.Color) 
            {                       // if the card is one rank lower, and is a diffrent color
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// returns true if you can place selectd card on a home tile
        /// </summary>
        /// <param name="selectedCard">the selected card</param>
        /// <param name="homeCell">the home cell</param>
        /// <returns></returns>
        static bool StackableHome(Card selectedCard, CardLocation homeCell)
        {
            if(selectedCard == null)
            {
                return false;
            }
            else if(homeCell.Card == null && selectedCard.Rank != Card.MinumumRank)
            {
                return false;
            }
            else if(homeCell.Card == null && selectedCard.Rank == Card.MinumumRank) 
            {
                return true;
            }
            else if (selectedCard.Suit == homeCell.Card.Suit && selectedCard.Rank == homeCell.Card.Rank + 1)
            {                           // same suit and if the rank of the selected card is 1 higher
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// returns true if the column can move to another column
        /// </summary>
        /// <param name="selectedColumn">the selected colum</param>
        /// <param name="finalColumn">the destination column</param>
        /// <param name="numCards">the number of cards selected ong</param>
        /// <returns></returns>
        private bool StackableColumnToColumn(TableauColumn selectedColumn, TableauColumn finalColumn, int numCards)
        {
                                            

            if (_freeCellStack.Count >= numCards - 1)                                                                    //if there is enough free cells
            {
                Card[] cardArray = selectedColumn.Column.ToArray();                         // convert the column into an array

                for (int i = 0; i < numCards-1; i++)                                                          //start at 1  so you dont iterate to far because
                {                                                                                                       //we check with the next card in the array

                    if (cardArray[i].Rank != cardArray[i + 1].Rank - 1 || cardArray[i].Color == cardArray[i + 1].Color) // used demorgans to invert
                    {                                                                                                   //if the card and the next one arent stacked 
                        return false;                                                                                   //right return false
                    }
                }
                    
                if(finalColumn.Column.Count == 0)                                                                       // if the dest column is empty you can move it
                {
                    return true;
                }
                else if (cardArray[numCards-1].Rank != finalColumn.Column.Peek().Rank - 1 || cardArray[numCards-1].Color == finalColumn.Column.Peek().Color)
                {
                    return false;   // if the column is not empty check if the card can move if i cant return false
                }
                else
                {
                    return true;    // otherwise return true
                }

                                                                                                     
            }
            return false;                                                                                               // return false if there is not
                                                                                                                        // enough free cells
        }

        /// <summary>
        /// try to do a complex column to column move using free cells
        /// </summary>
        /// <param name="selectedColumn">the selected column</param>
        /// <param name="finalColumn">the destination column</param>
        /// <param name="numCards">the amount of cards moving</param>
        private void MoveColumnToColumn(TableauColumn selectedColumn, TableauColumn finalColumn, int numCards)
        {
            Stack<CardLocation> tempStack = new Stack<CardLocation>();

            for (int i = 1; i <= numCards-1; i++)                                        //start i at 1 so it iterates to n-1 cards, plus numcards -1 so it indexes at 0?
            {                                                                           //might be worth double checking
                if(_freeCellStack.Count > 0) 
                {
                    tempStack.Push(_freeCellStack.Peek());
                    AddCard(_freeCellStack.Peek(), RemoveCardFromColumn(selectedColumn));   // using peek here cause supposedly add card will remove it from the free cell stack
                                                      //keping track of the free cell location in a temp stack
                    Redraw(finalColumn, _freeCellStack.Peek());
                }
                
                

            }
                AddCardToColumn(finalColumn,RemoveCardFromColumn(selectedColumn));
            
            for (int i = 1; i < numCards - 1; i ++)
            {
                AddCardToColumn(finalColumn, RemoveCardFromFreeCell(tempStack.Pop()));
                Redraw(finalColumn, _freeCellStack.Peek());
            }
        }

        /// <summary>
        /// look at each free cell and see if it can go to a home slot and return a bool if you can
        /// </summary>
        /// <returns>T/F whether it works or not</returns>
        private bool TryFreeToHomeCell()
        {
            foreach(CardLocation loc in FreeCells)              //look at each free cell 
            {
                foreach(CardLocation homeCell in HomeCells)     // for each freee cell look at each home cell
                {
                    if (StackableHome(loc.Card, homeCell))      // if you can stack the free card on the home vell
                    {
                        AddCard(homeCell,RemoveCardFromFreeCell(loc));            // add the card to the home cell
                        Redraw(homeCell, loc);                  // redraw both
                        return true;                            // exit code returning true so it doesnt cause a error
                    }                                           // because chaging amount of for each set/lenght?
                        
                }
                
            }
            return false;                                       // if you iterate through all and no moves return false
        }

        /// <summary>
        /// try to move a card from the columns to a home tile
        /// </summary>
        /// <returns></returns>
        private bool TryColumnToHome()
        {
            foreach (TableauColumn column in TableauColumns)    //for each column
            {
                foreach (CardLocation homeCell in HomeCells)    // check each home cell
                {
                    try
                    {
                        if (StackableHome(column.Column.Peek(), homeCell))
                        { // if the card can be palced there

                            AddCard(homeCell, RemoveCardFromColumn(column));
                            Redraw(homeCell, column);
                            return true;

                        }
                    }
                    catch(InvalidOperationException)
                    {

                    }
                        
                    
                }
                   
            }
            return false;
        }


        /// <summary>
        /// Reacts to the user's click on a free cell.
        /// </summary>
        /// <param name="loc">The chosen free cell.</param>
        public void ClickFreeCell(CardLocation loc)
        {
            
                if (_selectedFreeCell != null)                 // if there is a selected card
                {
                    CardLocation tempLoc = DeselectFreeCell();          // store it
                                                                      // deselect it
                    if (loc.Card == null)                           // if the location of the free cell is empty
                    {
                        AddCard(loc, RemoveCardFromFreeCell(tempLoc));                     // add it to the free cell
                    }

                }
                else if (_selectedColumn != null)
                {
                    TableauColumn tempColumn = DeselectColumn();


                    if (loc.Card == null && tempColumn.Column.Count() != 0)
                    {
                        AddCard(loc, RemoveCardFromColumn(tempColumn));
                    }

                    tempColumn.NumberSelected = 0;
                    tempColumn.Refresh();
                }
                else
                {

                    SelectFreeCell(loc);
                }
            
            
           

        }



        /// <summary>
        /// Reacts to the user's click on a tableau column.
        /// </summary>
        /// <param name="col">The column clicked.</param>
        /// <param name="n">The number of cards chosen.</param>
        public void ClickColumn(TableauColumn col, int n)
        {
            
                if (_selectedFreeCell != null)
                {
                    CardLocation tempLoc = DeselectFreeCell();

                    if (col.Column.Count() == 0 || StackableColumn(tempLoc.Card, col.Column.Peek()))
                    {
                        AddCardToColumn(col, RemoveCardFromFreeCell(tempLoc));
                    }

                }
                else if (_selectedColumn != null)
                {
                    TableauColumn tempColumn = DeselectColumn();
                    
                    if (StackableColumnToColumn(tempColumn, col, tempColumn.NumberSelected) && tempColumn.NumberSelected != 0)
                    {
                        MoveColumnToColumn(tempColumn, col, tempColumn.NumberSelected);
                    }

                    tempColumn.NumberSelected = 0;                              //***************
                    tempColumn.Refresh();
            }
                else
                {
                    SelectColumn(col, n);
                }

           
        }

        /// <summary>
        /// Reacts to the user's clicking a home cell.
        /// </summary>
        /// <param name="loc">The home cell that was clicked.</param>
        public void ClickHomeCell(CardLocation loc)
        {
            
                if (_selectedFreeCell != null)
                {
                    CardLocation tempLoc = DeselectFreeCell();

                    if (StackableHome(tempLoc.Card, loc))
                    {
                    AddCard(loc, RemoveCardFromFreeCell(tempLoc));
                    }

                }
                else if (_selectedColumn != null)
                {
                    TableauColumn tempColumn = DeselectColumn();

                    if (StackableHome((tempColumn.Column.Peek()), loc) && tempColumn.NumberSelected == 1)
                    {
                        AddCard(loc, RemoveCardFromColumn(tempColumn));
                    }

                    tempColumn.NumberSelected = 0;
                    tempColumn.Refresh();

            }
            


        }

        /// <summary>
        /// Moves all possible cards to home cells.
        /// </summary>
        public void MoveAll()
        {
            if(_selectedColumn != null)
            {
                _selectedColumn.NumberSelected = 0;
                DeselectColumn();
            
            }
            if(_selectedFreeCell != null)
            {
                DeselectFreeCell();
            }

            do; while (TryFreeToHomeCell() || TryColumnToHome());
            

        }
    }
}
