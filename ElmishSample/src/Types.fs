module App.Types

open Global

//TASK 2: Add new message type
type Msg =
  | CounterMsg of Counter.Types.Msg

//TASK 2: Update model
type Model = {
    currentPage: Page
    counter: Counter.Types.Model
  }
