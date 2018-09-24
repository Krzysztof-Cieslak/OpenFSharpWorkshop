module Server

open Saturn
open Config

// -------------------------
// TASK 4
// Last abstraction introduced by Saturn is `application` - it aims to replace ugly ASP.NET Core configuration with set of
// of high level feature flags that hides all the complexity and imperative configuration.
//
// TASK: Create the `pipeline` and plug it into application using `pipe_through` operation.
// This pipeline will be executed for every request  comming to the server.
// It's can be very useful for cross cutting concernes. Plug  built-in `head` and `requestId` into your pipeline.
//
// TASK 2: Create error handler for your application using custom handler that will render error view (defined in InternalError module)
// and `error_handler` operation in `application` CE.
// -------------------------

let app = application {
    use_router Router.appRouter
    url "http://0.0.0.0:8085/"
    use_static "static"
}

[<EntryPoint>]
let main _ =
    printfn "Working directory - %s" (System.IO.Directory.GetCurrentDirectory())
    run app
    0 // return an integer exit code