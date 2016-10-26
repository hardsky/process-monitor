## ProcessMonitor

Program, that running on Windows OS machine and watch current processes.

It allows multiple clients, that can communicate through different transport.
(In this moment only named pipes are implemented. But transport is separated into single module,
so other transport can be implemented in easy way.)

There are simple web frontend for MonitorProcess utility, also.
Web communicates with MonitorProcess as common client through pipe transport.
Web is static html, that updated by server through SignalR.
Potentially, web server and ProcessMonitor can be ran on different machines.