using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse.Templates
{
    class TemplatePartStartEnd : TemplatePart
    {
        public string StartString { get; set; }

        public string EndString { get; set; }


        public TemplatePartStartEnd(string partName, string startString, string endString):
            base(partName)
        {
            if (startString == null || endString == null)
                throw new ArgumentNullException("Один из параметров поиска не задан");

            _readingTemplate = ReadingTemplateEnum.StartEnd;
            
            StartString = startString;
            EndString = endString;
        }

        public override void LineProcessing(string fileRow)
        {
            if (fileRow.Contains())
            {

            }
        }
    }
}
