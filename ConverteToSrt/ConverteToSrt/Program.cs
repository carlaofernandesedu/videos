﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConverteToSrt
{
    class Program
    {
        static void Main(string[] args)
        {
            ConverterArquivosVTTparaSRT();
           
        }

        static void ConverterArquivosVTTparaSRT()
        {
            var pasta = Environment.CurrentDirectory;
            LogarEtapa("Lendo o diretório: " + pasta);
           foreach(var arquivo in Directory.GetFiles(pasta))
           {
               LogarEtapa("Lendo arquivo: " + arquivo);
               if (arquivo.ToLower().IndexOf(".vtt") > -1)
               {
                   var conteudoVTT = File.ReadAllText(Path.Combine(pasta,arquivo));
                   var conteudoSRT = ConvertWebvttToSrt(conteudoVTT);
                   File.WriteAllText(arquivo.ToLower().Replace(".vtt", ".srt"), conteudoSRT);
                   LogarEtapa("Arquivo " + arquivo + " convertido");
               }
           }
        }

        public static String ConvertWebvttToSrt(String webvttContent)
        {
            if (webvttContent == null)
                throw new ArgumentNullException("webvttContent");

            String srtResult = webvttContent;

            Int32 srtPartLineNumber = 0;

            srtResult = Regex.Replace(srtResult, @"(WEBVTT\s+)(\d{2}:)", "$2"); // Removes 'WEBVTT' word

            srtResult = Regex.Replace(srtResult, @"(\d{2}:\d{2}:\d{2})\.(\d{3}\s+)-->(\s+\d{2}:\d{2}:\d{2})\.(\d{3}\s*)", match =>
            {
                srtPartLineNumber++;
                return srtPartLineNumber.ToString() + Environment.NewLine +
                    Regex.Replace(match.Value, @"(\d{2}:\d{2}:\d{2})\.(\d{3}\s+)-->(\s+\d{2}:\d{2}:\d{2})\.(\d{3}\s*)", "$1,$2-->$3,$4");
                // Writes '00:00:19.620' instead of '00:00:19,620'
            }); // Writes Srt section numbers for each section

            return srtResult;
        }

        static void LogarEtapa(string mensagem)
        {
            Console.WriteLine(mensagem); 
        }
    }
}
