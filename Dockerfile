#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["QuickTrackWeb.csproj", ""]
COPY ["QuickTrackWeb.Shared/QuickTrackWeb.Shared.csproj", "QuickTrackWeb.Shared/"]
RUN dotnet restore "./QuickTrackWeb.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "QuickTrackWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuickTrackWeb.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS http://*:5000git
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickTrackWeb.dll"]