# syntax=docker/dockerfile:1
ARG DOTNET_SDK_VERSION
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
dotnet nuget add source /root/nuget-packages/ --name nuget-packages
dotnet build /p:TargetFramework=net$DOTNET_SDK_VERSION /p:Configuration=$BUILD_CONFIGURATION /p:RuntimeIdentifier=linux-$TARGETARCH /p:RuntimeFrameworkVersion=$RUNTIME_FRAMEWORK_VERSION /p:SelfContained=true
EOT

#----------------------------------------------------------------------------------------------------

FROM ghcr.io/gold-bull/dotnet-runtime-base:ubuntu-20.04-latest AS ubt20.04

ARG DOTNET_SDK_VERSION
ARG BUILD_CONFIGURATION
ARG TARGETARCH

ENV DEBIAN_FRONTEND=noninteractive

COPY --from=build /root/tests/bin/$BUILD_CONFIGURATION/net$DOTNET_SDK_VERSION/linux-$TARGETARCH/ /root/tests/

RUN <<EOT bash
set -ex
/root/tests/simple
EOT

#----------------------------------------------------------------------------------------------------

FROM ghcr.io/gold-bull/dotnet-runtime-base:ubuntu-22.04-latest AS ubt22.04

ARG DOTNET_SDK_VERSION
ARG BUILD_CONFIGURATION
ARG TARGETARCH

ENV DEBIAN_FRONTEND=noninteractive

COPY --from=ubt20.04 /root/tests/ /root/tests/

RUN <<EOT bash
set -ex
/root/tests/simple
EOT
