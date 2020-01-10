using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public class TemplatePart
    {
        private string _partName;

        public string PartName 
        { 
            get { return _partName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Наименование части шаблона не может быть пустым");

                _partName = value;
            }
        }

        private ReadingTemplateEnum _readingTemplate;

        public TemplatePart(string partName, ReadingTemplateEnum readingTemplate)
        {
            PartName = partName;

            _readingTemplate = readingTemplate;
        }
    }
}
