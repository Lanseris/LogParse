using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LogParse.Templates.TemplatePartsTypes;
using System.Linq;

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
                _exceptionInfoControll = new ExceptionInfoControll();

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



            File.Delete(copyFileString);
            return exceptionInfoDict; 

            #endregion
        }

        private Dictionary<string, List<ExceptionInfo>> extractTemplateObjects(string fileString, string copyFileString)
        {
            ///Надо ли делать проверку на пути к файлам
            
            //Список ошибок, разложеный по имени шаблона, применяемого для их считывания
            Dictionary<string, List<ExceptionInfo>> exceptionInfosDic = new Dictionary<string, List<ExceptionInfo>>();

            //Первые части шаблонов
            Dictionary<string, IReadingTemplatePart> FirstTemplatePartsDic;

            //считываемый на данный момент шаблон
            ExceptionBodyTemplate readingExceptionBodyTemplate = null;
            //считывается ли сейчас шаблон
            bool exceptionBodyTemplateIsReading = false;
            //считываемая на данный момент часть шаблона
            IReadingTemplatePart readingTemplatePart = null;

            //Объект, содержащий в себе список Шаблонов Частей Шаблона и их текстовое содержание
            ExceptionInfo exceptionInfo = null;

            FirstTemplatePartsDic = getFirstsTemplateParts();

            using (StreamReader streamReader = new StreamReader(copyFileString))
            {
               

                while (!streamReader.EndOfStream)
                {
                    fileString = streamReader.ReadLine();

                    #region Поиск совбадения первой части шаблона

                    //если шаблон ещё не считывается
                    if (!exceptionBodyTemplateIsReading)
                    {
                        exceptionInfo = new ExceptionInfo();

                        //для каждой части из списка первых частей шаблонов
                        foreach (var templatePart in FirstTemplatePartsDic)
                        {
                            //Проверка на совпадение с улсовием первой Части шаблона
                            if (templatePart.Value.CheckConditionMatch(fileString))
                            {
                                exceptionBodyTemplateIsReading = true;

                                //нахождение шаблона, первая часть которого начала считываться
                                readingExceptionBodyTemplate = _exceptionBodyTemplatesDict[templatePart.Key];

                                exceptionInfo.ExcceptionBodyTemplate = readingExceptionBodyTemplate;

                                readingTemplatePart = templatePart.Value;
                                break;
                            }
                        }
                    }
                    #endregion

                    #region Чтение шаблона
                    else
                    {
                        LinkedListNode<IReadingTemplatePart> FirstNode = readingExceptionBodyTemplate.TemplatePartsLinkedList.First;
                        //while (exceptionBodyTemplateIsReading)
                        //{
                        //    if (readingTemplatePart.IsReading)
                        //    {
                        //        readingTemplatePart.LineProcessing(fileString, ref exceptionInfo);
                        //    }
                        //    else
                        //    {
                        //        //конец считывания ЧАСТИ шаблона
                        //        if (readingExceptionBodyTemplate.TemplatePartsLinkedList.GetEnumerator().MoveNext())
                        //        {
                        //        }
                        //        else
                        //        {
                        //            //зафигачить Null-ы или обновить значения во всех необходимых переменных
                        //            exceptionBodyTemplateIsReading = false;
                        //            exceptionInfosDic.ContainsKey(readingExceptionBodyTemplate.TemplateName) ? exceptionInfosDic[readingExceptionBodyTemplate.TemplateName].Add(readingExceptionBodyTemplate);
                        //        }
                        //    }
                        //}

                    } 
                    #endregion
                }
            }

            return exceptionInfosDic;
        }

        /// <summary>
        /// Возвращает первые части шаблонов
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, IReadingTemplatePart> getFirstsTemplateParts()
        {
            return _exceptionBodyTemplatesDict.ToDictionary(x=> x.Key, i=>i.Value.TemplatePartsLinkedList.First());
        }

    }

}
