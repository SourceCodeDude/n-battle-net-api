using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Net;
using System.IO;

namespace BattleNet.API.WoW
{
    [DataContract]
    public class AuctionResponse : ResponseRoot
    {
        [XmlArray("files")]
        [XmlArrayItem("item")]
        [DataMember(Name="files")]
        public List<AuctionFile> Files { get; set; }
    }

    [DataContract]
    public class AuctionFile
    {
        [XmlIgnore]
        public BattleNetClient Client { set; protected get; }

        [XmlElement("url")]
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [XmlElement("lastModified")]
        public UnixTimestamp LastModified { get; set; }

        [DataMember(Name = "lastModified")]
        internal long lastModified
        {
            get{ return LastModified.ToMsec(); }
            set
            {
                LastModified = new UnixTimestamp(value);
            }
        }

        AuctionData data = null;

        /// <summary>
        /// The actual auction data loaded from the URL
        /// </summary>
        [XmlIgnore]        
        public AuctionData Data
        {
            get
            {
                if (data == null)
                {
                    LoadData();
                }
                return data;
            }
        }

        protected void LoadData()
        {
            data = Client.GetObject<AuctionData>(this.Url);
            /*
            WebRequest req = WebRequest.Create(this.Url);
            WebResponse res = req.GetResponse();

            // the file is json.gz  so lets decompress it first
            //  new GZipStream(res.GetResponseStream(), CompressionMode.Decompress)
            using (Stream st = res.GetResponseStream() )
            {
                using (TextReader r = new StreamReader(st))
                {
                    // sure to be a big string.. so clean it up
                    // as fast as possible
                    string txt = r.ReadToEnd();                    
                    data = JsonParser.Parse<AuctionData>(txt);
                    txt = null;                    
                }
            }
             */
        }
    }

    [DataContract]
    public class AuctionData
    {
        [XmlElement("realm")]
        [DataMember(Name="realm")]
        public Realm Realm { get; set; }

        [XmlElement("horde")]
        [DataMember(Name = "horde")]
        public AuctionCollection Horde { get; set; }

        [XmlElement("neutral")]
        [DataMember(Name = "neutral")]
        public AuctionCollection Neutral { get; set; }

        [XmlElement("alliance")]
        [DataMember(Name = "alliance")]
        public AuctionCollection Alliance { get; set; }
    }

    [DataContract]
    public class AuctionCollection
    {
        [XmlArray("auctions")]
        [XmlArrayItem("item")]
        [DataMember(Name="auctions")]
        public List<Auction> Auctions { get; set; }
    }

   
    [DataContract]
    public class Auction
    {
        /// <summary>
        /// Auction ID
        /// </summary>
        [XmlElement("auc")]
        [DataMember(Name = "auc")]
        public int Auc { get; set; }

        /// <summary>
        /// ID of item
        /// </summary>
        [XmlElement("item")]
        [DataMember(Name="item")]
        public int Item { get; set;}

        /// <summary>
        /// Name of auction owner
        /// </summary>
        [XmlElement("owner")]
        [DataMember(Name="owner")]
        // TODO: whats a good way to link back to the owner?
        public string Owner { get; set; }

        /// <summary>
        /// Bid ammount in copper
        /// </summary>
        [XmlElement("bid")]
        [DataMember(Name = "bid")]
        // TODO: turn this into a custom type with Gold, Silver, Copper?
        public long Bid { get; set; }

        [XmlElement("buyout")]
        [DataMember(Name = "buyout")]
        public long Buyout { get; set; }

        [XmlElement("quantity")]
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Amount of time left on the auction
        /// VERY_LONG  24+
        /// LONG       2 - 24 hours 
        /// MEDIUM     30min - 2 hours
        /// SHORT      less than 30min        
        /// </summary>
        [XmlElement("timeLeft")]
        [DataMember(Name = "timeLeft")]
        public string TimeLeft { get; set; }

        public double BuyoutPerItem
        {
            get
            {
                return 1.0*Buyout / this.Quantity;
            }
        }
        public double BidPerItem
        {
            get
            {
                return 1.0 * Bid / this.Quantity;
            }
        }
    }
}
