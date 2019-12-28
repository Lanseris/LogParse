using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    class RecordSignature
    {
        public string startWord;
        public string endWord;

        public List<string> text;

        public List<RecordSignature> childs;
    }
}
