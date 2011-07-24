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
        [XmlElement("url")]
        public Uri Url { get; set; }

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
            WebRequest req = WebRequest.Create(this.Url);
            WebResponse res = req.GetResponse();

            // the file is json.gz  so lets decompress it first
            using (GZipStream gzip = new GZipStream(res.GetResponseStream(), CompressionMode.Decompress))
            {
                using (TextReader r = new StreamReader(gzip))
                {
                    // sure to be a big string.. so clean it up
                    // as fast as possible
                    string txt = r.ReadToEnd();                    
                    data = JsonParser.Parse<AuctionData>(txt);
                    txt = null;                    
                }
            }
        }
    }

    
    public class AuctionData
    {
        [XmlElement("horde")]
        public AuctionCollection Horde { get; set; }

        [XmlElement("neutral")]
        public AuctionCollection Neutral { get; set; }

        [XmlElement("aliance")]
        public AuctionCollection Aliance { get; set; }
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
