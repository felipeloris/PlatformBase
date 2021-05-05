#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Loris.Webapi/Loris.Webapi.csproj", "Loris.Webapi/"]
RUN dotnet restore "Loris.Webapi/Loris.Webapi.csproj"
COPY . .
WORKDIR "/src/Loris.Webapi"
RUN dotnet build "Loris.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Loris.Webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Loris.Webapi.dll"]
