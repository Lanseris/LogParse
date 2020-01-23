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
            exceptionBodyTemplate.AddTemplatePart(TemplatePartCreator.CreateTemplatePart("Server stack trace", "Server stack trace", " "));

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

            try
            {
                _exceptionInfoControll = new ExceptionInfoControll(getExceptionsFromFile());

                loadSucess = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка парса логов: {0}", e.Message);
            }
            return loadSucess;
        }


/// <summary>
/// TODO передалать считывание под шаблоны (остановился на этом)
/// </summary>
/// <returns></returns>
        private Dictionary<string, List<ExceptionInfo>> getExceptionsFromFile(Dictionary<string, ExceptionBodyTemplate> exceptionBodyTemplatesDict)
        {
            #region СТАРОЕ РЕШЕНИЕ
            //string fileString = _logFileFullPath;
            //string copyFileString = fileString + "_Copy";

            //int excnNum = 0;

            //bool startException = false;
            //List<ExceptionInfo> exceptionInfoList = new List<ExceptionInfo>();
            //ExceptionInfo exceptionInfo = null;

            //if (File.Exists(copyFileString))
            //    File.Delete(copyFileString);

            //File.Copy(fileString, copyFileString);

            //using (StreamReader streamReader = new StreamReader(copyFileString))
            //{
            //    while (!streamReader.EndOfStream)
            //    {
            //        fileString = streamReader.ReadLine();
            //        if (!startException)
            //        {
            //            if (fileString.Contains("EXCEPTION"))
            //            {
            //                excnNum++;
            //                startException = true;
            //                exceptionInfo = new ExceptionInfo();
            //                exceptionInfo.exBody = new List<string>();
            //                exceptionInfo.exBody.Add(fileString);
            //            }
            //        }
            //        else
            //        {
            //            if (fileString == string.Empty)
            //            {
            //                startException = false;
            //                exceptionInfoList.Add(exceptionInfo);
            //            }
            //            else
            //            {
            //                exceptionInfo?.exBody.Add(fileString);
            //                if (fileString.Contains("Exception Type"))
            //                {
            //                    exceptionInfo.exType = fileString.Substring(16, fileString.Length - 16);
            //                }
            //            }
            //        }


            //    }
            //}

            //File.Delete(copyFileString);

            //return exceptionInfoList; 
            #endregion

            #region Переделка под шаблоны

            if (exceptionBodyTemplatesDict == null)
                throw new ArgumentNullException(nameof(exceptionBodyTemplatesDict));

            Dictionary<string, List<ExceptionInfo>> exceptionInfoDict = new Dictionary<string, List<ExceptionInfo>>();


            string fileString = _logFileFullPath;
            string copyFileString = fileString + "_Copy";

            if (File.Exists(copyFileString))
                File.Delete(copyFileString);

            File.Copy(fileString, copyFileString);

            foreach (var exceptionBodyTemplateKeyValue in exceptionBodyTemplatesDict)
            {
                if (exceptionInfoDict.ContainsKey(exceptionBodyTemplateKeyValue.Key))
                    throw new ArgumentException("Дублирующийся ключ: " + exceptionBodyTemplateKeyValue.Key);


                //exceptionInfoDict.Add(exceptionBodyTemplateKeyValue.Key, extractTemplateObjects(exceptionBodyTemplateKeyValue.Value, fileString, copyFileString));
            }


            File.Delete(copyFileString);
            return exceptionInfoDict; 

            #endregion
        }

        private List<ExceptionInfo> extractTemplateObjects(ExceptionBodyTemplate exceptionBodyTemplate,string fileString, string copyFileString)
        {
            if (exceptionBodyTemplate == null)
                throw new ArgumentNullException(nameof(exceptionBodyTemplate));

            List<ExceptionInfo> exceptionInfosList = new List<ExceptionInfo>();
            ExceptionInfo exceptionInfo = null;

            using (StreamReader streamReader = new StreamReader(copyFileString))
            {



                while (!streamReader.EndOfStream)
                {
                    fileString = streamReader.ReadLine();

                    



                    //if (!startException)
                    //{
                    //    if (fileString.Contains("EXCEPTION"))
                    //    {
                    //        excnNum++;
                    //        startException = true;
                    //        exceptionInfo = new ExceptionInfo();
                    //        exceptionInfo.exBody = new List<string>();
                    //        exceptionInfo.exBody.Add(fileString);
                    //    }
                    //}
                    //else
                    //{
                    //    if (fileString == string.Empty)
                    //    {
                    //        startException = false;
                    //        exceptionInfoList.Add(exceptionInfo);
                    //    }
                    //    else
                    //    {
                    //        exceptionInfo?.exBody.Add(fileString);
                    //        if (fileString.Contains("Exception Type"))
                    //        {
                    //            exceptionInfo.exType = fileString.Substring(16, fileString.Length - 16);
                    //        }
                    //    }
                    //}


                }
            }


            return exceptionInfosList;
        }

    }

}
