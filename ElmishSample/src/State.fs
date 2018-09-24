module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open Global
open Types

//TASK 2: Update parser
let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map Counter (s "counter")
  ]

let urlUpdate (result: Option<Page>) model =
  match result with
  | None ->
    console.error("Error parsing url")
    model,Navigation.modifyUrl (toHash model.currentPage)
  | Some page ->
      { model with currentPage = page }, []

//TASK 2: Update init function
let init result =
  let (counter, counterCmd) = Counter.State.init()
  let (model, cmd) =
    urlUpdate result
      { currentPage = Counter
        counter = counter }
  model, Cmd.batch [ cmd
                     Cmd.map CounterMsg counterCmd ]

//TASK 2: Update model function
let update msg model =
  match msg with
  | CounterMsg msg ->
      let (counter, counterCmd) = Counter.State.update msg model.counter
      { model with counter = counter }, Cmd.map CounterMsg counterCmd
