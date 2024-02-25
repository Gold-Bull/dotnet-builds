# syntax=docker/dockerfile:1
ARG DOTNET_SDK_VERSION=8.0
ARG BUILD_CONFIGURATION=Release

#----------------------------------------------------------------------------------------------------

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:$DOTNET_SDK_VERSION AS build

ARG DOTNET_SDK_VERSION
ARG BUILD_CONFIGURATION
ARG TARGETARCH
ARG RUNTIME_FRAMEWORK_VERSION

ENV DEBIAN_FRONTEND=noninteractive

COPY tests/simple/ /root/tests/

RUN --mount=type=bind,source=nuget-packages/,target=/root/nuget-packages/ <<EOT bash
set -ex
cd /root/tests
ls -l /root/tests/
ls -l /root/nuget-packages/
dotnet nuget add source /root/nuget-packages/ --name nuget-packages
dotnet build /p:Configuration=$BUILD_CONFIGURATION /p:RuntimeIdentifier=linux-$TARGETARCH /p:RuntimeFrameworkVersion=$RUNTIME_FRAMEWORK_VERSION /p:SelfContained=true
EOT

#----------------------------------------------------------------------------------------------------

FROM ghcr.io/gold-bull/dotnet-ubuntu-base:ubuntu-20.04-latest AS final

ARG DOTNET_SDK_VERSION
ARG BUILD_CONFIGURATION
ARG TARGETARCH

ENV DEBIAN_FRONTEND=noninteractive

RUN --mount=type=bind,source=/root/tests/bin/$BUILD_CONFIGURATION/net$DOTNET_SDK_VERSION/linux-$TARGETARCH/,target=/root/tests/,from=build <<EOT bash
set -ex
/root/tests/simple
EOT
