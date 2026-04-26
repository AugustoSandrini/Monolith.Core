FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Monolith.Core.sln", "."]

COPY ["src/Web/WebBff/WebBff.csproj", "src/Web/WebBff/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Core/Core.Application/Core.Application.csproj", "src/Core/Core.Application/"]
COPY ["src/Core/Core.Domain/Core.Domain.csproj", "src/Core/Core.Domain/"]
COPY ["src/Core/Core.Shared/Core.Shared.csproj", "src/Core/Core.Shared/"]
COPY ["src/Core/Core.Infrastructure/Core.Infrastructure.csproj", "src/Core/Core.Infrastructure/"]
COPY ["src/Core/Core.Persistence/Core.Persistence.csproj", "src/Core/Core.Persistence/"]
COPY ["src/Core/Core.Endpoints/Core.Endpoints.csproj", "src/Core/Core.Endpoints/"]
COPY ["src/Modules/User.Shared/User.Shared.csproj", "src/Modules/User.Shared/"]
COPY ["src/Modules/User.Domain/User.Domain.csproj", "src/Modules/User.Domain/"]
COPY ["src/Modules/User.Infrastructure/User.Infrastructure.csproj", "src/Modules/User.Infrastructure/"]
COPY ["src/Modules/User.Application/User.Application.csproj", "src/Modules/User.Application/"]
COPY ["src/Modules/User.Persistence/User.Persistence.csproj", "src/Modules/User.Persistence/"]

RUN dotnet restore "src/Web/WebBff/WebBff.csproj"

COPY . .

WORKDIR "/src/src/Web/WebBff"
RUN dotnet build "WebBff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBff.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    TZ=America/Sao_Paulo \
    LC_ALL=pt_BR.UTF-8 \
    LANG=pt_BR.UTF-8 \
    ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production

WORKDIR /app
COPY --from=publish /app/publish .

RUN groupadd -g 1001 appgroup && \
    useradd -u 1001 -g appgroup -s /bin/false -M appuser && \
    chown -R appuser:appgroup /app
USER appuser

ENTRYPOINT ["dotnet", "WebBff.dll"]
