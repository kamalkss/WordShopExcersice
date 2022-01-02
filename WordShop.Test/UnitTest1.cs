using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WordShopExcersice.Data.Models;
using WordShopExcersice.Services;
using WordShopExcersice.Services.Services.PostalCodeService;
using WordShopExcersice.WebApi.Controllers;
using WordShopExcersice.WebApi.DTO;
using Xunit;

namespace WordShop.Test;

public class UnitTest1
{
    [Fact]
    public async Task Post_Code_Insert_To_database()
    {
       
        
        var serviceMock = new Mock<IPostalCode>();
       
        var excepted = true;
        var postCodeModels = new List<PostCodeModel>();
        var postcodedetail = new PostCodeModel
        {
            city = "test",
            country = "test",
            PostCode = "test",
            longitude = 2.33,
            latitude = 2.33,
            county = "test"
        };
        postCodeModels.Add(postcodedetail);
        //var result = await _postalCode.CreatePostalCode(postCodeModels);
        serviceMock
            .Setup(m => m.CreatePostalCode(postCodeModels))
            .Returns(Task.FromResult(true))
            .Verifiable();

        var x = await serviceMock.Object.CreatePostalCode(postCodeModels);

        Assert.Equal(excepted, x);
    }

    [Fact]
    public async Task Get_Post_Code_details()
    {
        var postcodedetail = new PostCodeModel
        {
            city = "AB101DQ",
            country = "Scotland",
            PostCode = "AberdeenCity",
            longitude = 2.108554,
            latitude = 57.144925,
            county = ""
        };

        var serviceMock = new Mock<IPostalCode>();
       
        serviceMock
            .Setup(m => m.GetSingleDetailsOfPostalCode("AB101DQ"))
            .Returns(Task.FromResult(postcodedetail))
            .Verifiable();

        var excepted = postcodedetail;
        var x = await serviceMock.Object.GetSingleDetailsOfPostalCode("AB101DQ");
        

        Assert.Equal(excepted, x);

    }

    [Fact]
    public async Task Get_Post_Code_Using_Coordinate()
    {
        var postcodedetail = new PostCodeModel
        {
            city = "AB101DQ",
            country = "Scotland",
            PostCode = "AberdeenCity",
            longitude = 2.108554,
            latitude = 57.144925,
            county = ""
        };
        var excepted = postcodedetail;

        var serviceMock = new Mock<IPostalCode>();

        serviceMock
            .Setup(m => m.GetPostalCodeUsingLatAndLng(57.144925, 2.108554))
            .Returns(Task.FromResult(postcodedetail))
            .Verifiable();

        var result = await serviceMock.Object.GetPostalCodeUsingLatAndLng(57.144925, 2.108554);

        Assert.Equal(excepted, result);
    }
}