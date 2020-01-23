﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public interface IReadingTemplatePart
    {
        public ReadingTemplateEnum ReadingTemplateType { get; }

        public void LineProcessing(string fileRow);

        //public bool EndOfTemplate { get; }

        public event Action EndOfTemplate;

        public LinkedList<string> TemplateBody { get; }

    }
}
