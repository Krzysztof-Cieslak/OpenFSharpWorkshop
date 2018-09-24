# GiraffeSample

A [Giraffe](https://github.com/giraffe-fsharp/Giraffe) web application, which has been created via the `dotnet new giraffe` command.

## Build and test the application

### Windows

Run the `build.bat` script in order to restore and build the application:

```
> ./build.bat
```

### Linux/macOS

Run the `build.sh` script in order to restore and build the application:

```
$ ./build.sh
```

## Run the application

After a successful build you can start the web application by executing the following command in your terminal:

```
dotnet watch --project src/GiraffeSample/GirafeSample.fsproj
```

After the application has started visit [http://localhost:8080](http://localhost:8080) in your preferred browser. Application is running in the watch mode - it means it will recompile and restart on every file save.

## Tasks

All the tasks are in Program.fs file.