﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public abstract class TemplatePart: IReadingTemplatePart
    {
        protected string _partName;

        protected ReadingTemplateEnum _readingTemplate;

        public ReadingTemplateEnum ReadingTemplateType => _readingTemplate;

        public event Action EndOfTemplate;
        
        protected bool _reading;

        public bool IsReading => _reading;

        protected void InvokeEndOfTemplate()
        {
            EndOfTemplate?.Invoke();
        }


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


        public TemplatePart(string partName, Action endOfTemplate)
        {
            PartName = partName;
            EndOfTemplate += endOfTemplate;
          //  _templateBody = new LinkedList<string>();
            _reading = false;
        }

        public virtual void LineProcessing(string fileRow, ref ExceptionInfo templatePartBody) 
        {
            throw new NotImplementedException();
        }
    }
}
