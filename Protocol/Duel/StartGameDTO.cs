using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    [Serializable]
    public class StartGameDTO
    {
        public DeckMes deckMes1;
        public DeckMes deckMes2;

        public StartGameDTO (DeckMes val1,DeckMes val2)
        {
            deckMes1 = val1;
            deckMes2 = val2;
        }
    }

    [Serializable]
    public class DeckMes
    {
        public  Deck deck;
        public  string accountName;
    }
}
