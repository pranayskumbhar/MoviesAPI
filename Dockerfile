# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project files
COPY ["MoviesAPI/MoviesAPI.csproj", "MoviesAPI/"]
COPY ["Repository/Repository.csproj", "Repository/"]

# Restore dependencies
RUN dotnet restore "MoviesAPI/MoviesAPI.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/src/MoviesAPI"
RUN dotnet build "MoviesAPI.csproj" -c Release -o /app/build
RUN dotnet publish "MoviesAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "MoviesAPI.dll"]