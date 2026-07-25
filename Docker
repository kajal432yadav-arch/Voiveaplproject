# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copy only the project file first to leverage Docker layer caching
COPY VoiceAPI/VoiceAPI.csproj VoiceAPI/
RUN dotnet restore VoiceAPI/VoiceAPI.csproj

# Now copy the rest of the source
COPY . .
RUN dotnet publish VoiceAPI/VoiceAPI.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime
WORKDIR /app

# Create and use a non-root user
RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "VoiceAPI.dll"]
