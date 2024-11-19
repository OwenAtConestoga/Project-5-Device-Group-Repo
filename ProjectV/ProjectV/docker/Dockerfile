FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /src
COPY . .
RUN msbuild /p:Configuration=Release /p:OutputPath=/app/publish

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2019
WORKDIR /app
COPY --from=build /app/publish .
ENV LISTENER_IP="0.0.0.0"
ENV LISTENER_PORT="5000"
ENTRYPOINT ["ProjectV.exe"]