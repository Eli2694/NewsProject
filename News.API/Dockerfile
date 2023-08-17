FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["News.API/News.API.csproj", "News.API/"]
COPY ["News.Entity/News.Entity.csproj", "News.Entity/"]
COPY ["Logger/Logger.csproj", "Logger/"]
COPY ["News.DAL/News.DAL.csproj", "News.DAL/"]
COPY ["News.Model/News.Model.csproj", "News.Model/"]
RUN dotnet restore "News.API/News.API.csproj"
COPY . .
WORKDIR "/src/News.API"
RUN dotnet build "News.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "News.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "News.API.dll"]