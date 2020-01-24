using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public interface IReadingTemplatePart
    {
        public ReadingTemplateEnum ReadingTemplateType { get; }

        public void LineProcessing(string fileRow);

        public bool IsReading { get; }

        public event Action EndOfTemplate;

        public LinkedList<string> TemplateBody { get; }

        public string PartName { get; set; }

    }
}
