using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public interface IReadingTemplatePart
    {
        public ReadingTemplateEnum ReadingTemplate { get; }

        public void LineProcessing(string fileRow);

        public bool EndOfTemplate { get; }
    }
}
