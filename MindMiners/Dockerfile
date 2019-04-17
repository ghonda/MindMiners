FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MindMiners/MindMiners.csproj", "MindMiners/"]
COPY ["MindMiners.Domain/MindMiners.Domain.csproj", "MindMiners.Domain/"]
COPY ["MindMiners.CrossCutting.IoC/MindMiners.CrossCutting.IoC.csproj", "MindMiners.CrossCutting.IoC/"]
COPY ["MindMiners.CrossCutting.Infrastructure/MindMiners.CrossCutting.Infrastructure.csproj", "MindMiners.CrossCutting.Infrastructure/"]
COPY ["MindMiners.Application/MindMiners.Application.csproj", "MindMiners.Application/"]
COPY ["MindMiners.Data/MindMiners.Data.csproj", "MindMiners.Data/"]
RUN dotnet restore "MindMiners/MindMiners.csproj"
COPY . .
WORKDIR "/src/MindMiners"
RUN dotnet build "MindMiners.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MindMiners.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MindMiners.dll"]