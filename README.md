# PDBT

**NOTE**: This is a replacement for the [PDBT](https://github.com/PanicAtTheKernal/PDBT) project completely rewritten in Angular instead of React. All development will be focused on this repo instead of the old one.

----
## Installation 
### Docker:

#### Requirements:
1. Install the following requirements:

- Linux

Note: You can use either <b>Docker</b> or <b>Podman</b>.  
```
Docker
OR
Podman >= v3.4.4
Podman-compose >= v1.0.3
```

- Windows:

```
Docker Desktop >= Latest
```


2. Clone the repo and navigate into the folder:
```Bash
git clone https://github.com/PanicAtTheKernal/PDBT-Refactored
cd "PDBT-Refactored"
```

3. Create the extrasettings.json:

```Bash
cp blankextrasetting.json extrasettings.json 
```

4. Run the Docker Compose file
```Bash
docker-compose up -d
OR
podman-compose up -d
```

<br>

### Development Environment:

1. Install the following requirements:
```
dotnet = 6.0.11
mariadb >= 10.10.2
nodejs >= 14
```

2. Clone the repo and navigate into the folder:
```Bash
git clone https://github.com/PanicAtTheKernal/PDBT-Refactored
cd "PDBT-Refactored"
```

3. Add local https dev-certs:
- Linux

```Bash
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx 
dotnet dev-certs https --trust
```
- Windows

```Bash
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx 
dotnet dev-certs https --trust
```

4. Install dependencies:

```Bash
dotnet restore "PDBT/PDBT.csproj"
```

5. Initialize the database:

```Bash
mysql -u root -p < "./sqlscripts/dev_script.sql"
``` 

6. Run create tables via entity framework:

```Bash
cd PDBT
dotnet ef database update
```

7. Create the extrasettings.json:

```Bash
cp devextrasetting.json extrasettings.json 
```

8. Use any IDE to edit the files and compile the code
----

## **[License](LICENSE)**

