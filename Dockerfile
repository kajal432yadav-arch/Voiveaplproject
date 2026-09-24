FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY VoiceAPI/VoiceAPI.csproj VoiceAPI/
RUN dotnet restore VoiceAPI/VoiceAPI.csproj

COPY . .

RUN dotnet publish VoiceAPI/VoiceAPI.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "VoiceAPI.dll"]
