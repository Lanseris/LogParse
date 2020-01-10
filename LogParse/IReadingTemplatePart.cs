using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public interface IReadingTemplatePart
    {
        public ReadingTemplateEnum ReadingTemplate { get; }
    }
}
