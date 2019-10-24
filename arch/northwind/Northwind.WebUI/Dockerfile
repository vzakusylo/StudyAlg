FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["arch/northwind/Northwind.WebUI/Northwind.WebUI.csproj", "arch/northwind/Northwind.WebUI/"]

RUN dotnet restore "arch/northwind/Northwind.WebUI/Northwind.WebUI.csproj"
COPY . .
WORKDIR "/src/arch/northwind/Northwind.WebUI"
RUN dotnet build "Northwind.WebUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Northwind.WebUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Northwind.WebUI.dll"]