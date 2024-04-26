The solution consists of 2 parts:
	1) Server - Need to be deployed using IIS/IIS Express (From Visual Studio).
	2) Client - From command prompt ng serve command needs to executed.

From the client angular project, I have removed the node_modules folder. Before starting up the project, npm install command needs to be executed so that all the modules gets downloaded.

Default port for API to work on localhost is :7167. If this is not the case for you then it needs to be changed in Client angular project inside src -> app -> services -> api-calls.service.ts.

Swagger is also configured for the API so that you can also test the API seperately from the application as well in the browser itself.

CORS policy is already allowed on API side.

