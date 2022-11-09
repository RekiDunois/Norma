# My assistant bot

## what can this bot do?

(WIP)

## Deploy with Dockerfile:

```bash
git clone https://github.com/RekiDunois/Norma.git
cd Norma
```

Add config file named `appsettings.json` with content like this:

```json
{
    "Logging": {
        "IncludeScopes": false,
        "LogLevel": {
            "Default": "Debug",
            "System": "Information",
            "Microsoft": "Information"
        }
    },
    "BotConfiguration": {
        "BotToken": "xxxxxx:xxxxxx",
        "HostAddress": "https://localhost",
        "UserIds": [
            "1234567890"
        ]
    },
    "ProgramConfiguration": {
        "AssemblyNames": [
            "Norma"
        ]
    }
}
```

About getting the `BotToken` and `UserIds` content, you can follow the telegram docs in [here](https://core.telegram.org/bots#3-how-do-i-create-a-bot)

And then, run the docker build and run command, just like that.

```bash
docker build -t norma:version .
docker run -d norma:version
```


