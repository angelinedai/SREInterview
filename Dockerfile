FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

ARG BUILDCONFIG=RELEASE

COPY ./StockService/StockService.csproj ./StockService/
RUN dotnet restore ./StockService/

COPY ./ForgeRock/ForgeRock.csproj ./ForgeRock/
RUN dotnet restore ./ForgeRock/

COPY ./StockService/ ./StockService/
RUN dotnet build ./StockService/

COPY ./ForgeRock/ ./ForgeRock/
RUN dotnet build ./ForgeRock/

FROM build AS publish
RUN dotnet publish ./ForgeRock/ForgeRock.csproj -c $BUILDCONFIG -o /apps --configuration Release

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /apps

COPY --from=publish /apps .

ENTRYPOINT ["dotnet", "ForgeRock.dll"]