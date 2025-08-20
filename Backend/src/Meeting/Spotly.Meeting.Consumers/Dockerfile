FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Meeting/Spotly.Meeting.Consumers/Spotly.Meeting.Consumers.csproj", "src/Meeting/Spotly.Meeting.Consumers/"]
RUN dotnet restore "src/Meeting/Spotly.Meeting.Consumers/Spotly.Meeting.Consumers.csproj"
COPY . .
WORKDIR "/src/src/Meeting/Spotly.Meeting.Consumers"
RUN dotnet build "./Spotly.Meeting.Consumers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Spotly.Meeting.Consumers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spotly.Meeting.Consumers.dll"]
