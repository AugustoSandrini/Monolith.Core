FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base
WORKDIR /app

EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

ARG NUGET_USER
ARG NUGET_PACKAGES_TOKEN
ENV NUGET_USER=$NUGET_USER
ENV NUGET_PACKAGES_TOKEN=$NUGET_PACKAGES_TOKEN

WORKDIR /src

COPY ["Directory.Packages.props", "."]
COPY ["nuget.config", "."]

COPY ["src/Web/WebBff/WebBff.csproj", "src/Web/WebBff/"]
COPY ["src/Common/Common/Common.csproj", "src/Common/Common/"]
COPY ["src/Core/Core.Application/Core.Application.csproj", "src/Core/Core.Application/"]
COPY ["src/Core/Core.Domain/Core.Domain.csproj", "src/Core/Core.Domain/"]
COPY ["src/Core/Core.Shared/Core.Shared.csproj", "src/Core/Core.Shared/"]
COPY ["src/Core/Core.Infrastructure/Core.Infrastructure.csproj", "src/Core/Core.Infrastructure/"]
COPY ["src/Modules/User/User.Shared/User.Shared.csproj", "src/Modules/User/User.Shared/"]
COPY ["src/Modules/User/User.Domain/User.Domain.csproj", "src/Modules/User/User.Domain/"]
COPY ["src/Core/Core.Persistence/Core.Persistence.csproj", "src/Core/Core.Persistence/"]
COPY ["src/Core/Core.Endpoints/Core.Endpoints.csproj", "src/Core/Core.Endpoints/"]
COPY ["src/Modules/User/User.Infrastructure/User.Infrastructure.csproj", "src/Modules/User/User.Infrastructure/"]
COPY ["src/Modules/User/User.Endpoints/User.Endpoints.csproj", "src/Modules/User/User.Endpoints/"]
COPY ["src/Modules/User/User.Application/User.Application.csproj", "src/Modules/User/User.Application/"]
COPY ["src/Modules/User/User.Persistence/User.Persistence.csproj", "src/Modules/User/User.Persistence/"]

RUN dotnet restore "./src/Web/WebBff/WebBff.csproj"
COPY . .
WORKDIR "/src/src/Web/WebBff"
RUN dotnet build "./WebBff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./WebBff.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

RUN apk add --no-cache icu-libs
RUN apk add --no-cache icu-data-full
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    TZ=America/Sao_Paulo
ENV LC_ALL=pt_BR.UTF-8
ENV LANG=pt_BR.UTF-8

WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBff.dll"]