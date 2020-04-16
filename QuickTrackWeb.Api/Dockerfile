#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["QuickTrackWeb.Api/QuickTrackWeb.Api.csproj", "QuickTrackWeb.Api/"]
RUN dotnet restore "QuickTrackWeb.Api/QuickTrackWeb.Api.csproj"
COPY . .
WORKDIR "/src/QuickTrackWeb.Api"
RUN dotnet build "QuickTrackWeb.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuickTrackWeb.Api.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS http://*:5000
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickTrackWeb.Api.dll"]