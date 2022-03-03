FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieShopMVC/MovieShopMVC.csproj", "MovieShopMVC/"]
RUN dotnet restore "MovieShopMVC/MovieShopMVC.csproj"
COPY . .
WORKDIR "/src/MovieShopMVC"
RUN dotnet build "MovieShopMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieShopMVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieShopMVC.dll"]
