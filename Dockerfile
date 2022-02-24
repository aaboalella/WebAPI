FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5050
EXPOSE 5050

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV PORT=5050
ENTRYPOINT ["dotnet","WebAPI_Docker.dll"]