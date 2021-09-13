using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Aspose.Html;
using Aspose.Html.IO;
using Aspose.Html.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda1
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {



            //using (var configuration = new Configuration())
            //{
            //    // Get the IUserAgentService
            //    var userAgentService = configuration.GetService<IUserAgentService>();

            //    // Set the custom style parameters for the "h1" and "p" elements
            //    userAgentService.UserStyleSheet = "h1 { color:#a52a2a; }\r\n" +
            //                                      "p { color:grey; }\r\n";

            //    // Set custom font folder path
            //    //return string.Join(',', userAgentService.FontsSettings.GetFontsLookupFolders());

            //    // Initialize the HTML document with specified configuration
            //    using (var document = new HTMLDocument(Path.Combine(OutputDir, "user-agent-fontsetting.html"), configuration))
            //    {
            //        // Convert HTML to PDF
            //        Converter.ConvertHTML(document, new PdfSaveOptions(), Path.Combine(OutputDir, "user-agent-fontsetting_out.pdf"));
            //    }
            //}



            using (var streamProvider = new MemoryStreamProvider())
            {
                try
                {
                    Aspose.Html.Converters.Converter.ConvertHTML(@$"<span>Hello World Sample</span><br/><span>Lambda Input value : {input.ToUpper()}</span>", ".", new Aspose.Html.Saving.PdfSaveOptions(), streamProvider);
                }
                catch (Exception ex) 
                {
                    context.Logger.Log(ex.InnerException.Message);
                    context.Logger.Log(ex.Message);
                    return "fail";
                }
                

                var memory = streamProvider.Streams.First();
                memory.Seek(0, System.IO.SeekOrigin.Begin);
                return memory.ToArray().ToString();
                // Flush the result data to the output file
                //using (System.IO.FileStream fs = System.IO.File.Create("output.pdf"))
                //{
                //    memory.CopyTo(fs);
                //}
            }
                
            return input?.ToUpper();
        }
    }
}
