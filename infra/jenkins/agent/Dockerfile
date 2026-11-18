# Official Jenkins Inbound Agent image with Java 21
FROM jenkins/inbound-agent:jdk21

# Change to root user to install software
USER root

# Install the dependencies required by the .NET SDK
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    libicu76 \
    curl \
    ca-certificates && \
    rm -rf /var/lib/apt/lists/*

# Install the .NET 10.0.403 SDK
RUN curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh && \
    bash /tmp/dotnet-install.sh --version 10.0.403 --install-dir /usr/share/dotnet && \
    ln -s /usr/share/dotnet/dotnet /usr/local/bin/dotnet && \
    rm /tmp/dotnet-install.sh

#  Configuration of the .NET environment variables
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="${DOTNET_ROOT}:${PATH}"

# Change to unprivileged user
USER jenkins
