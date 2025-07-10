FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy solution and project files
COPY ["Monolith.Core.sln", "."]
COPY ["Directory.Packages.props", "."]

# Copy all project files
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
COPY ["tests/WebBff.Tests/WebBff.Tests.csproj", "tests/WebBff.Tests/"]

# Restore dependencies
RUN dotnet restore "src/Web/WebBff/WebBff.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/src/Web/WebBff"
RUN dotnet build "WebBff.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WebBff.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

# Install ICU for internationalization
RUN apk add --no-cache icu-libs icu-data-full

# Set environment variables
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    TZ=America/Sao_Paulo \
    LC_ALL=pt_BR.UTF-8 \
    LANG=pt_BR.UTF-8 \
    ASPNETCORE_URLS=http://+:8080

WORKDIR /app

# Copy published files
COPY --from=publish /app/publish .

# Create non-root user for security
RUN addgroup -g 1001 -S appgroup && \
    adduser -S appuser -G appgroup -u 1001
USER appuser

ENTRYPOINT ["dotnet", "WebBff.dll"]