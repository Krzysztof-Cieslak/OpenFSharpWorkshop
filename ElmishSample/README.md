# Elmish Sample

## Building and running the app

* Install JS dependencies: `npm install`
* **Move to `src` folder**: `cd src`
* Install F# dependencies: `dotnet restore`
* Start Fable daemon and [Webpack](https://webpack.js.org/) dev server: `dotnet fable npm-start`
* In your browser, open: http://localhost:8080/

> `dotnet fable yarn-start` (or `npm-start`) is used to start the Fable daemon and run a script in package.json concurrently. It's a shortcut of `yarn-run [SCRIP_NAME]`, e.g. `dotnet fable yarn-run start`.

If you are using VS Code + [Ionide](http://ionide.io/), you can also use the key combination: Ctrl+Shift+B (Cmd+Shift+B on macOS) instead of typing the `dotnet fable yarn-start` command. This also has the advantage that Fable-specific errors will be highlighted in the editor along with other F# errors.


## Tasks

### Task 1

In task 1 our aim is to make the counter displayed by default on the webpage working correctly. The current implementation has few problems - the model is just plain `int` (which is theoretically fine, but it doesn't happen too often in practice - usually we have more complex models using F# types), the message type doesn't contain any meaningful operations (and after changing it will require updating code in other places).
Start with editing `Counter/Types.fs` file, and then move to other files in `Counter` folder. Compiler errors should guide you to places requiring changes

### Task 2

In task 2 we will create completely new view (something simple - for example text box, and the string displaying reversed text from the text box), and we will plug it into our application. Our aim here is to understand how does navigation works in typical Elmish application.
Start with creating new component (State.fs, Types.fs, View.fs - use similar code structure to the one from Counter example) (And don't forget to add new files to fsproj file!). Then, edit Global.fs and Type.fs files, updating our types and helper functions to support new page. And in the end, move to State.fs and App.fs files - compiler should guide you to places requiring changes.

### Task 3

In task 3 we will create complex view - the view using previously created component (Counter) to build more complex view. The task is to create new page that will initially display one counter, but will also have a button that will add new counter to the view. The page should also have a text field that will display the sum of all counters on the page.

Start with creating new component (State.fs, Types.fs, View.fs - use similar code structure to the one from Counter example) (And don't forget to add new files to fsproj file!) using Counter component, and add it to navigation system.