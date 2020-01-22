using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public abstract class TemplatePart: IReadingTemplatePart
    {
        private string _partName;

        private bool _endOfTemplate;

        public bool EndOfTemplate => _endOfTemplate;

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

        protected ReadingTemplateEnum _readingTemplate;

        public ReadingTemplateEnum ReadingTemplate => _readingTemplate;

        

        public TemplatePart(string partName)
        {
            PartName = partName;
            _endOfTemplate = false;
        }

        public virtual void LineProcessing(string fileRow)
        {
            throw new NotImplementedException();
        }
    }
}
