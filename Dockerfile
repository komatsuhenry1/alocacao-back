# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura as dependências
COPY ["AlocacaoDeVeiculos/AlocacaoDeVeiculos.csproj", "AlocacaoDeVeiculos/"]
RUN dotnet restore "AlocacaoDeVeiculos/AlocacaoDeVeiculos.csproj"

# Copia o restante dos arquivos e compila
COPY . .
WORKDIR "/src/AlocacaoDeVeiculos"
RUN dotnet build "AlocacaoDeVeiculos.csproj" -c Release -o /app/build

# Publica a aplicação
FROM build AS publish
RUN dotnet publish "AlocacaoDeVeiculos.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlocacaoDeVeiculos.dll"]