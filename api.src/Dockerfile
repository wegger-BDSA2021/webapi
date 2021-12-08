FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# enabling swagger  
ENV ASPNETCORE_ENVIRONMENT=Development 

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["api.src.csproj", "./"]
RUN dotnet restore "./api.src.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "api.src.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "api.src.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT [ "dotnet", "api.src.dll" ]