using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse.Templates
{
    class TemplatePartStartAndNum : TemplatePart
    {
        public string StartString { get; set; }

        public int StringNum { get; set; }


        public TemplatePartStartAndNum(string partName, string startString, int stringNum) :
            base(partName)
        {
            if (startString == null || stringNum == 0)
                throw new ArgumentNullException("Один из параметров поиска не задан");

            _readingTemplate = ReadingTemplateEnum.StartAndNum;

            StartString = startString;
            StringNum = stringNum;
        }
    }
}
