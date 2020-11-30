#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BlazorSignalRClient/BlazorSignalRClient.csproj", "BlazorSignalRClient/"]
RUN dotnet restore "BlazorSignalRClient/BlazorSignalRClient.csproj"
COPY . .
WORKDIR "/src/BlazorSignalRClient"
RUN dotnet build "BlazorSignalRClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorSignalRClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorSignalRClient.dll"]