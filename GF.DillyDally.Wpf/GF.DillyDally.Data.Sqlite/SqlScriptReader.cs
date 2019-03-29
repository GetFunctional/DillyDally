using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GF.DillyDally.Data.Sqlite
{
    internal sealed class SqlScriptReader : IEnumerable<string>
    {
        private readonly Stream _stream;

        public SqlScriptReader(Stream stream)
        {
            this._stream = stream;
        }

        private string ReadStream()
        {
            var stream = this._stream;
            if (stream == null)
                return string.Empty;

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Regex.Split(Regex.Replace(this.ReadStream(), @"((?<!\r)\n)|(\r(?!\n))",
                    Environment.NewLine), @"/\*NEXT\*/|\bGO\b")
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line)).GetEnumerator();
        }

        public IList<string> ToList()
        {
            var list = new List<string>();
            using (var enumerator = this.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Current);
                }
            }

            return list;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}