using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WordShopExcersice.Data.DatabaseContext;
using WordShopExcersice.Data.Models;

namespace WordShopExcersice.Services.Services.PostalCodeService;

public class SqlPostalCode : IPostalCode
{
    private readonly AppDbContext _context;
    private readonly ILogger<SqlPostalCode> _logger;

    public SqlPostalCode(AppDbContext context, ILogger<SqlPostalCode> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() >= 0;
    }

    public async Task<List<PostCodeModel?>> GetLast100PostCode()
    {
        return await _context.PostCode.TakeLast(100).ToListAsync();
    }

    public async Task<PostCodeModel> GetPostalCodeUsingLatAndLng(double lat, double lng)
    {
        var postacode = await _context.PostCode.FirstOrDefaultAsync(c => c.latitude == lat &&
                                                                         c.longitude == lng);

        if (postacode != null)
        {
            return postacode;
        }

        postacode.PostCode = "Not Found";
        return postacode;
    }

    public async Task<List<PostCodeModel>> GetPostalCodeRangeUsingLatAndLng(double lat, double lng, int range)
    {
        var RangeModel = new List<PostCodeModel>();
        var Center = new GeoCoordinate(lat, lng);

        var nearby = _context.PostCode.Select(x => new GeoCoordinate(x.latitude, x.longitude))
            .AsEnumerable().Where(x => x.GetDistanceTo(Center) <= (range * 1609.344)).ToList();
        foreach (var item in nearby)
        {
            var FindPostalCode = await GetPostalCodeUsingLatAndLng(item.Latitude, item.Longitude);
            RangeModel.Add(FindPostalCode);
        }

        return RangeModel;
    }

    public async Task<PostCodeModel?> GetSingleDetailsOfPostalCode(string PostalCode)
    {
        return await _context.PostCode.FirstOrDefaultAsync(c => c.PostCode == PostalCode);
    }

    public async Task<List<PostCodeModel>> GetPostalCodeRange(string PostalCode, int range)
    {
        var Details = await GetSingleDetailsOfPostalCode(PostalCode);

        var RangeModel = await GetPostalCodeRangeUsingLatAndLng(Details.latitude, Details.longitude, range);
        return RangeModel;
    }

    public async Task<bool> CreatePostalCode(ICollection<PostCodeModel> postCode)
    {
        try
        {
            await _context.PostCode.AddRangeAsync(postCode);
            await SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}