using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNet.API
{
    public class ResponseException : Exception
    {
        public Status Status { get; protected set; }

        public ResponseException(Status s, string rea)
            : base(rea)
        {
            Status = s;            
        }
    }

 
    public class RealmNotFoundException : Exception
    {
        public RealmNotFoundException(string msg)
            : base(msg)
        {
        }
    }
}
