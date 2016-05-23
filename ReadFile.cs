using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;

namespace TestProject
{
    class ReadFile
    {
        const int N = 20000;

        //Метод чтения файла - словаря
        public static StringCollection ReadDictionary(string filename)
        {
            StringCollection wordsDictionary = new StringCollection();
            using(StreamReader readerDictionary = new System.IO.StreamReader(filename))
            {
                string wordDictionary;
                try
                {
                    while ((wordDictionary = readerDictionary.ReadLine()) != null)
                        wordsDictionary.Add(wordDictionary);
                    return wordsDictionary;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Файл не найден");
                }
                catch (FileLoadException)
                {
                    MessageBox.Show("Ошибка чтения файла");
                }
            }
            
            
            return null;
        }

        //Проверка сущ. файла 
        public static bool CheckFile(string fileName)
        {
            try
            {
                FileInfo f = new FileInfo(fileName);
                if (f.Exists)
                    return true;
            }
            catch (FileNotFoundException)
            {
                return false;              
            }
            return false;
        }

        
       




        //Чтение файла с текстом и создание html документов
        public static void ReadAndWrite(string fileDictionary, string fileText, string directoryHtml)
        {
            
            StreamReader sr = new StreamReader(fileText);
            string lineText;
            int countLines = 0;
            int countFile = 0;
            StringCollection wordsDictionary = ReadDictionary(fileDictionary);
           
            
            string pathdirectory = directoryHtml.Split('.')[0];
            
            while ((lineText = sr.ReadLine()) != null)
            {
                bool flagNextFile = false;
                string result = "";
                bool flag = false;
                string[] wordsTextWithP= lineText.Split(' ');

                
                
                foreach (var wordT in wordsTextWithP)
                {
                    string wordsTextWithoutP = wordT.Replace("[,.!?:;]", "");
                    

                    foreach (var wordD in wordsDictionary)
                    {
                        if (wordsTextWithoutP.ToLower().Equals(wordD.ToLower()))
                            flag = true;

                    }
                    if (flag)
                    {
                        result += wordT.Replace(wordsTextWithoutP, "<b><i>" + wordsTextWithoutP + "</b></i>" + " ");
                        flag = false;
                    }
                    else
                        result += wordT + " ";
                }


                StreamWriter sw = new StreamWriter(pathdirectory + countFile + ".html", true, Encoding.Unicode);

                if (countLines > N)
                {
                    sw.Write(result.Substring(0, result.Split(new[] {'.','?','!'})[0].Length));
                    sw.Close();
                    countLines = 0;
                    flagNextFile = true;
                }
                else
                    countLines++;

                if (flagNextFile)
                {
                    using (StreamWriter sw1 = new StreamWriter(pathdirectory + (countFile++) + ".html", true, Encoding.Unicode))
                    {
                        string[] newResult = result.Split(new[] { '.', '?', '!' });

                        for (int j = 1; j < newResult.Length; j++)
                            sw1.Write(newResult[j], newResult[j].Length + 1);

                        flagNextFile = false;
                    }
  
                }
                else
                {
                    sw.Write(result);
                    sw.Close();
                }
                
            }
          
                
            

        }
    }
}