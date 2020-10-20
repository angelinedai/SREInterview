FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

ARG BUILDCONFIG=RELEASE

COPY ./StockService/StockService.csproj ./StockService/
RUN dotnet restore ./StockService/

COPY ./SREInterview/SREInterview.csproj ./SREInterview/
RUN dotnet restore ./SREInterview/

COPY ./StockService/ ./StockService/
RUN dotnet build ./StockService/

COPY ./SREInterview/ ./SREInterview/
RUN dotnet build ./SREInterview/

FROM build AS publish
RUN dotnet publish ./SREInterview/SREInterview.csproj -c $BUILDCONFIG -o /apps --configuration Release

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /apps

COPY --from=publish /apps .

ENTRYPOINT ["dotnet", "SREInterview.dll"]