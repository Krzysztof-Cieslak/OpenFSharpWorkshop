module Counter.View

open Fable.Core
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types

let simpleButton txt action dispatch =
  div
    [ ClassName "column is-narrow" ]
    [ a
        [ ClassName "button"
          OnClick (fun _ -> action |> dispatch) ]
        [ str txt ] ]

let root model dispatch =
  div
    [ ClassName "columns is-vcentered" ]
    [ div [ ClassName "column" ] [ ]
      div
        [ ClassName "column is-narrow"
          Style
            [ CSSProp.Width "170px" ] ]
        [ str (sprintf "Counter value: %i" model) ]
      //TASK 1: dispatch correct actions for each button
      simpleButton "+1" NoOp dispatch
      simpleButton "-1" NoOp dispatch
      simpleButton "Reset" NoOp dispatch
      div [ ClassName "column" ] [ ] ]
