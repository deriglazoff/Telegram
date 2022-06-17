#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DinnerBot/DinnerBot.csproj", "DinnerBot/"]
RUN dotnet restore "DinnerBot/DinnerBot.csproj"
COPY . .
WORKDIR "/src/DinnerBot"
RUN dotnet build "DinnerBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DinnerBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DinnerBot.dll"]