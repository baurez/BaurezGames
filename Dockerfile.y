FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["BaurezGames/Server/BaurezGames.Server.csproj", "Server/"]
COPY ["BaurezGames/Client/BaurezGames.Client.csproj", "Client/"]
COPY ["BaurezGames/Shared/BaurezGames.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/BaurezGames.Server.csproj"


COPY . ./
WORKDIR "/src/"
RUN dotnet build "BaurezGames.sln" -c Release -o /app/build



FROM build AS publish
WORKDIR "/src/Server/"
RUN dotnet publish "BaurezGames.Server.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaurezGames.Server.dll"]