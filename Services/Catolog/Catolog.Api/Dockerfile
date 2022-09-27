#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Catolog/Catolog.Api/Catolog.Api.csproj", "Services/Catolog/Catolog.Api/"]
RUN dotnet restore "Services/Catolog/Catolog.Api/Catolog.Api.csproj"
COPY . .
WORKDIR "/src/Services/Catolog/Catolog.Api"
RUN dotnet build "Catolog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catolog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catolog.Api.dll"]
