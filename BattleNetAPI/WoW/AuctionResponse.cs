using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace BattleNet.API.WoW
{
    public class AuctionResponse
    {
        [XmlArray("files")]
        [XmlArrayItem("items")]
        public List<AuctionFile> Files { get; set; }
    }

    public class AuctionFile
    {
        [XmlIgnore]
        public BattleNetClient Client { set; protected get; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("lastModified")]
        public UnixTimestamp LastModified { get; set; }

        AuctionData data = null;
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

    
    public class AuctionData
    {
        [XmlElement("realm")]
        public Realm Realm { get; set; }
        [XmlElement("horde")]
        public AuctionCollection Horde { get; set; }

        [XmlElement("neutral")]
        public AuctionCollection Neutral { get; set; }

        [XmlElement("alliance")]
        public AuctionCollection Alliance { get; set; }
    }

    public class AuctionCollection
    {
        [XmlArray("auctions")]
        [XmlArrayItem("items")]
        public List<Auction> Auctions { get; set; }
    }

   

    public class Auction
    {
        [XmlElement("auc")]
        public int Auc { get; set; }

        [XmlElement("item")]
        public int Item { get; set;}

        [XmlElement("owner")]
        // TODO: whats a good way to link back to the owner?
        public string Owner { get; set; }

        [XmlElement("bid")]
        // TODO: turn this into a custom type with Gold, Silver, Copper?
        public long Bid { get; set; }

        [XmlElement("buyout")]
        public long Buyout { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }
    }
}
