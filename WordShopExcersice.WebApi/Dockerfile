#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WordShopExcersice.WebApi/WordShopExcersice.WebApi.csproj", "WordShopExcersice.WebApi/"]
COPY ["WordShopExcersice.Data/WordShopExcersice.Data.csproj", "WordShopExcersice.Data/"]
COPY ["WordShopExcersice.Services/WordShopExcersice.Services.csproj", "WordShopExcersice.Services/"]
RUN dotnet restore "WordShopExcersice.WebApi/WordShopExcersice.WebApi.csproj"
COPY . .
WORKDIR "/src/WordShopExcersice.WebApi"
RUN dotnet build "WordShopExcersice.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WordShopExcersice.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WordShopExcersice.WebApi.dll"]