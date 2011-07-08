
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BattleNet.API.WoW
{
    /// <summary>
    /// Caches things into memory, untill out of room then caches to disk
    /// </summary>
    public class FailoverCache : IDisposable
    {
        bool dirty = false;

        string file;
        Dictionary<string, long> index = new Dictionary<string, long>();
        Stream fileBacking;

        public FailoverCache(string file)
        {
            this.file = file;
            LoadIndex(File.Open(file+".idx", FileMode.OpenOrCreate));
            fileBacking = File.Open(file, FileMode.OpenOrCreate);
        }

        private void LoadIndex(Stream st)
        {
            using (BinaryReader r = new BinaryReader(st))
            {
                while (r.BaseStream.Position < r.BaseStream.Length)
                {
                    string key = r.ReadString();
                    long offset = r.ReadInt64();
                    index[key] = offset;
                }
            }
        }
        public void SaveIndex()
        {
            SaveIndex(File.Open(file + ".idx", FileMode.Create));
        }
        public void SaveIndex(Stream st)
        {
            using (st)
            {
                using (BinaryWriter r = new BinaryWriter(st))
                {
                    foreach (KeyValuePair<string, long> kvp in index)
                    {
                        r.Write(kvp.Key);
                        r.Write(kvp.Value);
                    }
                }
            }
            
        }

        public string this[string key]
        {
            get { 
                long offset;
                if (index.TryGetValue(key, out offset))
                {
                    fileBacking.Seek(offset, SeekOrigin.Begin);
                    BinaryReader r = new BinaryReader(fileBacking);
                    {
                        return r.ReadString();
                    }
                }
                return null;
            }
            set {

                if (index.ContainsKey(key))
                {
                    dirty = true;
                }
                // go to ending
                fileBacking.Seek(0, SeekOrigin.End);
                long offset = fileBacking.Position;
                BinaryWriter w = new BinaryWriter(fileBacking);
                {
                    w.Write(value);
                    w.Flush();
                }

                index[key] = offset;
            }
        }

        private void Compact()
        {
            Dictionary<string, long> newIndex = new Dictionary<string, long>();
            using (BinaryWriter newFile = new BinaryWriter(File.Open(file + ".tmp", FileMode.Create)) )
            {                
                foreach (KeyValuePair<string, long> kvp in index)
                {                    
                    long offset = newFile.BaseStream.Position;
                    newIndex[kvp.Key] = offset;
                    newFile.Write(kvp.Value);
                }
            }

            fileBacking.Close();
             
            File.Move(file + ".tmp", file);
            fileBacking = File.Open(file, FileMode.OpenOrCreate);
            index = newIndex;

            dirty = false;
        }

        #region IDisposable Members

        public void Dispose()
        {
            SaveIndex();
            if(dirty) Compact();
            
        }

        #endregion
    }
}
