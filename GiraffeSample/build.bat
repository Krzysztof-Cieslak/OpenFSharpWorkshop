IF NOT EXIST paket.lock (
    START /WAIT .paket/paket.exe install
)
dotnet restore src/GiraffeSample
dotnet build src/GiraffeSample

