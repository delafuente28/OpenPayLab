# Official Jenkins Inbound Agent image with Java 21
FROM jenkins/inbound-agent:jdk21

# Change to root user to install software
USER root

# Install the dependencies required by the .NET SDK
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    libicu76 \
    curl \
    ca-certificates \
    gnupg && \
    rm -rf /var/lib/apt/lists/*

# Install and verify the .NET 10.0.403 SDK installer
RUN set -eux; \
    curl --proto '=https' --proto-redir '=https' -fsSL \
        https://dot.net/v1/dotnet-install.sh \
        -o /tmp/dotnet-install.sh; \
    curl --proto '=https' --proto-redir '=https' -fsSL \
        https://dot.net/v1/dotnet-install.sig \
        -o /tmp/dotnet-install.sig; \
    curl --proto '=https' --proto-redir '=https' -fsSL \
        https://dot.net/v1/dotnet-install.asc \
        -o /tmp/dotnet-install.asc; \
    ACTUAL_FINGERPRINT="$(gpg --show-keys --with-colons /tmp/dotnet-install.asc | awk -F: '$1 == "fpr" {print $10; exit}')"; \
    EXPECTED_FINGERPRINT="2B930AB1228D11D5D7F6B6ACB9CF1A51FC7D3ACF"; \
    test "$ACTUAL_FINGERPRINT" = "$EXPECTED_FINGERPRINT"; \
    gpg --batch --import /tmp/dotnet-install.asc; \
    gpg --batch --verify /tmp/dotnet-install.sig /tmp/dotnet-install.sh; \
    bash /tmp/dotnet-install.sh --version 10.0.403 --install-dir /usr/share/dotnet; \
    ln -s /usr/share/dotnet/dotnet /usr/local/bin/dotnet; \
    rm -f /tmp/dotnet-install.sh /tmp/dotnet-install.sig /tmp/dotnet-install.asc

# Configuration of the .NET environment variables
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="${DOTNET_ROOT}:${PATH}"

# Change to unprivileged user
USER jenkins
