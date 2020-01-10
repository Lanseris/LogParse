using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse.Templates
{
    class TemplatePartAuto : TemplatePart
    {

        public TemplatePartAuto(string partName) :
            base(partName)
        {
            _readingTemplate = ReadingTemplateEnum.Auto;
        }
    }
}
