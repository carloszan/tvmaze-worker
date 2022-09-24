# Work in Progress

## Design

![Design](docs/design.jfif)

## Running

Don't forget to change the MongoDb connection string in appsettings.json

```
dotnet run
```

## Testing

```
dotnet test
```

## Notes

So, after I saw that just one thread was taking too long, I decided to paralized it to 4 threads.

On my computer, 1000 casts were processed in 8 minutes and after paralized, changed to 3.30 minutes.

I decided not to paralize shows as it is already fast enough.

### Todo

- [ ] K8s files

### In Progress

### Done ✓

- [x] Get shows and save it to database
- [x] Get cast and save it to database
