## About
LibRbxl is software library targeting .NET 4.5 and that provides an easy way to read and write Roblox .rbxl files.

## Usage Examples
Add a script to an existing place.
```csharp
var doc = RobloxDocument.FromFile("Example.rbxl");
var serverScriptService = doc:FindFirstChild("ServerScriptService");
var script = new Script();
script.Source = "print(\"Hello, World!\")";
script.Parent = serverScriptService;
doc.Save("Example.rbxl");
```
Open a file and count the number of parts.
```csharp
var doc = RobloxDocument.FromFile("Example2.rbxl");
var partCount = doc.Instances.Count(n => n Is Part);
```
Create document from scratch.
```csharp
var doc = new RobloxDocument();
var workspace = new Workspace();
doc.Children.Add(workspace);
var model = new Model();
model.Name = "ExampleModel";
model.Parent = workspace; // Setting the parent property automatically adds the instance as a child of the parent
doc.Save("Example3.rbxl");
```
## Build
LibRbxl is a stand-alone DLL. To compile it, you will need Visual Studio. Clone the repository locally and build it using the solution file.
## Contribute
Contributions are welcome! If you're not inclined to dig into the code yourself, but encounter an issue, feel free to submit an issue using the GitHub issue tracker.
## License
LibRbxl is open source, and is licensed under the MIT License. The full text is included in the repository.
