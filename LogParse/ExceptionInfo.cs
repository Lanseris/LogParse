using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public class ExceptionInfo
    {
        public ExceptionBodyTemplate ExcceptionBodyTemplate { get; set; }

        public Dictionary<string, IReadingTemplatePart> ExceptionParts { get; set; }

        //public string exType;

        //    public DateTime exceptionDateTime;

        //    public string message;

        //    public List<string> exBody;
        }
    }
