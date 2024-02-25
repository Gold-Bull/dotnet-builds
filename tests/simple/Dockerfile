# syntax=docker/dockerfile:1 
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG TARGETARCH
ARG RUNTIME_FRAMEWORK_VERSION

ENV DEBIAN_FRONTEND=noninteractive

COPY nuget-packages/ /root/nuget-packages/
COPY tests/simple/ /root/tests/

RUN <<EOT bash
set -ex
cd /root/tests
ls -l /root/tests/
ls -l /root/nuget-packages/
dotnet nuget add source /root/nuget-packages/ --name nuget-packages
dotnet restore /p:Configuration=Release /p:RuntimeIdentifier=linux-$TARGETARCH /p:RuntimeFrameworkVersion=$RUNTIME_FRAMEWORK_VERSION
dotnet build --no-restore /p:Configuration=Release /p:RuntimeIdentifier=linux-$TARGETARCH /p:RuntimeFrameworkVersion=$RUNTIME_FRAMEWORK_VERSION /p:SelfContained=true
EOT

#----------------------------------------------------------------------------------------------------

FROM ghcr.io/gold-bull/dotnet-ubuntu-base:ubuntu-20.04-latest AS final

ARG TARGETARCH

ENV DEBIAN_FRONTEND=noninteractive

RUN --mount=type=bind,target=/root/tests/,source=/root/tests/,from=build <<EOT bash
set -ex
ls -l /root/tests/bin/Release/net8.0/linux-$TARGETARCH/
EOT
