curl https://download.visualstudio.microsoft.com/download/pr/e59acfc2-5987-43f9-bd03-0cbe446679e1/7db7313c1c99104279a69ccd47d160a1/dotnet-sdk-8.0.100-osx-x64.tar.gz \
    -o dotnet.tar.gz -L
mkdir dotnet
tar zxf dotnet.tar.gz -C dotnet
cat > .zshrc << EOL
export DOTNET_ROOT=~/dotnet
export PATH=\$PATH:~/dotnet
EOL
source .zshrc
mkdir Lab4
cd Lab4
dotnet new tool-manifest
dotnet tool install VBahrii --add-source http://10.0.2.2:5000/v3/index.json