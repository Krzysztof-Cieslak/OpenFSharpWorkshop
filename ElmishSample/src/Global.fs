module Global

//TASK 2: Add new page
type Page =
  | Counter

//TASK 2: Add new page
let toHash page =
  match page with
  | Counter -> "#counter"
