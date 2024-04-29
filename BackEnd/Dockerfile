FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["URLShortener/URLShortener.csproj", "URLShortener/"]
RUN dotnet restore "URLShortener/URLShortener.csproj"
COPY . .
WORKDIR "/src/URLShortener"
RUN dotnet build "URLShortener.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "URLShortener.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS tools
RUN dotnet tool install --global dotnet-ef

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=tools /root/.dotnet/tools .
ENV PATH="/app:${PATH}"
ENTRYPOINT ["dotnet", "URLShortener.dll"]
