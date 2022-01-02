using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WordShopExcersice.Data.DatabaseContext;
using WordShopExcersice.Data.Models;
using WordShopExcersice.Services.Services.PostalCodeService;
using WordShopExcersice.WebApi.ApiServices.Download;
using WordShopExcersice.WebApi.DTO;

namespace WordShopExcersice.WebApi.Controllers;

[ApiController]
[Route("api/PostCode")]
public class PostCodeController : Controller
{
    
    private readonly IPostalCode _postalCode;
    private readonly IMapper _mapper;

    public PostCodeController(IMapper mapper, IPostalCode postalCode)
    {
        _mapper = mapper;
        _postalCode = postalCode;
        AppDbContext app = new AppDbContext();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        
        var download = new DownloadAndUnzip(_postalCode);
        download.ReturnResult();
        var CleanData = new CleanDataAndInsert(_postalCode);
        CleanData.Cleandata();
        
        

        return Ok();
    }

    

    [HttpGet("postcode")]
    public async Task<ActionResult<ReadPostCodeDTO>> GetPostCode(string postcode)
    {
        try
        {
            var Detail = await _postalCode.GetSingleDetailsOfPostalCode(postcode);
            if(Detail != null)
                return Ok(Detail);
            else
            {
                return NotFound();
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("postcoderange")]
    public async Task<ActionResult<ReadPostCodeDTO>> GetPostcodeWithRange(string postcode, int range)
    {
        try
        {
            var Postcodes = await _postalCode.GetPostalCodeRange(postcode, range);
            if (Postcodes.Count>0)
                return Ok(Postcodes);
            else
            {
                return NotFound();
            }
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("coordinate")]
    public async Task<ActionResult<ReadPostCodeDTO>> GetPostcodeWithCoordinate(double lat,double lng)
    {
        try
        {
            var Postcodes = await _postalCode.GetPostalCodeUsingLatAndLng(lat, lng);
            if (Postcodes != null)
                return Ok(Postcodes);
            else
            {
                return NotFound();
            }
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("coordinaterange")]
    public async Task<ActionResult<ReadPostCodeDTO>> GetPostcodeWithCoordinateRange(double lat, double lng, int range)
    {
        try
        {
            var Postcodes = await _postalCode.GetPostalCodeRangeUsingLatAndLng(lat,lng,range);
            if (Postcodes.Count > 0)
                return Ok(Postcodes);
            else
            {
                return NotFound();
            }
           
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}