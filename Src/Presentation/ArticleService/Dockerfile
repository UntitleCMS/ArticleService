#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/Presentation/ArticleService/ArticleService.csproj", "Src/Presentation/ArticleService/"]
COPY ["Src/Core/Application/Application.csproj", "Src/Core/Application/"]
COPY ["Src/Core/Domain/Domain.csproj", "Src/Core/Domain/"]
COPY ["Src/Infrastructure/Infrastructure/Infrastructure.csproj", "Src/Infrastructure/Infrastructure/"]
RUN dotnet restore "Src/Presentation/ArticleService/ArticleService.csproj"
COPY . .
WORKDIR "/src/Src/Presentation/ArticleService"
RUN dotnet build "ArticleService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArticleService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
LABEL org.opencontainers.image.source = "https://github.com/UntitleCMS/ArticleService"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleService.dll"]
