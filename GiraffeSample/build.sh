if [ ! -e "paket.lock" ]
then
    exec mono .paket/paket.exe install
fi
dotnet restore src/GiraffeSample
dotnet build src/GiraffeSample

