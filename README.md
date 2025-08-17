How to run

- Server-side project

Run the server using the https profile so the API is launched on https://localhost:7089.
The client is configured to call this URL. If you run the API on a different host or port, update the client .env file (DevelopmentTask/client/.env, key VITE_API_BASE_URL).

- Database

The server project uses SQLite to store imported data. The database is created on first launch and saved as FullStackDeveloperTask.Api/app.db.

- Client

Make sure you have Node installed. The client app is in DevelopmentTask/client. 
Then:

npm i

npm run dev
