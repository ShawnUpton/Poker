using System;

using System.IO;
using System.Reflection;
using System.Collections.Generic;
namespace Poker
{
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        king,
        Ace
    }
    [Serializable]
    public struct Card
    {        
        public Card(Suit suit, Rank value)
        {
            Suit = suit;
            Rank = value;
        }
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

       

        public byte[] ImageBytes
        { 
            get {

                var rank = (int)Rank;
                var rankVal = rank.ToString();
                if (rank > 10)    
                {
                    switch(rank)
                    {
                        case 11:
                            rankVal = "J";
                            break;
                        case 12:
                            rankVal = "Q";
                            break;
                        case 13:
                            rankVal = "K";
                            break;
                        case 14:
                            rankVal = "A";
                            break;
                    }
                }

                var suitVal = Suit.ToString().Substring(0,1);                
                var resourceName = "Poker.Content.Images." + rankVal + suitVal + ".png";
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                Stream myStream = myAssembly.GetManifestResourceStream(resourceName);                 
                return Stream2Bytes(myStream);                
            } 
        }

        /// <summary>
        /// Return byte array from Stream
        /// Code taken from https://stackoverflow.com/questions/60674467/c-sharp-net-core-imageconverter
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        private byte[] Stream2Bytes(Stream stream, int chunkSize = 1024)
        {            
            if (stream == null)
            {
                throw new ArgumentException("Parameter cannot be null", "stream");   
            }

            if (chunkSize < 1)
            {
                throw new ArgumentException("Parameter must be greater than zero", "chunkSize");               
            }

            if (chunkSize > 1024 * 64)
            {
                throw new ArgumentException(String.Format("Parameter must be less or equal {0}", 1024 * 64), "chunkSize");                
            }

            List<byte> buffers = new List<byte>();

            using (BinaryReader br = new BinaryReader(stream))
            {
                byte[] chunk = br.ReadBytes(chunkSize);

                while (chunk.Length > 0)
                {
                    buffers.AddRange(chunk);
                    chunk = br.ReadBytes(chunkSize);
                }
            }
            return buffers.ToArray();
        }
}
}
