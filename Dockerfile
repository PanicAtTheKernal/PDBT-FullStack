FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
# EXPOSE 80
# EXPOSE 443
EXPOSE 9000
EXPOSE 9001


# ENV ASPNETCORE_URLS="https://+:80;"
ENV ASPNETCORE_URLS="https://+:9000;"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

COPY ["./https/aspnetapp.pfx", "/https/"]
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["PDBT_CompleteStack.sln", "/src/"]

COPY ["./PDBT/PDBT.csproj", "/src/PDBT/"]
RUN dotnet restore "PDBT/PDBT.csproj"
COPY ./PDBT ./PDBT
WORKDIR "/src/PDBT"
RUN dotnet dev-certs https 
RUN dotnet build "PDBT.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PDBT.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "PDBT.dll", "--server.urls", "https://+:80;https://+:443;"]
ENTRYPOINT ["dotnet", "PDBT.dll", "--server.urls", "https://+:9000;https://+:9001;"]

# This is for development use

