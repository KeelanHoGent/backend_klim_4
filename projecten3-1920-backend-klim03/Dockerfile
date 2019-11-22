FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
RUN ["apt-get", "update"]
RUN ["apt-get", "-y", "install", "wkhtmltopdf"]
RUN ["apt-get", "-y", "install", "xvfb"]
RUN ["apt-get", "-y", "install", "libgdiplus"]
RUN ["apt-get", "-y", "install", "libfontconfig"]
RUN ["apt-get", "-y", "install", "libc6-dev"]
RUN ["apt-get", "-y", "install", "openssl"]
RUN ["apt-get", "-y", "install", "libssl1.0-dev"]
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "projecten3-1920-backend-klim03.dll"]