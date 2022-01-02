using System.IO.Compression;
using System.Net;
using WordShopExcersice.Data.Models;
using WordShopExcersice.Services.Services.PostalCodeService;

namespace WordShopExcersice.WebApi.ApiServices.Download;

public class DownloadAndUnzip
{
    private readonly IPostalCode postalCode;
    private readonly WebClient webClient = new();

    public DownloadAndUnzip(IPostalCode _postalCode)
    {
        postalCode = _postalCode;
    }


    public void ReturnResult()
    {
        webClient.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
        webClient.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
        webClient.DownloadFile(new Uri("https://www.doogal.co.uk/files/postcodes.zip"), "postcodes.zip");
        var FileUrl = "https://www.doogal.co.uk/files/postcodes.zip";
        //var ms = new MemoryStream();
        var path = Path.Combine(Directory.GetCurrentDirectory(), "postcodes.zip");

        var tempPath = Path.Combine(Directory.GetCurrentDirectory(), "tempUnzip");

        var files = Directory.GetFiles(tempPath);


        if (files.Length > 0)
            foreach (var file in files)
            {
                var f = new FileInfo(file);
                //Check if the file exists already, if so delete it and then move the new file to the extract folder
                if (File.Exists(Path.Combine(tempPath, f.Name)))
                {
                    File.Delete(Path.Combine(tempPath, f.Name));
                    ZipFile.ExtractToDirectory("postcodes.zip", tempPath);
                }
            }
        else
            ZipFile.ExtractToDirectory("postcodes.zip", tempPath);

    }
}