module GiraffeSample.App

open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Giraffe.ModelBinding
open Giraffe.Core
open Microsoft.AspNetCore.Http

// ---------------------------------
// TASK 4
// So far we've been only GET requests and some string input from an URL.
// But we can also just read some content from the body, for example as a JSON, and deserialize it into F# type
//
// TASK: Define your model (F# record representing request body),
// read it (some members of the `ctx` object may be useful), and then plug the handler into your webApp.
// ---------------------------------

let postHandler (next: HttpFunc) (ctx: HttpContext) : HttpFuncResult =
    task {
        return None
    }


// ---------------------------------
// TASK 3
// Giraffe can be used not only to create APIs that return data in some format,
// but can be also used for server-side rendering. It supports several ways
// of defining views (including Razor) but in this example we will
// use F# specific DSL based on the lists
// Below, layout function defines general structure of every webpage of your application
// If you're familiar with ASP.NET you can treat this function as the master view.
//
// TASK: Create some really nice HTML view in the index function,
// display it in the indexHandler function (you may find `htmlView` function useful),
// and then plug `indexHandler` into your webApp.
// ---------------------------------

module Views =
    open GiraffeViewEngine

    let layout (content: XmlNode list) =
        html [] [
            head [] [
                title []  [ encodedText "GiraffeSample" ]
                link [ _rel  "stylesheet"
                       _type "text/css"
                       _href "/main.css" ]
            ]
            body [] content
        ]

    let index string =
        [] |> layout


let indexHandler (name : string) =
    text "Index"

// ---------------------------------
// TASK 2
// While Giraffe contains set of built-in HttpHandlers, it's sometimes not enough.
// But HttpHandlers are normal F# functions which means you can create custom handlers easily
// HttpHandler is a function of type: HttpFunc -> HttpContext -> Task<Option<HttpContext>>.
// HttpHandler are using CPS patter, and they can result in 3 states - they execute `next` handler,
// return `Some` (which stops further processing) but is considered successful result, or they return `None`.
//
// TASK: Create an HttpHandler that will test if request url contians given string. Plugin it into your webApp above
// ---------------------------------

let urlContainsTest (testString : string) (next: HttpFunc) (ctx: HttpContext) : HttpFuncResult =
    task { return None }

// ---------------------------------
// TASK 1
// Giraffe applications are build by combining together "smaller" HttpHandlers into
// "bigger" HttpHandlers. In the end your whole Giraffe application is on HttpHandler.
//
// There are 2 basic ways of combining HttpHandlers:
// 1. "Sequentially" - using >=> operator - it invokes HttpHandler on the left and if
//   it was successful passes results to HttpHandler on the right
// 2. "Horizontally" - using choose function - it accepts as parameter list of HttpHandlers,
//   iterest over them, and uses as its result, result of the first HttpHandler that was successful
//
// Using those 2 functions it is possible to model almost any decision tree that will be used to handle incoming request.
//
// TASK: Using `choose`, `>=>` combinators, built-in HttpHandlers `GET`, `route`, `routef`, `text`
// create small "Hello World" application - depending on the request urls it should return different strings to the user.
// `route` and `routef` handlers will help you to do url tests, and `text` is used to return text to the user
// ---------------------------------

let webApp =
    choose [
        setStatusCode 404 >=> text "Not Found" ] // Unmatched case response.




// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

let configureApp (app : IApplicationBuilder) =
    let env = app.ApplicationServices.GetService<IHostingEnvironment>()
    (match env.IsDevelopment() with
    | true  -> app.UseDeveloperExceptionPage()
    | false -> app.UseGiraffeErrorHandler errorHandler)
        .UseStaticFiles()
        .UseGiraffe(webApp)

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    let filter (l : LogLevel) = l.Equals LogLevel.Error
    builder.AddFilter(filter).AddConsole().AddDebug() |> ignore

[<EntryPoint>]
let main _ =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot     = Path.Combine(contentRoot, "WebRoot")

    WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(contentRoot)
        .UseWebRoot(webRoot)
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .ConfigureLogging(configureLogging)
        .UseUrls("http://localhost:8080")
        .Build()
        .Run()
    0