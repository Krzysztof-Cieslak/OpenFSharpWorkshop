module Counter.State

open Elmish
open Types

let init () : Model * Cmd<Msg> =
  0, []

//TASK 1: implement update function
let update msg model : Model * Cmd<Msg> =
  match msg with
  | NoOp -> 0, []
