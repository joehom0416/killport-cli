# Killport CLI
A Command Line Interface for kill the running process which currently using the port.

## Setup
```
dotnet tool install --global killport.Cli.tool --version 1.0.0
```


## Usage

```
killport [-p|--port] (<port number>)
killport [-h|--help]
killport [-v|--version]
```
#### Example (Windows Command Prompt)

```
killport --p 8070                // Kill the process which currently using port 8070.
killport -h                // Display help.
killport -v              // Display version of this cli.
```
