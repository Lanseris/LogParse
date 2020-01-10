using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LogParse.Templates.TemplatePartsTypes;

namespace LogParse
{
    class LogControll
    {
        private string _logFileFullPath;

        private ExceptionInfoControll _exceptionInfoControll;

        public ExceptionInfoControll ExceptionInfoControll => _exceptionInfoControll;

        private Dictionary<string, ExceptionBodyTemplate> _exceptionBodyTemplatesDict;

        public LogControll(string path)
        {
            SetLogFilePath(path);

            _exceptionBodyTemplatesDict = new Dictionary<string, ExceptionBodyTemplate>();


            #region Создание тестового шаблона
            ExceptionBodyTemplate exceptionBodyTemplate = new ExceptionBodyTemplate("Тест_1");
            exceptionBodyTemplate.AddTemplatePart(TemplatePartCreator.CreateTemplatePart("ExceptionStart", "EXCEPTION", null, 1));
            exceptionBodyTemplate.AddTemplatePart(TemplatePartCreator.CreateTemplatePart("MachineName", "MachineName", null, 1));
            exceptionBodyTemplate.AddTemplatePart(TemplatePartCreator.CreateTemplatePart("Exception Type", "Exception Type", null, 1));
            exceptionBodyTemplate.AddTemplatePart(TemplatePartCreator.CreateTemplatePart("Message", "Message: ", null, 1));
            exceptionBodyTemplate.AddTemplatePart(TemplatePartCreator.CreateTemplatePart("Server stack trace", "Server stack trace", ""));

            _exceptionBodyTemplatesDict.Add(exceptionBodyTemplate.TemplateName, exceptionBodyTemplate); 
            #endregion

        }

        private bool SetLogFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (File.Exists(path))
            {
                _logFileFullPath = path;
                return true;
            }

            return false;
        }

        public bool LoadExceptions()
        {
            bool loadSucess = false;

            _exceptionInfoControll = new ExceptionInfoControll(GetExceptionsFromFile());

            loadSucess = true;
            return loadSucess;
        }



        private List<ExceptionInfo> GetExceptionsFromFile()
        {
            string fileString = _logFileFullPath;
            string copyFileString = fileString + ".Copy";
           
            int excnNum = 0;

            bool startException = false;
            List<ExceptionInfo> exceptionInfoList = new List<ExceptionInfo>();
            ExceptionInfo exceptionInfo = null;

            if (File.Exists(copyFileString))
                File.Delete(copyFileString);

            File.Copy(fileString, copyFileString);

            using (StreamReader streamReader = new StreamReader(copyFileString))
            {
                while (!streamReader.EndOfStream)
                {
                    fileString = streamReader.ReadLine();
                    if (!startException)
                    {
                        if (fileString.Contains("EXCEPTION"))
                        {
                            excnNum++;
                            startException = true;
                            exceptionInfo = new ExceptionInfo();
                            exceptionInfo.exBody = new List<string>();
                            exceptionInfo.exBody.Add(fileString);
                        }
                    }
                    else
                    {
                        if (fileString == string.Empty)
                        {
                            startException = false;
                            exceptionInfoList.Add(exceptionInfo);
                        }
                        else
                        {
                            exceptionInfo?.exBody.Add(fileString);
                            if (fileString.Contains("Exception Type"))
                            {
                                exceptionInfo.exType = fileString.Substring(16, fileString.Length - 16);
                            }
                        }
                    }


                }
            }

            File.Delete(copyFileString);

            return exceptionInfoList;
        }

    }
}
