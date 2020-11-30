#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["WebAppSignalR/WebAppSignalR.csproj", "WebAppSignalR/"]
RUN dotnet restore "WebAppSignalR/WebAppSignalR.csproj"
COPY . .
WORKDIR "/src/WebAppSignalR"
RUN dotnet build "WebAppSignalR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppSignalR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppSignalR.dll"]