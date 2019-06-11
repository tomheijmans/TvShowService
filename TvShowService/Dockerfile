FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["TvShowService/TvShowService.csproj", "TvShowService/"]
COPY ["TvShowService.BusinessLogic/TvShowService.BusinessLogic.csproj", "TvShowService.BusinessLogic/"]
COPY ["TvShowServce.TvMazeClient/TvShowService.TvMazeClient.csproj", "TvShowServce.TvMazeClient/"]
COPY ["Common.Interfaces/Common.Interfaces.csproj", "Common.Interfaces/"]
RUN dotnet restore "TvShowService/TvShowService.csproj"
COPY . .
WORKDIR "/src/TvShowService"
RUN dotnet build "TvShowService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TvShowService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TvShowService.dll"]