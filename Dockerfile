# Etapa 1: Compilacion y publicacion (Build stage)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar solucion y archivos de proyecto para restaurar dependencias aprovechando cache de capas
COPY ["Lab11 - AdhelmarFlores.sln", "./"]
COPY ["Lab11 - AdhelmarFlores/Lab11 - AdhelmarFlores.csproj", "Lab11 - AdhelmarFlores/"]
COPY ["Lab11 - AdhelmarFlores.Application/Lab11 - AdhelmarFlores.Application.csproj", "Lab11 - AdhelmarFlores.Application/"]
COPY ["Lab11 - AdhelmarFlores.Domain/Lab11 - AdhelmarFlores.Domain.csproj", "Lab11 - AdhelmarFlores.Domain/"]
COPY ["Lab11 - AdhelmarFlores.Infrastructure/Lab11 - AdhelmarFlores.Infrastructure.csproj", "Lab11 - AdhelmarFlores.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "Lab11 - AdhelmarFlores.sln"

# Copiar el codigo fuente completo de los 4 proyectos
COPY ["Lab11 - AdhelmarFlores/", "Lab11 - AdhelmarFlores/"]
COPY ["Lab11 - AdhelmarFlores.Application/", "Lab11 - AdhelmarFlores.Application/"]
COPY ["Lab11 - AdhelmarFlores.Domain/", "Lab11 - AdhelmarFlores.Domain/"]
COPY ["Lab11 - AdhelmarFlores.Infrastructure/", "Lab11 - AdhelmarFlores.Infrastructure/"]

# Compilar y publicar la aplicacion Web principal en modo Release
WORKDIR "/src/Lab11 - AdhelmarFlores"
RUN dotnet publish "Lab11 - AdhelmarFlores.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Entorno de ejecucion ligero (Runtime stage)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Configuracion de puerto y entorno para Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Copiar binarios publicados desde la etapa de compilacion
COPY --from=build /app/publish .

# Comando de inicio del contenedor
ENTRYPOINT ["dotnet", "Lab11 - AdhelmarFlores.dll"]

