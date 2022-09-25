# Work in Progress

## Design

![Design](docs/design.jfif)

## Running

Don't forget to change the MongoDb connection string in appsettings.json

```
cd TvMaze.Worker
dotnet run
```

## Testing

```
dotnet test
```

## Notes

So, after I saw that just one thread was taking too long, I decided to paralized it to 4 threads.

On my computer, 1000 casts were processed in 8 minutes and after paralized, increased to 3:30 minutes whick took ~210 minutes.

I decided not to paralize shows fetching data as it is already fast enough.

## Bot

This is a bot. We can run it as a CronJob in a Kubernetes cluster.

I would recommend running this bot/worker daily with a cron job in order to update new shows and casts. Less than daily is unnecessary as the request to get shows are 24 hours cached.

And also it is import to mention that the bot runs only looking for shows and casts that one doesn't have so they can be as fast as possible.

```
0 0 * * *
```

### Todo

### In Progress

### Done ✓

- [x] K8s files
- [x] Get shows and save it to database
- [x] Get cast and save it to database
