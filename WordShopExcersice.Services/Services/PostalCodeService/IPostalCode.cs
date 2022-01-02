using WordShopExcersice.Data.Models;

namespace WordShopExcersice.Services.Services.PostalCodeService;

public interface IPostalCode
{
    public Task<bool> SaveChanges();
    public Task<List<PostCodeModel?>> GetLast100PostCode();
    public Task<PostCodeModel> GetPostalCodeUsingLatAndLng(double lat, double lng);
    public Task<List<PostCodeModel>> GetPostalCodeRangeUsingLatAndLng(double lat, double lng, int range);
    public Task<PostCodeModel?> GetSingleDetailsOfPostalCode(string PostalCode);
    public Task<List<PostCodeModel>> GetPostalCodeRange(string PostalCode, int range);
    public Task<bool> CreatePostalCode(ICollection<PostCodeModel> postCode);
}