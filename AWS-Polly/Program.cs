using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AWS_Polly
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var credentials = new BasicAWSCredentials("accessKey", "secreatKey");

            var polly = new AmazonPollyClient(credentials, RegionEndpoint.USWest2);

            Console.WriteLine("Digite um texto para ser transformado em audio: ");

            var text = Console.ReadLine();

            bool isLexicon = true;
            
            if (isLexicon)
            {
                string content = File.ReadAllText(@"C:\Users\demetrius.pecoraro\Documents\POC\AWS\AWS-Polly\AWS-Polly\AwsLexicons.xml");

                var lexResp = polly.PutLexiconAsync(new PutLexiconRequest
                {
                    Name = "1ALexicon1",
                    Content = content
                }).Result;

            }

            var lexicon = polly.ListLexiconsAsync(new ListLexiconsRequest()).Result;

            List<string> lexicons = new List<string>();

            lexicon.Lexicons.ForEach(l => lexicons.Add(l.Name));

            var req = polly.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
            {
                LanguageCode = "pt-BR",
                OutputFormat = "mp3",
                SampleRate = "8000",
                Text = text,
                TextType = "text",
                VoiceId = "Ricardo",
                Engine = "Standard",
                LexiconNames = lexicons
            }).Result;

            var meta = req.ResponseMetadata;

            var helper = new AudioHelper();
            helper.SalvarMp3(req, String.Concat(Directory.GetCurrentDirectory(), meta, ".mp3"));

            Task.WaitAll();

            Console.WriteLine("Hello World!");
        }

        
    }
}
