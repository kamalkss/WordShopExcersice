namespace WordShopExcersice.WebApi.DTO;

public class ReadPostCodeDTO
{
    public string PostCode { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string? city { get; set; }
    public string? country { get; set; }
    public string? county { get; set; }
}