

# 🚀 HotReloadX

A lightweight **.NET hot reload CLI tool** that watches your project files, rebuilds automatically, and restarts your application with zero manual effort.

---

## ✨ Features

* 🔍 Recursive file watching
* ⚡ Fast rebuild & restart workflow
* 🧠 Debounce mechanism (prevents duplicate rebuilds)
* 🔄 Automatic build + execution pipeline
* 🛑 Proper process termination (kills child processes)
* 🚫 Avoids file locks and port conflicts
* 📡 Real-time log streaming
* ❌ Skips restart on build failure

---

## 🏗️ Project Structure

```
HotReloadX/
├── HotReloadX.CLI/     # CLI entry point
├── HotReloadX.Core/    # Core logic (watcher, pipeline, process manager)
├── TestServer/         # Sample .NET server
```

---

## ⚙️ How It Works

1. Watches the project directory for file changes
2. Debounces rapid file events
3. Stops the currently running server
4. Rebuilds the project
5. Restarts the server if the build succeeds
6. Streams logs in real time

---

## ▶️ Usage

### 1. Navigate to CLI

```bash
cd HotReloadX.CLI
```

### 2. Run the tool

```bash
dotnet run -- \
  --root "../TestServer" \
  --build "dotnet build ../TestServer" \
  --exec "dotnet ../TestServer/bin/Debug/net10.0/TestServer.dll --urls=http://localhost:5005"
```

### 3. Open in browser

[http://localhost:5005](http://localhost:5005)

### 4. Modify code

Edit any `.cs` file inside `TestServer` and save.

👉 The server will automatically rebuild and restart.

---

## 🧪 Example Output

```
🔥 Change detected → Reloading...
🛑 Stopping previous server...
🔨 Building project...
✔ Build succeeded
🚀 Starting server...
```

---

## ⚠️ Important Notes

* ❌ Do NOT use `dotnet run` inside `--exec` (causes infinite loops)
* ✅ Always execute the compiled `.dll`
* ⚠️ Ensure the target port is not already in use

---

## 🧠 Key Concepts

* FileSystemWatcher (file change detection)
* Debouncing high-frequency events
* Process lifecycle management
* Handling file locks and port conflicts
* Event-driven pipeline design

---

## 🚀 Future Improvements

* Config file support (`hotreload.json`)
* Ignore patterns (like `.gitignore`)
* Cross-platform process handling improvements
* CLI flags (`--help`, `--verbose`)
* Colored logs

---

## 💼 Why This Project Matters

This project demonstrates:

* Backend engineering skills
* Systems-level thinking
* Real-world problem solving
* Developer tooling experience

---

## 📜 License

MIT License

---
