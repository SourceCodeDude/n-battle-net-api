using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BattleNet.API
{

    public enum Status
    {
        Ok,
        [XmlEnum("nok")]
        NotOk,
    }

    /// <summary>
    /// Base class for a response
    /// </summary>
    [XmlRoot("root")]
    public class ResponseRoot
    {
        [XmlElement("status")]
        public Status Status { get; set; }
        [XmlElement("reason", IsNullable = true)]
        public string Reason { get; set; }

    }
}
