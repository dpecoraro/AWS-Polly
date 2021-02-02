using Amazon.Polly.Model;
using System.IO;

namespace AWS_Polly
{
    public class AudioHelper
    {
        public async void SalvarMp3(SynthesizeSpeechResponse response, string path)
        {
            Stream audioStream = response.AudioStream;
                        
            using(MemoryStream ms = new MemoryStream())
            using (FileStream fs = new FileStream(path,
                   FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                audioStream.CopyTo(ms);

                byte[] buffer = ms.ToArray();

                await audioStream.ReadAsync(buffer);

               await fs.WriteAsync(buffer);

                fs.Flush();
            }

        }
    }
}
