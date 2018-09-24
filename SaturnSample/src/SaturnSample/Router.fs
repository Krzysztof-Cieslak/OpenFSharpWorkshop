module Router

open Saturn
open Giraffe.Core
open Giraffe.ResponseWriters
open Giraffe.Common

// -------------------------
// TASK 3
// Most powerful Saturn abstraction is `controller`. It's an abstraction that provides common set of functionalites
// That are used for building (REST) controllers following set of opinionated conventions. Using it you may focus on solving
// your business issues and not on typical concernes such as routing, output serialization, or API versioning.
//
// TASK: Add more functionalities to controller below - implement action such as `show`, `create` and `delete`.
// Operations should modify our amaizing in-memory database. Plug the controller into the router (for example "/book" url)
//
// Notice that from actions you can return either normal HttpHandler result (Task<Option<HttpContext>>)
// or just normal F# data structures that will be automatically serialized.
//
// TASK 2: Create BookV2 type and bookV2Controller that will provide similar set of
// functionalities but for the new structure. In controller CE Use `version` operation
// to enable versioning support. Plug new controller *above* the old one, but with same url (for example "/book").
// Test behaviour of application in the client, use `x-controller-version` header to call the V2 controller.
//
// TASK 3: One another capabilities of controllers is providing custom HttpHandlers for certain actions in controller.
// For example you may need to authorize a user before handling POST request, but for GET requests you just do IP check. Or you do nothing.
// Add custom HttpHandler (for example setting some additional header) for `create` and `delete`
// actions - it can be done using `plug` operation in `controller` CEs
// -------------------------

type Book = { Title: string }

let mutable books : Book list = []

let bookController = controller {
    version "asd"
    index (fun ctx -> task { return books }  )
}

let browser = pipeline {
    plug acceptHtml
    plug putSecureBrowserHeaders
    plug fetchSession
    set_header "x-pipeline-type" "Browser"
}

// -------------------------
// TASK 2
// The simplest Saturn abstraction is `pipeline` - it's declarative way of combining set of HttpHandlers
// together, in a same way as with >=> operator in plain Giraffe. `pipeline` CE contains set of custom
// operations that provide built-in functionality for many use cases, or can use any custom HttpHandler using
// `plug` operation. What's important - pipelines are transformed into normal HttpHandlers, they doesn't provide any
// custom abstraction layer on top of the HttpHandler. It means they can be
// easily composed (together, with other parts of Saturn or with plain Giraffe)
//
// TASK: Create couple of pipelines that will set headers, correct response codes,
// some content (text, xml, json operations may be useful) and plug them into your router
// -------------------------

let defaultView = router {
    get "/" (htmlView Index.layout)
    get "/index.html" (redirectTo false "/")
    get "/default.html" (redirectTo false "/")
}

let browserRouter = router {
    forward "" defaultView //Use the default view
}

// -------------------------
// TASK 1
// The first important Saturn abstraction is `router`.
// As name suggest, `router` CE is responsible for creating routing tree for the application.
// It provides set of custom operations such as `get`, `getf`, `post`, `forward` ... that enables
// you to create routing based on the request method and url. What's important - routers can be easily composed together
// providing subrouting capabilities (as shown in an example above).
// Custom operations that are provided by CE are accepting standard HttpHandlers
// as an action handlers. What's more routers are also transformed into normal HttpHandlers wich means everything can be composed.
//
// TASK: Build routing capabilities similar to the one from previous tasks. Add few routes, different methods,
// some simple handlers (just returning text) and test everything in the browser.
// -------------------------


let appRouter = router {
    // forward "/api" apiRouter
    forward "" browserRouter
}