# debt-snowball

This repository has 2 projects - the server application and the client application. Because this is the new version of Blazor, we have fine-grained control on where/how things render and can choose SSR/CSR depending on the needs of the component.

## The most important pieces so far are

- debt-snowball/Data where I have created the Models. Right now thery're crammed in one file, but I'll get to cleaning that up at some point. We have the DBContext setup there as well.
- debt-snowball/Account/Pages/DebtsPage.razor. This is where I have the debt creation form as well as the debt display. Nothing is styles as of yet, I just wanted to get data moving through. Probably the next to-dos here are to 1) Create the same type of form/display for payments as well as moving some of these pieces to smaller components rather than having it all in one file. Nothing is styled as of yet.
- I left in the example pages for the counter on the client side and weather on the server side so you can example the different patterns. Will probably remove them once we have the core functionality working and get to styling/tidying things up.
- If you spin up a local SQL server you should be able to run dotnet ef database update and populate it from the migrations in debt-snowball/Data, but if you have issues with that LMK and i can give you the connection string for the Live DB. It currently is IP restricted, so I'll need your IP address to grant access if we go that route. At some point we should maybe set up a staging and/or make sure we both have dev databases, but for now I'm not heartbroken if we have to reset if for some reason
- I have deployed this to a container app on Azure just to make sure everything is configured before we get too deep in the project. You should be able to access it here: https://debt-snowball-app-20231129131825.delightfulpebble-75fd41eb.eastus.azurecontainerapps.io/
- I would like to swap out Bootstrap for TailwindCSS or something more modern at some point, but we'll see how much time there is left for styling once the functionality is there (at least my opinion on the matter)

## Next Steps

- Create payment flow and display payments for each debt
- Set up snowball calculation object
- Create module to call the calculation API
- Display results of Calculation
- Look into data visualization
- Figure out how payment flow show work - is it two overlayed data visualizations?
- Clean up UI and add mascot
- Text and/or email notifications to update payments (MAYBE)
- AI Integration for encouragement (MAYBE)

Let me know if you have any other questions or need help getting set up in the editor. It definitely took me some wrestling with the project template to get the database/entity framework/etc set up.

## TailwindCSS

I have installed Tailwind CSS. If you have the Tailwind CLI installed, run the following commands in 2 separate PowerShell instances:

```bash
 npx tailwindcss -i .\Styles\app.css -o .\wwwroot\css\app.css --watch
```

The first is the watch command for the TailwindCSS JIT compiler that takes your CSS classes and adds them to the app's CSS file.

```bash
dotnet watch
```

This will build/run the project and watch the CSS file for changes and hot reload on save. This can get a little buggy at times and needs to be reset, but it works decent enough.
