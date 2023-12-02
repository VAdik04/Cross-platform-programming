wget https://download.visualstudio.microsoft.com/download/pr/5226a5fa-8c0b-474f-b79a-8984ad7c5beb/3113ccbf789c9fd29972835f0f334b7a/dotnet-sdk-8.0.100-linux-x64.tar.gz \
    -O dotnet.tar.gz
mkdir dotnet
tar zxf dotnet.tar.gz -C dotnet
cat > .bashrc << EOL
export DOTNET_ROOT=~/dotnet
export PATH=\$PATH:~/dotnet
EOL