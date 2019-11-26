FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Usavc.Microservices.Appointment/USAVC.Microservices.Appointment.csproj", "Usavc.Microservices.Appointment/"]
COPY ["Usavc.Microservices.Common/USAVC.Microservices.Common.csproj", "Usavc.Microservices.Common/"]
RUN dotnet restore "Usavc.Microservices.Appointment/USAVC.Microservices.Appointment.csproj"
COPY . .
WORKDIR "/src/Usavc.Microservices.Appointment"
RUN dotnet build "USAVC.Microservices.Appointment.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "USAVC.Microservices.Appointment.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "USAVC.Microservices.Appointment.dll"]
