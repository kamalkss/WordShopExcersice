using System.Text.RegularExpressions;
using WordShopExcersice.Data.Models;
using WordShopExcersice.Services.Services.PostalCodeService;

namespace WordShopExcersice.WebApi.ApiServices.Download
{
    public class CleanDataAndInsert
    {
        private readonly IPostalCode _postalCode;
        public CleanDataAndInsert(IPostalCode postalCode)
        {
            this._postalCode = postalCode;
        }

        public void Cleandata()
        {
            Regex rgx = new Regex("[^a-zA-Z0-9,.]");
            ICollection<PostCodeModel> postCodes = new List<PostCodeModel>();
            int counter = 0;
            using (var steam = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "tempUnzip\\postcodes.csv")))
            {

                string currentLine;
                while ((currentLine = steam.ReadLine()) != null)
                {
                    var selectline = currentLine;
                    
                    selectline = rgx.Replace(selectline, "");
                    var line = selectline.Split(',');
                    if (line[0] != "Postcode")
                    {
                        var pcm = new PostCodeModel
                        {
                            PostCode = line[0],
                            city = line[8],
                            country = line[12],
                            county = line[7],
                            latitude = Convert.ToDouble(line[2]),
                            longitude = Convert.ToDouble(line[3])
                        };

                        if (counter == 100000)
                        {
                            _postalCode.CreatePostalCode(postCodes);
                            postCodes.Clear();
                            counter = 0;
                        }
                        postCodes.Add(pcm);
                        counter++;

                        //_postalCode.CreatePostalCode(pcm);
                    }
                }
            }
        }
    }
}
